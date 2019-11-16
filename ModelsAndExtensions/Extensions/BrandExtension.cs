using System;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using ModelsAndExtensions.Models;

namespace ModelsAndExtensions.Extensions
{
    public static class BrandExtension
    {
        public static BrandOut ToOut(this Brand brand)
        {
            if(brand == null)
                throw new ArgumentNullException("Brand can't be null.");

            var brandOut = new BrandOut
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description
            };

            return brandOut;
        }

        public static Brand ToModel(this BrandIn brandIn)
        {
            var brandModel = new Brand
            {
                Name = brandIn.Name,
                Description = brandIn.Description
            };

            return brandModel;
        }
    }
}