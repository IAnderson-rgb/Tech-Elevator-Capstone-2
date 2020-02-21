using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
        
        //Console.WriteLine(DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
        // Get the connection string from the appsettings.json file
        IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("npcampground");


            IParkDAO parkDAO = new ParkSqlDAO(connectionString);
            ICampgroundDAO campgroundDAO = new CampgroundSqlDAO(connectionString);
            ISiteDAO siteDAO = new SiteSqlDAO(connectionString);
            IReservationDAO reservationDAO = new ReservationSqlDAO(connectionString);

            NPCampsiteReservationCLI cli = new NPCampsiteReservationCLI(parkDAO, campgroundDAO, siteDAO, reservationDAO);
            cli.RunCLI();
        }
    }
}
