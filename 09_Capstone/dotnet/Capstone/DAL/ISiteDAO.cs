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

     
    }
}
