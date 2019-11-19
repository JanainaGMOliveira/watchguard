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
    public class BrandServicesTest
    {
        private WarehouseContext InitializeTests()
        {
            DbContextOptions<WarehouseContext> options;
            var builder = new DbContextOptionsBuilder<WarehouseContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            return new WarehouseContext(options);
        }

        [TestMethod]
        public void TestListAllBrandSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest0);
            var brandTest1 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest1);

            var brands = brandRep.ListAll();

            Assert.AreEqual(brands.Count(), 2);
        }

        [TestMethod]
        public void TestAddBrandSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest);

            // os dados inseridos estão corretos
            var brandInserida = context.Brand.LastOrDefault();
            Assert.AreEqual(brandInserida.Name, brandTest.Name);
            Assert.AreEqual(brandInserida.Description, brandTest.Description);
        }

        [TestMethod]
        public void TestUpdateBrandSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            var brandTest1 = new BrandIn()
            {
                Id = 1,
                Name = "Brand Update",
                Description = "Description Brand Test"
            };

            brandRep.Update(brandTest1);
            // os dados editados estão corretos
            var brandEditada = context.Brand.LastOrDefault();
            Assert.AreNotEqual(brandEditada.Name, brandTest1.Name);
            // não editou a descrição
            Assert.AreEqual(brandEditada.Description, brandTest1.Description);
        }

        [TestMethod]
        public void TestUpdateBrandFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            // Tentando editar uma brand que não existe
            var brandTest1 = new BrandIn()
            {
                Id = 0,
                Name = "Brand Update",
                Description = "Description Brand Test"
            };

            // joga exceção quando a brand a ser editada não existe
            Assert.ThrowsException<ArgumentNullException>(() => brandRep.Update(brandTest1));
        }


        [TestMethod]
        public void TestDeleteBrandFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            Assert.ThrowsException<ArgumentNullException>(() => brandRep.Delete(0));
        }

        
        [TestMethod]
        public void TestGetByIdBrandSuccess()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Update",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest0);
            var brandTest1 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };
            brandRep.Add(brandTest1);

            var brand = brandRep.GetById(1);

            Assert.AreEqual(brand.Id, 1);
            Assert.AreEqual(brand.Name, brandTest0.Name);
            Assert.AreEqual(brand.Description, brandTest0.Description);
        }
        [TestMethod]
        public void TestGetByIdBrandFail()
        {
            var context = InitializeTests();
            var brandRep = new BrandRepository(context);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            Assert.ThrowsException<ArgumentNullException>(() => brandRep.GetById(0));
        }
    }
}
