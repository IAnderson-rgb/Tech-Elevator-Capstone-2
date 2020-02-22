using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private string connectionString;
        /// <summary>
        /// Creates a new sql-based city dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public SiteSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public IList<Site> GetAvailableSites(int campgroundId, string startDate, string endDate)
        { 
        IList<Site> sites = new List<Site>();

            return sites;
        }

        /// <summary>
        /// Gets a list of available reservations given a park.
        /// </summary>
        /// <param name="campgroundId">campground ID from sql database</param>
        /// <param name="startDate">requested date reservation will begin</param>
        /// <param name="endDate">requested date reservation will end</param>
        public IList<Site> GetAvailableReservationsWholePark(Park parkSelected, string startDate, string endDate)
        {
            IList<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT site.site_id, site.campground_id, site.site_number, site.max_occupancy, site.accessible, site.max_rv_length, site.utilities 
                        FROM site
                        LEFT JOIN reservation on site.site_id = reservation.site_id
                        JOIN campground on site.campground_id = campground.campground_id
                        WHERE campground.park_id = @parkId AND campground.open_from_mm <= MONTH(@startDate) AND campground.open_to_mm >= MONTH(@endDate)

                        EXCEPT 

                        SELECT site.site_id, site.campground_id, site.site_number, site.max_occupancy, site.accessible, site.max_rv_length, site.utilities
                        FROM site
                        LEFT JOIN reservation on site.site_id = reservation.site_id
                        JOIN campground on site.campground_id = campground.campground_id
                        WHERE campground.park_id = @parkId AND reservation.from_date <= @endDate 
                        AND reservation.to_date >= @startDate;", conn);

                    cmd.Parameters.AddWithValue("@parkId", parkSelected.ParkId);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.SiteId = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting reservations.");
                Console.WriteLine(ex.Message);
                throw;
            }




            return sites;
        }

        /// <summary>
        /// Gets a list of available reservations given a single campground.
        /// </summary>
        /// <param name="campgroundId">campground ID from sql database</param>
        /// <param name="startDate">requested date reservation will begin</param>
        /// <param name="endDate">requested date reservation will end</param>
        public IList<Site> GetAvailableReservationsSingleCapmground(int campgroundId, string startDate, string endDate)
        {
            IList<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * 
                        FROM site
                        LEFT JOIN reservation on site.site_id = reservation.site_id
                        JOIN campground on site.campground_id = campground.campground_id
                        WHERE site.campground_id = @campgroundId AND campground.open_from_mm <= MONTH(@startDate) AND campground.open_to_mm >= MONTH(@endDate)

                        EXCEPT 

                        SELECT * 
                        FROM site
                        LEFT JOIN reservation on site.site_id = reservation.site_id
                        JOIN campground on site.campground_id = campground.campground_id
                        WHERE site.campground_id = @campgroundId AND reservation.from_date <= @endDate 
                        AND reservation.to_date >= @startDate;", conn);

                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Site site = new Site();
                        site.SiteId = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting reservations.");
                Console.WriteLine(ex.Message);
                throw;
            }




            return sites;
        }
    }
}
