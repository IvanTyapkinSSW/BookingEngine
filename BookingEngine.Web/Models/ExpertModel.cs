using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingEngine.Web.Models
{
    public class ExpertModel
    {
        public ExpertModel()
        { }

        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public decimal HourlyRate { get; set; }
        public string HourlyRateFormatted { get; set; }

        public string ImageUrl { get; set; }

        public List<AvailabilityModel> Availability { get; set; }
    }
}