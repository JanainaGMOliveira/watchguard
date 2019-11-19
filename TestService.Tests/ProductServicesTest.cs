using System;
using System.Collections.Generic;
using System.Linq;
using Infra.DTO.Ins;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsAndExtensions.Models;
using APIWarehouse.Context;
using APIWarehouse.Repository;

namespace TestService.Tests
{
    [TestClass]
    public class ProductServicesTest
    {
        private static WarehouseContext InitializeTests()
        {
            DbContextOptions<WarehouseContext> options;
            var builder = new DbContextOptionsBuilder<WarehouseContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            return new WarehouseContext(options);
        }


        [TestMethod]
        public void TestAddProductSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);

            var productTest = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };

            productRep.Add(productTest);

            // só inseriu um product
            Assert.AreEqual(1, context.Product.Count());
            // os dados inseridos estão corretos
            var productInserida = context.Product.LastOrDefault();
            Assert.AreEqual(productInserida.Name, productTest.Name);
            Assert.AreEqual(productInserida.Unit, productTest.Unit);
            Assert.AreEqual(productInserida.Price, productTest.Price);
            Assert.AreEqual(productInserida.Quantity, productTest.Quantity);
            Assert.AreEqual(productInserida.Active, productTest.Active);
            Assert.AreEqual(productInserida.BrandId, productTest.BrandId);
        }

        [TestMethod]
        public void TestAddProductFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var productTest = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 0
            };

            // não existe brand, não cria product
            Assert.ThrowsException<ArgumentNullException>(() => productRep.Add(productTest));
        }

        [TestMethod]
        public void TestUpdateProductSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest);

            // os dados editados estão corretos
            var productEditada = context.Product.LastOrDefault();
            Assert.AreNotEqual(productEditada.Name, productTest.Name);
            Assert.AreNotEqual(productEditada.Unit, productTest.Unit);
            // não editou a o preço
            Assert.AreEqual(productEditada.Price, productTest.Price);
            Assert.AreEqual(productEditada.Quantity, productTest.Quantity);
            Assert.AreEqual(productEditada.Active, productTest.Active);
            Assert.AreEqual(productEditada.BrandId, productTest.BrandId);
        }

        [TestMethod]
        public void TestUpdateProductFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);

            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };

            productRep.Add(productTest0);

            // Tentando editar uma product que não existe
            var productTest1 = new ProductIn()
            {
                Id = 2,
                Name = "Product Update",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 0
            };

            // joga exceção quando a product a ser editada não existe
            Assert.ThrowsException<ArgumentNullException>(() => productRep.Update(productTest1));
        }


        [TestMethod]
        public void TestDeleteProductFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };

            productRep.Add(productTest0);

            Assert.ThrowsException<ArgumentNullException>(() => productRep.Delete(0));
        }

        [TestMethod]
        public void TestListAllProductSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest0);
            var productTest1 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest1);

            var products = productRep.ListAll();

            Assert.AreEqual(products.Count(), 2);
        }
        [TestMethod]
        public void TestListAllActiveProductSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest0);
            var productTest1 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = false,
                BrandId = 1
            };
            productRep.Add(productTest1);

            var products = productRep.ListAll();

            Assert.AreEqual(products.Count(), 1);
        }
        [TestMethod]
        public void TestGetByIdProductSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest0);
            var productTest1 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest1);

            var product = productRep.GetById(1);

            Assert.AreEqual(product.Id, 1);
            Assert.AreEqual(product.Name, productTest0.Name);
            Assert.AreEqual(product.Unit, productTest0.Unit);
            Assert.AreEqual(product.Price, productTest0.Price);
            Assert.AreEqual(product.Quantity, productTest0.Quantity);
            Assert.AreEqual(product.Active, productTest0.Active);
        }
        [TestMethod]
        public void TestGetByIdProductFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };

            productRep.Add(productTest0);

            Assert.ThrowsException<ArgumentNullException>(() => productRep.GetById(0));
        }
        [TestMethod]
        public void TestSumActiveProductsSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest0);
            var productTest1 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = false,
                BrandId = 1
            };
            productRep.Add(productTest1);

            var sum = productRep.SumOfActiveProducts();
            Assert.AreEqual(sum, 1);
        }
        [TestMethod]
        public void TestSumActiveProductsFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);
            var productRep = new ProductRepository(context, brandRep);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest);
            var productTest0 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest0);
            var productTest1 = new ProductIn()
            {
                Name = "Product Test",
                Unit = "Unit Test",
                Price = 1.0,
                Quantity = 1,
                Active = true,
                BrandId = 1
            };
            productRep.Add(productTest1);

            var sum = productRep.SumOfActiveProducts();
            Assert.AreNotEqual(sum, 1);
        }
    }
}
