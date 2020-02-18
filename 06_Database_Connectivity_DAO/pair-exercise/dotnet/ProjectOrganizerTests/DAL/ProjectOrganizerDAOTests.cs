using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizerTests.DAL;

namespace ProjectOrganizerTests.DAL
{
    [TestClass]
    public class ProjectOrganizerDAOTests
    {
        protected string ConnectionString; //{ get; } = "Server=.\\SQLEXPRESS;Database=World;Trusted_Connection=True;";

        /// <summary>
        /// Holds the newly generated city id.
        /// </summary>
        protected int NewEmployeeID { get; private set; }

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            var configuration = GetIConfigurationRoot(Environment.CurrentDirectory);
            //configuration = configuration;
            ConnectionString = configuration.GetConnectionString("EmployeeDB");
            // Begin the transaction    
            transaction = new TransactionScope();

            // Get the SQL Script to run
            string sql = File.ReadAllText("test-script.sql");

            // Execute the script
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // If there is a row to read
                if (reader.Read())
                {
                    this.NewEmployeeID = Convert.ToInt32(reader["newEmployeeId"]);
                }
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        protected int GetRowCount(string table)
        {
            int rows = 0;


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT count(*) from {table}", conn);
                rows = Convert.ToInt32(cmd.ExecuteScalar());

            }

            return rows;

        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

    }

}

