using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BookingEngine.Data
{
    public interface IUnitOfWorkFactory 
    {
        IUnitOfWork Create();
    }
}
