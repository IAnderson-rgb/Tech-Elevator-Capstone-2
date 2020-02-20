using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string EstablishedDate{ get; set; }
        public int Area { get; set; }
        public int Vistiors{ get; set; }
        public string Description{ get; set; }

        public override string ToString()
        {
            return ParkId.ToString().PadRight(6) + Name.PadRight(30) + EstablishedDate.PadRight(30) + 
                Area.ToString().PadRight(10) + Vistiors.ToString().PadRight(10)+'\n'+Description;
        }
    }
}
