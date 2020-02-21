using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        /// <summary>
        /// Gets all available reservations provided a campground Id.
        /// </summary>
        /// <param name="campgroundId">The campground Id to search for.</param>
        /// <returns></returns>
        IList<Reservation> GetAvailableReservationsSingleCapmground(int campgroundId, string startDate, string endDate);

        /// <summary>
        /// Gets all available reservations at a selected park.
        /// </summary>
        /// <param name="parkSelected">The park to search.</param>
        /// <returns></returns>
        IList<Reservation> GetAvailableReservationsWholePark(Park parkSelected, string startDate, string endDate);

        /// <summary>
        /// Makes a reservation from a site Id and customer name.
        /// </summary>
        /// <param name="siteId">The site Id to search for.</param>
        /// <param name="customerName">The customer name.</param>
        /// <returns></returns>
        Reservation MakeAReservation(int siteId, string customerName,string startDate, string endDate);
    }
}
