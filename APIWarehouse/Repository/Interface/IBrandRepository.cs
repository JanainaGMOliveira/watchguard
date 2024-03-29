using System.Collections.Generic;
using Infra.DTO.Ins;
using Infra.DTO.Outs;

namespace APIWarehouse.Repository.Interface
{
    public interface IBrandRepository
    {
        BrandOut GetById(long id);
        void Add(BrandIn brandIn);
        void Update(BrandIn brandIn);
        void Delete(long id);
        IEnumerable<BrandOut> ListAll();
    }
}