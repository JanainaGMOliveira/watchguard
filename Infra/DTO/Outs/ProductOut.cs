using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.DTO.Outs
{
    public class ProductOut
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public string BrandName { get; set; }
    }
}