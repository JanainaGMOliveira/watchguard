using System;
using System.Collections.Generic;
using System.Linq;
using APIWarehouse.Context;
using APIWarehouse.Repository.Interface;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using Microsoft.EntityFrameworkCore;
using ModelsAndExtensions.Extensions;

namespace APIWarehouse.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseContext _context;
        
        public ProductRepository(WarehouseContext context)
        {
            _context = context;
        }

        public void Add(ProductIn productIn)
        {
            var product = productIn.ToModel();
            _context.Product.Add(product);
            _context.SaveChanges();
        }
        public IEnumerable<ProductOut> ListAll(bool? filtroAtivo)
        {
            var products = _context.Product
                                   .Include(x => x.Brand)
                                   .AsQueryable();
            if(filtroAtivo.HasValue)
                products = products.Where(x => x.Active == filtroAtivo.Value).AsQueryable();

            return products.AsEnumerable().Select(x => x?.ToOut());
        }
        public ProductOut GetById(long id)
        {
            var product = _context.Product
                                  .Include(x => x.Brand)
                                  .SingleOrDefault(x => x.Id == id);

            if(product == null)
                throw new ArgumentNullException("The product doesn't exists.");

            return product?.ToOut();
        }
        public void Update(ProductIn productIn)
        {
            var product = _context.Product.Find(productIn.Id);

            if(product == null)
                throw new ArgumentNullException("The product doesn't exists.");

            product.Name = productIn.Name;
            product.Unit = productIn.Unit;
            product.Quantity = productIn.Quantity;
            product.Price = productIn.Price;
            product.Active = productIn.Active;
            product.BrandId  = productIn.BrandId;
            
            _context.Product.Update(product);
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            var product = _context.Product.Find(id);

            if(product == null)
                throw new ArgumentNullException("The product doesn't exists.");

            _context.Product.Remove(product);
            _context.SaveChanges();
        }
    }
}