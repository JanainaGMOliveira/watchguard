using System.Collections.Generic;
using System.Linq;
using APIWarehouse.Context;
using APIWarehouse.Repository.Interface;
using Infra.DTO;
using Infra.Filtros;
using ModelsAndExtensions.Models;

namespace APIWarehouse.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly WarehouseContext _context;
        public BrandRepository(WarehouseContext context)
        {
            _context = context;
        }
        public void Add(BrandDTO brandIn)
        {
            var brand = new Brand
                            {
                                Name = brandIn.Name,
                                Description = brandIn.Description
                            };
            _context.Brand.Add(brand);
            _context.SaveChanges();
        }

        public IEnumerable<BrandDTO> Get(BrandFilter filtro)
        {
            var brands = _context.Brand.Select(x => x);//.Where(id);
            return brands.Select(x => new BrandDTO
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        });
        }

        public BrandDTO GetById(long id)
        {
            var brand = _context.Brand.Find(id);
            return new BrandDTO
                        {
                            Id = brand.Id,
                            Name = brand.Name,
                            Description = brand.Description
                        };
        }

        public void Update(BrandDTO brandIn)
        {
            var brand = _context.Brand.Find(brandIn.Id);
            brand.Name = brandIn.Name;
            brand.Description = brandIn.Description;
            
            _context.Brand.Update(brand);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var brand = _context.Brand.Find(id);
            // deletar as associações com products
            _context.Brand.Remove(brand);
            _context.SaveChanges();
        }
    }
}