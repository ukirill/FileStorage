using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Repositories
{
    public interface IBaseRepository<T>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> GetAll();

        //IQueryable<T> GetById()
    }
}
