using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class OrderDetailDAO : IRepository<OrderDetail>
    {
        private ShopBikeDbContext db = null;

        public OrderDetailDAO()
        {
            db = new ShopBikeDbContext();
        }

        public bool Create(OrderDetail entity)
        {
            try
            {
                var item = GetListODByODIdAndPDId(entity.OrderID, entity.ProductID);
                if (item != null)
                {
                    item.Quantity += entity.Quantity;
                    db.SaveChanges();
                }
                else
                {
                    db.OrderDetails.Add(entity);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(OrderDetail entity)
        {
            try
            {
                db.OrderDetails.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public OrderDetail GetListODByODIdAndPDId(int orderId, int productId)
        {
            return db.OrderDetails.FirstOrDefault(x => x.OrderID == orderId && x.ProductID == productId);
        }

        public IEnumerable<OrderDetail> ListAllPaging(int orderId, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            model = model.Where(x => x.OrderID == orderId);
            return model.OrderBy(x => x.OrderID).ToPagedList(page, pageSize);
        }

        public IEnumerable<OrderDetail> ListAllPaging(string searchString, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public bool Update(OrderDetail entity)
        {
            try
            {
                var oldItem = GetListODByODIdAndPDId(entity.OrderID, entity.ProductID);
                oldItem.Quantity = entity.Quantity;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        OrderDetail IRepository<OrderDetail>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}