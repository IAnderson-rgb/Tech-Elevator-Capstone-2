﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }  
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenFromMonth { get; set; }
        public int OpenToMonth { get; set; }
        public decimal DailyFee { get; set; }

        public override string ToString()
        {
            return CampgroundId.ToString().PadRight(6) + ParkId.ToString().PadRight(6) + Name.PadRight(30) + OpenFromMonth.ToString().PadRight(30) + DailyFee.ToString().PadRight(10);
        }
    }

    
}
