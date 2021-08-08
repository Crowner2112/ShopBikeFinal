using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class ImageDAO : IRepository<Image>
    {
        private ShopBikeDbContext db = null;

        public ImageDAO()
        {
            db = new ShopBikeDbContext();
        }

        public bool Create(Image entity)
        {
            try
            {
                db.Images.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Image entity)
        {
            try
            {
                db.Images.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Image GetById(int id)
        {
            return db.Images.Find(id);
        }

        public Image GetMainPicByProID(int id)
        {
            return db.Images.SingleOrDefault(x => x.ProductID == id && x.MainPic == true);
        }

        public List<Image> GetByCategoryId(int id)
        {
            return db.Images.Where(x => x.Product.CategoryID == id && x.MainPic == true).ToList();
        }

        public List<Image> GetByProductId(int id)
        {
            return db.Images.Where(x => x.ProductID == id).ToList();
        }

        public IEnumerable<Image> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Image> model = db.Images;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ProductID == Int32.Parse(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public bool Update(Image entity)
        {
            try
            {
                var oldItem = db.Images.Find(entity.ID);
                oldItem.ProductID = entity.ProductID;
                oldItem.Link = entity.Link;
                oldItem.MainPic = entity.MainPic;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Image> GetAll()
        {
            return db.Images.ToList();
        }
    }
}