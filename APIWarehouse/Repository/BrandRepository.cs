using System;
using System.Collections.Generic;
using System.Linq;
using APIWarehouse.Context;
using APIWarehouse.Repository.Interface;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using ModelsAndExtensions.Extensions;

namespace APIWarehouse.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly WarehouseContext _context;
        public BrandRepository(WarehouseContext context)
        {
            _context = context;
        }
        public void Add(BrandIn brandIn)
        {
            var brand = brandIn.ToModel();
            _context.Brand.Add(brand);
            _context.SaveChanges();
        }
        public IEnumerable<BrandOut> ListAll()
        {
            var brands = _context.Brand;

            return brands.AsEnumerable().Select(x => x?.ToOut());
        }
        public BrandOut GetById(long id)
        {
            var brand = _context.Brand
                                .SingleOrDefault(x => x.Id == id);

            if(brand == null)
                throw new ArgumentNullException("The brand doesn't exists.");

            return brand?.ToOut();
        }
        public void Update(BrandIn brandIn)
        {
            var brand = _context.Brand.Find(brandIn.Id);

            if(brand == null)
                throw new ArgumentNullException("The brand doesn't exists.");

            brand.Name = brandIn.Name;
            brand.Description = brandIn.Description;
            
            _context.Brand.Update(brand);
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            var brand = _context.Brand.Find(id);

            if(brand == null)
                throw new ArgumentNullException("The brand doesn't exists.");

            _context.Brand.Remove(brand);
            _context.SaveChanges();
        }
    }
}