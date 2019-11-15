using System.Collections.Generic;
using Infra.DTO;
using Infra.Filtros;

namespace APIWarehouse.Repository.Interface
{
    public interface IProductRepository
    {
        ProductDTO GetById(long id);
        void Add(ProductDTO productIn);
        void Update(ProductDTO productIn);
        void Delete(long id);
        IEnumerable<ProductDTO> Get(ProductFilter filtro);
    }
}