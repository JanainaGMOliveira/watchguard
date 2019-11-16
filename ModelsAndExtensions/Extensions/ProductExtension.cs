using System;
using System.ComponentModel.DataAnnotations.Schema;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using ModelsAndExtensions.Models;

namespace ModelsAndExtensions.Extensions
{
    public static class ProductExtension
    {
        public static ProductOut ToOut(this Product product)
        {
            if(product == null)
                throw new ArgumentNullException("Product can't be null.");

            var productOut = new ProductOut
            {
                Id = product.Id,
                Name = product.Name,
                Unit = product.Unit,
                Quantity = product.Quantity,
                Price = product.Price,
                Active = product.Active
            };

            if (product.Brand != null)
                productOut.BrandName = product.Brand.Name;

            return productOut;
        }

        public static Product ToModel(this ProductIn productIn)
        {
            var productModel = new Product
            {
                Name = productIn.Name,
                Unit = productIn.Unit,
                Quantity = productIn.Quantity,
                Price = productIn.Price,
                Active = productIn.Active,
                BrandId = productIn.BrandId
            };

            return productModel;
        }
    }
}