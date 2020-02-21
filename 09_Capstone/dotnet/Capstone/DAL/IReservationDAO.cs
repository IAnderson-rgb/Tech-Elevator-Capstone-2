using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        

        /// <summary>
        /// Makes a reservation from a site Id and customer name.
        /// </summary>
        /// <param name="siteId">The site Id to search for.</param>
        /// <param name="customerName">The customer name.</param>
        /// <returns></returns>
        Reservation MakeAReservation(int siteId, string customerName,string startDate, string endDate);
    }
}
