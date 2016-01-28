using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingEngine.Data;
using BookingEngine.Web.Models;
using BookingEngine.Web.Services;

namespace BookingEngine.Web.Controllers
{
    public class ExpertController : ApiController
    {

        ExpertQryService _expSvc;

        public ExpertController(ExpertQryService expSvc)
        {
            _expSvc = expSvc;

        }
        public ExpertController()
        {
            var factory = (IUnitOfWorkFactory)new SampleUnitOfWorkFactory();
            _expSvc = new ExpertQryService(factory);
        }

        // GET: api/Booking
        public IEnumerable<ExpertModel> Get()
        {
            return _expSvc.ListExperts();
        }
        public ExpertModel Get([FromUri]string id)
        {

            return _expSvc.GetExpert(id);
        }

        // GET: api/Booking/5
        public ExpertModel Get([FromUri]string id, DateTime date, int timeZoneOffset)
        {
           
            return _expSvc.GetExpertWithAvailability(id, date, timeZoneOffset);
        }

        // POST: api/Booking
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Booking/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Booking/5
        public void Delete(int id)
        {
        }
    }
}
