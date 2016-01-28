using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEngine.Data
{
    public class DbUnitOfWork : IUnitOfWork
    {

        BookingEngineContext _context;
        public DbUnitOfWork(BookingEngineContext context)
        {
            _context = context;
        }

        IExpertRepository _experts;
        public IExpertRepository Experts
        {
            get
            {
                return (_experts ?? (_experts = new ExpertDbRepository(_context)));
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

       

        public void Dispose()
        {
            _context.Dispose();
        }
      
    }
}
