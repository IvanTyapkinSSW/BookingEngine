using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingEngine.Data.Entities;

namespace BookingEngine.Data
{
    public class ExpertDbRepository : DbRepositoryBase<Expert>, IExpertRepository
    {
        public ExpertDbRepository(BookingEngineContext context)
            :base(context)
        {

        }
    }
}
