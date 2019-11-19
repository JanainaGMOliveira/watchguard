using System;
using System.Collections.Generic;
using System.Linq;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using Microsoft.EntityFrameworkCore;
using ModelsAndExtensions.Extensions;
using APIWarehouse.Context;
using APIWarehouse.Repository.Interface;

namespace APIWarehouse.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseContext _context;
        private readonly IBrandRepository _brandRep;
        public ProductRepository(WarehouseContext context, IBrandRepository brandRep)
        {
            _context = context;
            _brandRep = brandRep;
        }

        public void Add(ProductIn productIn)
        {
            var brand = _brandRep.GetById(productIn.BrandId);
            if (brand == null)
                throw new ArgumentNullException("The brand doesn't exists.");

            var product = productIn.ToModel();
            _context.Product.Add(product);
            _context.SaveChanges();
        }
        public IEnumerable<ProductOut> ListAll()
        {
            var products = _context.Product
                                   .Include(x => x.Brand)
                                   .AsQueryable();

            return products.AsEnumerable().Select(x => x?.ToOut());
        }
        public ProductOut GetById(long id)
        {
            var product = _context.Product
                                  .Include(x => x.Brand)
                                  .SingleOrDefault(x => x.Id == id);

            if (product == null)
                throw new ArgumentNullException("The product doesn't exists.");

            return product?.ToOut();
        }
        public void Update(ProductIn productIn)
        {
            var product = _context.Product.Find(productIn.Id);

            if (product == null)
                throw new ArgumentNullException("The product doesn't exists.");

            product.Name = productIn.Name;
            product.Unit = productIn.Unit;
            product.Quantity = productIn.Quantity;
            product.Price = productIn.Price;
            product.Active = productIn.Active;
            var brand = _brandRep.GetById(productIn.BrandId);
            if (brand == null)
                throw new ArgumentNullException("The brand doesn't exists.");

            product.BrandId = productIn.BrandId;

            _context.Product.Update(product);
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            var product = _context.Product.Find(id);

            if (product == null)
                throw new ArgumentNullException("The product doesn't exists.");

            _context.Product.Remove(product);
            _context.SaveChanges();
        }
        public long SumOfActiveProducts()
        {
            return _context.Product
                           .Include(x => x.Brand)
                           .Where(x => x.Active == true)
                           .Sum(x => x.Quantity);
        }

        public IEnumerable<IGrouping<string, ProductOut>> ProductsByBrand()
        {
            return _context.Product
                           .Include(x => x.Brand)
                           .Select(x => x.ToOut())
                           .GroupBy(x => x.BrandName)
                           .AsEnumerable();
                           
        }
    }
}