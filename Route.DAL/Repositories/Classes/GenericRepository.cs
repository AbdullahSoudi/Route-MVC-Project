using Route.DAL.Data.Contexts;
using Route.DAL.Models.DepartmentModule;
using Route.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.DAL.Repositories.Classes
{
    public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context = _context;
        private readonly DbSet<T> dbSet = _context.Set<T>();

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return dbSet.ToList();
            else
                return dbSet.AsNoTracking().ToList();
        }

        public T? GetById(int id)
        {
            return dbSet.Find(id);
        }

        public int Add(T entity)
        {
            dbSet.Add(entity);
            return context.SaveChanges();
        }

        public int Update(T entity)
        {
            dbSet.Update(entity);
            return context.SaveChanges();
        }

        public int Delete(T entity)
        {
            dbSet.Remove(entity);
            return context.SaveChanges();
        }
    }
}
