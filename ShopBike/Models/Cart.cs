using System.Collections.Generic;
using System.Linq;

namespace ShopBike.Models
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
    }

    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public bool Add(int proID, string name, string image, decimal? price, int quantity = 1)
        {
            var item = items.FirstOrDefault(x => x.ProductID == proID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    ProductID = proID,
                    Name = name,
                    Image = image,
                    Quantity = quantity,
                    Price = price
                });
                return true;
            }
            else
            {
                item.Quantity++;
                item.Price *= item.Quantity;
                return false;
            }
        }

        public void Update(int id, int quantity, decimal? price)
        {
            var item = items.Find(x => x.ProductID == id);
            if (item != null)
            {
                item.Quantity = quantity;
                item.Price = price;
            }
        }

        public void Delete(int id)
        {
            var item = items.Find(x => x.ProductID == id);
            items.Remove(item);
        }

        public int Total()
        {
            int total = 0;
            foreach (var item in items)
            {
                total += (int)item.Price;
            }
            return total;
        }
    }
}