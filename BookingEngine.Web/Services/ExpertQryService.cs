using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingEngine.Data;
using BookingEngine.Data.Entities;
using BookingEngine.Web.Models;

namespace BookingEngine.Web.Services
{
    public class ExpertQryService
    {
        private IUnitOfWorkFactory _factory;

        public ExpertQryService()
        {

        }
        public ExpertQryService(IUnitOfWorkFactory f)
        {
            _factory = f;

        }

        public ExpertModel GetExpert(string code)
        {

            using (var ctx = _factory.Create())
            {
                var p = ctx.Experts.Get(n => n.Code == code).FirstOrDefault();

                if (p == null)
                    return null;

                ExpertModel pm = LoadModel(p);

                return pm;
            }
        }

        public ExpertModel GetExpertWithAvailability(string code, DateTime dateLocal, int timeZoneOffset)
        {
            using (var ctx = _factory.Create())
            {
                var p = ctx.Experts.Get(n => n.Code == code).FirstOrDefault();

                if (p == null)
                    return null;

                ExpertModel pm = LoadModel(p);

                pm.Availability = new List<AvailabilityModel>();

                if (dateLocal > DateTime.Today.AddDays(1) && dateLocal.DayOfWeek != DayOfWeek.Sunday && dateLocal.DayOfWeek != DayOfWeek.Saturday)
                {
                    var morn = new AvailabilityModel() { Duration = 4, StartDateTimeUtc = new DateTime(dateLocal.Year, dateLocal.Month, dateLocal.Day, 9, 0, 0), EndDateTimeUtc = new DateTime(dateLocal.Year, dateLocal.Month, dateLocal.Day, 13, 0, 0) };
                    var even = new AvailabilityModel() { Duration = 4, StartDateTimeUtc = new DateTime(dateLocal.Year, dateLocal.Month, dateLocal.Day, 14, 0, 0), EndDateTimeUtc = new DateTime(dateLocal.Year, dateLocal.Month, dateLocal.Day, 18, 0, 0) };
                    pm.Availability.Add(morn);
                    pm.Availability.Add(even);

                    int i = 0;
                    foreach (var a in pm.Availability)
                    {
                        a.SessionStarts = new List<SessionStartModel>();

                        for (DateTime dt = a.StartDateTimeUtc; dt < a.EndDateTimeUtc; dt = dt.AddHours(1))
                        {
                            i++;
                            a.SessionStarts.Add(new SessionStartModel()
                            {
                                SessionStartId = i,
                                StartDateTimeUtc = dt
                            });

                        }
                    }


                }

                return pm;
            }

        }
        public List<ExpertModel> ListExperts()
        {
            List<ExpertModel> model = new List<ExpertModel>();

            using (var ctx = _factory.Create())
            {
                var list = ctx.Experts.Get(null, n => n.OrderBy(e => e.FirstName), null);

                foreach (var p in list)
                {
                    ExpertModel pm = LoadModel(p);
                    model.Add(pm);
                }
            }

            return model;
        }

        public ExpertModel LoadModel(Expert p)
        {
            ExpertModel pm = new ExpertModel();
            pm.Id = p.ExpertId;
            pm.Code = p.Code;
            pm.FirstName = p.FirstName;
            pm.LastName = p.LastName;
            pm.Description = p.Description;
            pm.HourlyRate = p.DefaultRate;
            pm.HourlyRateFormatted = p.DefaultRate.ToString("$#.#");
            pm.ImageUrl = p.ImageUrl;
            return pm;
        }
    }
}