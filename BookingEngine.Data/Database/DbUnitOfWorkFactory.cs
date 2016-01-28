using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEngine.Data
{
    public class DbUnitOfWorkFactory : IUnitOfWorkFactory
    {
        List<BookingEngineContext> _contextList;
        public DbUnitOfWorkFactory()
        {
            _contextList = new List<BookingEngineContext>();
        }

        public IUnitOfWork Create()
        {

            DbUnitOfWork ctx = new DbUnitOfWork(new BookingEngineContext());
 
            return ctx;
        }

       

    }
}
