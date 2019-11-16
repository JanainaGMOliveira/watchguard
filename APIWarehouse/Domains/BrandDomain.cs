using System.Collections.Generic;
using APIWarehouse.Domains.Interface;
using APIWarehouse.Repository.Interface;
using Infra.DTO.Ins;
using Infra.DTO.Outs;

namespace APIWarehouse.Domains
{
    public class BrandDomain : IBrandDomain
    {
        private readonly IBrandRepository _repo;
        public BrandDomain(IBrandRepository repo)
        {
            _repo = repo;
        }
        public void Add(BrandIn brandIn)
        {
            _repo.Add(brandIn);
        }
        public IEnumerable<BrandOut> ListAll()
        {
            return _repo.ListAll();
        }
        public BrandOut GetById(long id)
        {
            return _repo.GetById(id);
        }
        public void Update(BrandIn brandIn)
        {
            _repo.Update(brandIn);
        }
        public void Delete(long id)
        {
            _repo.Delete(id);
        }
    }
}