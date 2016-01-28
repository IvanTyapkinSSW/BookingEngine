using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEngine.Data
{
    public class SampleUnitOfWork : IUnitOfWork
    {

        IExpertRepository _experts;
        public IExpertRepository Experts
        {
            get
            {
                return (_experts ?? (_experts = new ExpertSampleRepository()));
            }
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {

        }
    }
}
