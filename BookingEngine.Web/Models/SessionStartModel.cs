using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingEngine.Web.Models
{
    public class SessionStartModel
    {
        public int SessionStartId { get; set; }
        public DateTime StartTimeLocal { get; set; }
        public string StartTimeFormatted { get; set; }
    }
}