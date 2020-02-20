using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IParkDAO
    {
           
        /// <summary>
        /// Gets all parks.
        /// </summary>
        /// <returns></returns>
        IList<Park> GetAllParks();

        /// <summary>
        /// Gets a park provided a park Id.
        /// </summary>
        /// <param name="parkId">The park Id to search for.</param>
        /// <returns></returns>
        Park GetParkByParkId(int parkId);
    }
}
