using System.Collections.Generic;

namespace Models.DAO
{
    public interface IRepository<T>
    {
        IEnumerable<T> ListAllPaging(string searchString, int page, int pageSize);

        T GetById(int id);

        bool Create(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}