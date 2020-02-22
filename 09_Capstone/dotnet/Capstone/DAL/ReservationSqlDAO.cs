using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {
        private string connectionString;
        /// <summary>
        /// Creates a new sql-based city dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public ReservationSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }



        public Reservation MakeAReservation(int siteId, string customerName, string startDate, string endDate)
        {
            Reservation reservations = new Reservation();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the command to create a reservation
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO reservation VALUES (@countrycode, @language, @isofficial, @percentage);
                                                    @@Identity
                                                    SELECT * FROM reservation WHERE reservation.reservation_id = @@Identity);", conn);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@countrycode", newLanguage.CountryCode);
                    cmd.Parameters.AddWithValue("@language", newLanguage.Name);
                    cmd.Parameters.AddWithValue("@isofficial", newLanguage.IsOfficial);
                    cmd.Parameters.AddWithValue("@percentage", newLanguage.Percentage);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Site site = new Site();
                        site.SiteId = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.MaxOccupancy = Convert.ToInt32(reader["site_occupancy"]);
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
            return reservations; 
        }
    }
}
