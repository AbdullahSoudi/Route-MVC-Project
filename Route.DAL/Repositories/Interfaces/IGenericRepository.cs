using Route.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        int Add(T entity);
        int Delete(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        T? GetById(int id);
        int Update(T entity);
    }
}
