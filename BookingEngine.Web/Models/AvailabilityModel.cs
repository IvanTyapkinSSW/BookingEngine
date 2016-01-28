using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingEngine.Web.Models
{
    public class AvailabilityModel
    {
        public int BlockId { get; set; }
        public DateTime StartDateTimeUtc { get; set; }

        public DateTime EndDateTimeUtc { get; set; }

        public int TimeZoneOffset { get; set; }

        public string StartTimeLocal { get; set; }
        public string EndTimeLocal { get; set; }
        public decimal Duration { get; set; }
        public string DurationFormatted { get; set; }

        public List<SessionStartModel> SessionStarts { get; set; }
    }
}