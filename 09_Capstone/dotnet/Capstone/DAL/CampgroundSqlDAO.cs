using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class CampgroundSqlDAO : ICampgroundDAO
    {
        private string connectionString;
        /// <summary>
        /// Creates a new sql-based city dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public CampgroundSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public IList<Campground> GetCampgroundByParkId(int parkId)
        {
            IList<Campground> campgrounds = new List<Campground>();

            return campgrounds;
        }

        public Campground GetCampgroundById(int campgroundId)
        {
            Campground campground = new Campground();


            return campground;
        }

    }
}
