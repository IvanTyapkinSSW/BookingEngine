using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookingEngine.Data.Entities;
using System.Linq.Expressions;

namespace BookingEngine.Data
{

    public class BookingEngineContext : DbContext
    {
        public BookingEngineContext() : base("BookingEngineContext")
        {
           
        }

        public BookingEngineContext(System.Data.Common.DbConnection cnn, bool contextOwnsConnection) 
            : base(cnn, contextOwnsConnection)
        {

        }
       
        public virtual DbSet<Expert> Experts { get; set; }
   
    }
}
