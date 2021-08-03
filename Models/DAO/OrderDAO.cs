using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class OrderDAO : IRepository<Order>
    {
        private ShopBikeDbContext db = null;

        public OrderDAO()
        {
            db = new ShopBikeDbContext();
        }

        public bool Create(Order entity)
        {
            try
            {
                db.Orders.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Order entity)
        {
            try
            {
                db.Orders.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Order GetById(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CustomerName.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public bool Update(Order entity)
        {
            try
            {
                var oldItem = db.Orders.Find(entity.ID);
                oldItem.CustomerName = entity.CustomerName;
                oldItem.Email = entity.Email;
                oldItem.PhoneNumber = entity.PhoneNumber;
                oldItem.Address = entity.Address;
                oldItem.Total = entity.Total;
                oldItem.CreatedDate = entity.CreatedDate;
                oldItem.Status = entity.Status;
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