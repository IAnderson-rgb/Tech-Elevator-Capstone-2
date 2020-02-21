using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int? ReservationId { get; set; }
        public int? SiteId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CreateDate { get; set; }

        public override string ToString()
        {
            return ReservationId.ToString().PadRight(6) + SiteId.ToString().PadRight(6) + Name.PadRight(30) + FromDate.PadRight(30) + ToDate.PadRight(30) + CreateDate.PadRight(30);
        }
    }
}
