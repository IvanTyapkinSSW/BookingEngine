using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEngine.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IExpertRepository Experts { get; }
        int SaveChanges();

       
    }
}
