using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingEngine.Data.Entities
{
    public class Expert
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpertId { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Description { get; set; }

        public decimal DefaultRate { get; set; }
        public string ImageUrl { get; set; }
      
    }
}
