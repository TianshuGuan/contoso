using ContosoDomain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContosoDAL.Repositories.Common
{
    public class UserRepository<T>:IUserRepository<T> where T : ApplicationUser
    {

        protected readonly IDbSet<T> _dbSet;
        protected ContosoDBContext _context;
        public UserRepository()
        {
            _context = ContosoDBContext.Create();
            _dbSet = _context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }


        public virtual IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public virtual IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<T> results = query.Where(predicate).ToList();
            return results;
        }


        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<U> GetBy<U>(Expression<Func<T, U>> columns, Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).Select(columns);
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var queryable = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(queryable, (current, property) => current.Include(property));
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public T GetByEmail(string email)
        {
            return _dbSet.Where(u => u.Email == email).FirstOrDefault();
        }

        public IEnumerable<T> GetByLastName(string name)
        {
            return _dbSet.Where(u => u.LastName == name);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }

    public interface IUserRepository<T>:IRepository<T> where T: ApplicationUser
    {
        T GetByEmail(string email);
        T GetById(string id);
        IEnumerable<T> GetByLastName(string name);
    }
}
