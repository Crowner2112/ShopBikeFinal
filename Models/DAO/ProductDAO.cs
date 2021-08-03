using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class ProductDAO : IRepository<Product>
    {
        private ShopBikeDbContext db = null;

        public ProductDAO()
        {
            db = new ShopBikeDbContext();
        }

        public bool Create(Product entity)
        {
            try
            {
                db.Products.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Product entity)
        {
            try
            {
                db.Products.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public bool Update(Product entity)
        {
            try
            {
                var oldItem = db.Products.Find(entity.ID);
                oldItem.Name = entity.Name;
                oldItem.CategoryID = entity.CategoryID;
                oldItem.Price = entity.Price;
                oldItem.Quantity = entity.Quantity;
                oldItem.Description = entity.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }
    }
}