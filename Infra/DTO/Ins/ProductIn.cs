using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.DTO.Ins
{
    public class ProductIn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public long BrandId { get; set; }
    }
}