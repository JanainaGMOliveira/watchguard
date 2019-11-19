using System.Collections.Generic;
using System.Linq;
using Infra.DTO.Ins;
using Infra.DTO.Outs;

namespace APIWarehouse.Repository.Interface
{
    public interface IProductRepository
    {
        ProductOut GetById(long id);
        void Add(ProductIn productIn);
        void Update(ProductIn productIn);
        void Delete(long id);
        IEnumerable<ProductOut> ListAll();
        long SumOfActiveProducts();
        IEnumerable<IGrouping<string, ProductOut>> ProductsByBrand();
    }
}