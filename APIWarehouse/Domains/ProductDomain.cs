using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using APIWarehouse.Domains.Interface;
using APIWarehouse.Repository.Interface;
using System.Diagnostics;

namespace APIWarehouse.Domains
{
    public class ProductDomain : IProductDomain
    {
        private readonly IProductRepository _repo;

        public ProductDomain(IProductRepository repo)
        {
            _repo = repo;
        }

        public void Add(ProductIn productIn)
        {
            _repo.Add(productIn);
        }
        public IEnumerable<ProductOut> ListAll()
        {
            return _repo.ListAll();
        }
        public ProductOut GetById(long id)
        {
            return _repo.GetById(id);
        }
        public void Update(ProductIn productIn)
        {
            _repo.Update(productIn);
        }
        public void Delete(long id)
        {
            _repo.Delete(id);
        }
        public string FileWithActiveProducts()
        {
            var sum = _repo.SumOfActiveProducts();
            // SAVE IN TXT FILE
            var docPath = @"/app/Arquivos"; // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (var outputFile = new StreamWriter(Path.Combine(docPath, "SumProducts.txt")))
                outputFile.WriteLine("The sum of active products  is " + sum + ".");

            return Path.Combine(docPath, "SumProducts.txt");
        }

        public string FileWithProductsByBrand()
        {
            var products = _repo.ProductsByBrand().Select(x => new { Brand = x.Key, Quantity = x.Count() });
            var doc = new XmlDocument();
            var docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            var brandsNode = doc.CreateElement("brands");
            doc.AppendChild(brandsNode);

            foreach (var item in products)
            {
                var brandNode = doc.CreateElement("brand");
                brandsNode.AppendChild(brandNode);

                var nameNode = doc.CreateElement("name");
                nameNode.AppendChild(doc.CreateTextNode(item.Brand));
                brandNode.AppendChild(nameNode);
                var priceNode = doc.CreateElement("qtdproduct");
                priceNode.AppendChild(doc.CreateTextNode(item.Quantity.ToString()));
                brandNode.AppendChild(priceNode);
            }
            var docPath = @"/app/Arquivos";
            using (var outputFile = new StreamWriter(Path.Combine(docPath, "ProductsByBrand.xml")))
                doc.Save(outputFile);

            return Path.Combine(docPath, "ProductsByBrand.xml");
        }

    }
}