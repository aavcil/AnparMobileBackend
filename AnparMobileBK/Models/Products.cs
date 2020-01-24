using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBK.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ModelCode { get; set; }
        public string Description { get; set; }
        public string ProductMeasure { get; set; }
        public string Series { get; set; }
        public int Size { get; set; }
        public int CartonAmount { get; set; }
        public string RawMaterials { get; set; }
        public int CartonSize { get; set; }
        public double CartonVolume { get; set; }
        public double CartonWeight { get; set; }
        public string CartonMeasure { get; set; }
        public double ListPrice { get; set; }
        public double Price { get; set; }
        public string ImageUrl => "Test";

    }
}
