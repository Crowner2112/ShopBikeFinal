using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class CategoryDAO : IRepository<Category>
    {
        private ShopBikeDbContext db = null;

        public CategoryDAO()
        {
            db = new ShopBikeDbContext();
        }

        public bool Create(Category entity)
        {
            try
            {
                db.Categories.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Category entity)
        {
            try
            {
                db.Categories.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public bool Update(Category entity)
        {
            try
            {
                var oldItem = db.Categories.Find(entity.ID);
                oldItem.Name = entity.Name;
                oldItem.Description = entity.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}