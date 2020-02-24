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
            return ParkId.ToString()+") " + Name.PadRight(5)+" "+"National Park" +'\n'+ "Location:"+" "+ Location.PadRight(5) +'\n'+
               "Established:"+" "+ EstablishedDate.Substring(0,9).PadRight(5) +'\n'+ "Area:"+" "+ Area.ToString().PadRight(5) +'\n'+ "Annual Visitors:"+" "+ Vistiors.ToString().PadRight(5)+'\n'+'\n'+ Description;
        }
    }
}
