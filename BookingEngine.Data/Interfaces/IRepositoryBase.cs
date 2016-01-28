using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BookingEngine.Data
{
    public interface IRepositoryBase<T> where T : class
    {
        void Insert(T o);
        void Update(T o);
        void Delete(T o);

        List<T> Get(Expression<Func<T, bool>> where);
        List<T> Get(Expression<Func<T, bool>> where, string include);

        List<T> Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string include);
    }
}
