using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ICampgroundDAO
    {
        /// <summary>
        /// Gets all campground provided a park Id.
        /// </summary>
        /// <param name="parkId">The park Id to search for.</param>
        /// <returns></returns>
        IList<Campground> GetCampgroundsByParkId(int parkId);

        /// <summary>
        /// Gets a campground from a campground Id.
        /// </summary>
        /// <param name="campgroundId">The campground Id to search for.</param>
        /// <returns></returns>
        //Campground GetCampgroundById(int campgroundId);

        
    }
}
