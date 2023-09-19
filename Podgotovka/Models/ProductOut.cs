using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podgotovka.Models
{
    public class ProductOut
    {
        public ProductOut(int id, string nameProduct, decimal price, int quantity)
        {
            Id = id;
            NameProduct = nameProduct;
            Price = price;
            Quantity = quantity;
        }
        public ProductOut()
        {
           
        }

        public int Id { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
