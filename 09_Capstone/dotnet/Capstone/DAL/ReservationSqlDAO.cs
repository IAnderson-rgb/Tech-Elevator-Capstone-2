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

        public IList<Reservation> GetAvailableReservationsWholePark(Park parkSelected, string startDate, string endDate) 
        {
            IList<Reservation> reservations = new List<Reservation>();




            return reservations;
        }

        /// <summary>
        /// Gets a list of available reservations given a single campground.
        /// </summary>
        /// <param name="campgroundId">campground ID from sql database</param>
        /// <param name="startDate">requested date reservation will begin</param>
        /// <param name="endDate">requested date reservation will end</param>
        public IList<Reservation> GetAvailableReservationsSingleCapmground(int campgroundId, string startDate, string endDate)
        {
            IList<Reservation> reservations = new List<Reservation>();
            //Get list of sites at single campground available during the given dates
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
                        
                        Reservation reservation = new Reservation();
                        reservation.ReservationId = Convert.ToInt32(reader["reservation_id"]);
                        reservation.SiteId = Convert.ToInt32(reader["site_id"]);
                        reservation.Name = Convert.ToString(reader["name"]);
                        reservation.FromDate = Convert.ToString(reader["from_date"]);
                        reservation.ToDate= Convert.ToString(reader["to_date"]);
                        reservation.CreateDate = DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                        reservations.Add(reservation);
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

        public Reservation MakeAReservation(int siteId, string customerName, string startDate, string endDate)
        {
            Reservation reservations = new Reservation();

            return reservations; 
        }
    }
}
