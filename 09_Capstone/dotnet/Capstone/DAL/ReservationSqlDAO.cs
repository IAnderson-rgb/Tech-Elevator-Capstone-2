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



        public Reservation MakeAReservation(Site site, string customerName, string startDate, string endDate)
        {
            int newReservationId = -1;//-1 for non value
            Reservation reservations = new Reservation();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the command to create a reservation
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO reservation (site_id, name, from_date, to_date, create_date)
                    VALUES (@siteNumber, @customerName, @beginDate, @endDate, GETDATE());
                    SELECT reservation_id FROM reservation WHERE reservation.reservation_id = SCOPE_IDENTITY();", conn);
                    cmd.Parameters.AddWithValue("@siteNumber", site.SiteNumber);
                    cmd.Parameters.AddWithValue("@beginDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@customerName", customerName);


                    //SELECT reservation_id FROM reservation WHERE reservation.reservation_id = SCOPE_IDENTITY()
                    //newReservationId = cmd.ExecuteScalar();
                    newReservationId = Convert.ToInt32(cmd.ExecuteScalar());

                    Console.WriteLine("The reservation has been made and the confirmation id is " + newReservationId);

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
