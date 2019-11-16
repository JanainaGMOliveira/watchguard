using System.Collections.Generic;
using System.Net.Http;
using Infra.DTO.Ins;
using Infra.DTO.Outs;

namespace APIWarehouse.Domains.Interface
{
    public interface IProductDomain
    {
        ProductOut GetById(long id);
        void Add(ProductIn productIn);
        void Update(ProductIn productIn);
        void Delete(long id);
        IEnumerable<ProductOut> ListAll(bool? filtroAtivo);
        string FileWithActiveProducts();
    }
}