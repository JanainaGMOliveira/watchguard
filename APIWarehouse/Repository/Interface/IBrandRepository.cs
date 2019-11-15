using System.Collections.Generic;
using Infra.DTO;
using Infra.Filtros;

namespace APIWarehouse.Repository.Interface
{
    public interface IBrandRepository
    {
        BrandDTO GetById(long id);
        void Add(BrandDTO brandIn);
        void Update(BrandDTO brandIn);
        void Delete(long id);
        IEnumerable<BrandDTO> Get(BrandFilter filtro);
    }
}