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
        public IList<Campground> GetCampgroundsByParkId(int parkId)
        {
            IList<Campground> campgrounds = new List<Campground>();
            

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @park_id;", conn);
                    cmd.Parameters.AddWithValue("@park_id", parkId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        campgrounds.Add(ConvertReaderCampground(reader));
                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading campgrounds.");
                Console.WriteLine(ex.Message);
                throw;
            }

            
            return campgrounds;
        }

        private Campground ConvertReaderCampground(SqlDataReader reader)
        {
            Campground campground = new Campground();

            campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            campground.Name = Convert.ToString(reader["name"]);
            campground.OpenFromMonth = Convert.ToInt32(reader["open_from_mm"]);
            campground.OpenToMonth = Convert.ToInt32(reader["open_to_mm"]);
            campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
            return campground;
        }

        public Campground GetCampgroundById(int campgroundId)
        {
            Campground campground = new Campground();


            return campground;
        }

    }
}
