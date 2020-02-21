using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;


namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        /// <summary>
        /// Gets all available sites during provided time frame.
        /// </summary>
        /// <param name="campgroundId">The campground Id to search availible sites.</param>
        /// <returns></returns>
        IList<Site> GetAvailableSites(int campgroundId, string startDate, string endDate);

        /// <summary>
        /// Gets all available reservations provided a campground Id.
        /// </summary>
        /// <param name="campgroundId">The campground Id to search for.</param>
        /// <returns></returns>
        IList<Site> GetAvailableReservationsSingleCapmground(int campgroundId, string startDate, string endDate);

        /// <summary>
        /// Gets all available reservations at a selected park.
        /// </summary>
        /// <param name="parkSelected">The park to search.</param>
        /// <returns></returns>
        IList<Site> GetAvailableReservationsWholePark(Park parkSelected, string startDate, string endDate);


    }
}
