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
        private static void InitializeTests(WarehouseContext context, BrandRepository brandRep)
        {
            DbContextOptions<WarehouseContext> options;
            var builder = new DbContextOptionsBuilder<WarehouseContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
            context = new WarehouseContext(options);
            brandRep = new BrandRepository(context);
        }

        [TestMethod]
        public void TestAddBrandSuccess()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);
            var brandTest = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest);

            // só inseriu uma brand
            Assert.AreEqual(1, context.Brand.Count());
            // os dados inseridos estão corretos
            var brandInserida = context.Brand.LastOrDefault();
            Assert.AreEqual(brandInserida.Name, brandTest.Name);
            Assert.AreEqual(brandInserida.Description, brandTest.Description);
        }

        [TestMethod]
        public void TestUpdateBrandSuccess()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

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
            // não inseriu nada novo
            Assert.AreEqual(1, context.Brand.Count());
            // os dados editados estão corretos
            var brandEditada = context.Brand.LastOrDefault();
            Assert.AreEqual(brandEditada.Name, brandTest1.Name);
            // não editou a descrição
            Assert.AreNotEqual(brandEditada.Description, brandTest1.Description);
        }

        [TestMethod]
        public void TestUpdateBrandFail()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            // Tentando editar uma brand que não existe
            var brandTest1 = new BrandIn()
            {
                Id = 2,
                Name = "Brand Update",
                Description = "Description Brand Test"
            };

            // joga exceção quando a brand a ser editada não existe
            Assert.ThrowsException<ArgumentNullException>(() => brandRep.Update(brandTest1));
        }

        [TestMethod]
        public void TestDeleteBrandSuccess()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            brandRep.Delete(1);

            Assert.AreEqual(0, context.Brand.Count());
        }

        [TestMethod]
        public void TestDeleteBrandFail()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

            var brandTest0 = new BrandIn()
            {
                Name = "Brand Test",
                Description = "Description Brand Test"
            };

            brandRep.Add(brandTest0);

            Assert.ThrowsException<ArgumentNullException>(() => brandRep.Delete(0));
        }

        [TestMethod]
        public void TestListAllBrandSuccess()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

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
        public void TestGetByIdBrandSuccess()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

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

            var brand = brandRep.GetById(1);

            Assert.AreEqual(brand.Id, 1);
            Assert.AreEqual(brand.Name, brandTest0.Name);
            Assert.AreEqual(brand.Description, brandTest0.Description);
        }
        [TestMethod]
        public void TestGetByIdBrandFail()
        {
            WarehouseContext context = null;
            BrandRepository brandRep = null;
            InitializeTests(context, brandRep);

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
