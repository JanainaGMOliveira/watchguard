using System.Collections.Generic;
using Infra.DTO.Ins;
using Infra.DTO.Outs;

namespace APIWarehouse.Domains.Interface
{
    public interface IBrandDomain
    {
        BrandOut GetById(long id);
        void Add(BrandIn brandIn);
        void Update(BrandIn brandIn);
        void Delete(long id);
        IEnumerable<BrandOut> ListAll();
    }
}