using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAO
{
    public class AccountDAO : IRepository<Account>
    {
        private ShopBikeDbContext db = null;

        public AccountDAO()
        {
            db = new ShopBikeDbContext();
        }

        public bool Create(Account entity)
        {
            try
            {
                db.Accounts.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Account entity)
        {
            try
            {
                db.Accounts.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Account GetById(int id)
        {
            return db.Accounts.Find(id);
        }

        public IEnumerable<Account> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.FullName.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public bool Update(Account entity)
        {
            try
            {
                var oldItem = db.Accounts.Find(entity.ID);
                oldItem.FullName = entity.FullName;
                oldItem.Email = entity.Email;
                oldItem.PhoneNumber = entity.PhoneNumber;
                oldItem.Password = entity.Password;
                oldItem.Address = entity.Address;
                oldItem.Role = entity.Role;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Login(string email, string passWord)
        {
            var result = db.Accounts.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return 0; //wrong email
            }
            else
            {
                if (result.Password == passWord)
                {
                    if (result.Role == 1) return 1; //admin
                    else return -2; //customer
                }
                else
                {
                    return -1; //wrong password
                }
            }
        }

        public Account GetByEmail(string email)
        {
            return db.Accounts.SingleOrDefault(x => x.Email == email);
        }
    }
}