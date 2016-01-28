using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;

namespace BookingEngine.Data
{
    public class DbRepositoryBase<T> where T : class
    {
        protected BookingEngineContext _context;
        public DbRepositoryBase(BookingEngineContext context)
        {
            _context = context;
        }
        public void Insert(T o)
        {
            _context.Set<T>().Add(o);
        }
        public void Update(T o)
        {
            _context.Entry(o).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(T o)
        {
            _context.Entry(o).State = System.Data.Entity.EntityState.Deleted;
        }

        public List<T> Get(Expression<Func<T, bool>> where)
        {
            IQueryable<T> query = _context.Set<T>();

            if (where != null)
            {
                query = query.Where(where);
            }

            return query.ToList();
        }
        public List<T> Get(Expression<Func<T, bool>> where, string include)
        {
            IQueryable<T> query = _context.Set<T>();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                foreach (var includeProperty in include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public List<T> Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string include)
        {

            IQueryable<T> query = _context.Set<T>();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            if (include != null)
            {
                foreach (var includeProperty in include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
    }
}
