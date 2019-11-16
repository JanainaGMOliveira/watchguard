using System.Collections.Generic;
using System.Linq;
using APIWarehouse.Context;
using APIWarehouse.Repository.Interface;
using Infra.DTO;
using Infra.Filtros;
using Microsoft.EntityFrameworkCore;
using ModelsAndExtensions.Models;

namespace APIWarehouse.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseContext _context;
        public ProductRepository(WarehouseContext context)
        {
            _context = context;
        }
        public void Add(ProductDTO productIn)
        {
            var product = new Product
                            {
                                Name = productIn.Name,
                                Unit = productIn.Unit,
                                Quantity = productIn.Quantity,
                                Price = productIn.Price,
                                Active = productIn.Active,
                                BrandId  = productIn.Brand.Id
                            };
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        public IEnumerable<ProductDTO> Get(ProductFilter filtro)
        {
            var products = _context.Product
                                   .Include(x => x.Brand)
                                   .Select(x => x);//.Where(id);
            return products.Select(x => new ProductDTO
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Unit = x.Unit,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            Active = x.Active,
                            Brand  = new BrandDTO
                                    {
                                        Id = x.Brand.Id,
                                        Name = x.Brand.Name,
                                        Description = x.Brand.Description
                                    }
                        });
        }

        public ProductDTO GetById(long id)
        {
            var product = _context.Product
                                  .Include(x => x.Brand)
                                  .SingleOrDefault(x => x.Id == id);
            return new ProductDTO
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Unit = product.Unit,
                            Quantity = product.Quantity,
                            Price = product.Price,
                            Active = product.Active,
                            Brand  = new BrandDTO
                                    {
                                        Id = product.Brand.Id,
                                        Name = product.Brand.Name,
                                        Description = product.Brand.Description
                                    }
                        };
        }

        public void Update(ProductDTO productIn)
        {
            var product = _context.Product.Find(productIn.Id);
            product.Name = productIn.Name;
            product.Unit = productIn.Unit;
            product.Quantity = productIn.Quantity;
            product.Price = productIn.Price;
            product.Active = productIn.Active;
            product.BrandId  = productIn.Brand.Id;
            
            _context.Product.Update(product);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var product = _context.Product.Find(id);
            _context.Product.Remove(product);
            _context.SaveChanges();
        }
    }
}