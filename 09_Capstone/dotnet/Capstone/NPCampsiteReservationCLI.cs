using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class NPCampsiteReservationCLI
    {
        private Park selectedPark = new Park();
        private  IParkDAO parkDAO;
        private ICampgroundDAO campgroundDAO;
        private ISiteDAO siteDAO;
        private IReservationDAO reservationDAO;
        public NPCampsiteReservationCLI(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;

        }

        public void RunCLI()
        {
            //PrintHeader();
            IList<Park> parks = ViewParksListMenu();
            int parkChoice = 0;

            while (true)
             {
                 string command = Console.ReadLine();

                if (char.ToLower(command[0]) == 'q') 
                {
                    return;
                }

                else if (int.TryParse(command, out parkChoice))
                {


                    if (parkChoice < 1 || parkChoice > parks.Count)
                    {
                        Console.WriteLine("Invalid selection, Please try again");

                    }
                    else
                    {
                        selectedPark = parks[parkChoice - 1];
                        ParksMenu(parks[parkChoice-1]);
                        Console.WriteLine(selectedPark.ToString());
                    }
                }

                else {
                    Console.WriteLine("Invalid Inuput");
                }

                ViewParksListMenu();
             }
        }

        private void ParksMenu(Park parkSelected)
        {
            const int Option_ViewCampgrounds = 1;
            const int Option_SearchForReservation = 2;
            const int Option_Return = 3;

            List<Reservation> reservationList = new List<Reservation>();

            while (true)
            {
                string command = Console.ReadLine();

                int commandSelected = -1;

                if (int.TryParse(command, out commandSelected))
                {

                    if (commandSelected > Option_Return)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numerical input.");
                    }

                    else if (commandSelected == Option_Return)
                    {
                        return;
                    }
                    else if (commandSelected == Option_SearchForReservation)
                    {
                        //reservationList = reservationDAO.GetAvailableReservations(parkSelected);

                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numerical input.");
                }




                return;
            }
        }

        private IList<Park> ViewParksListMenu()
        {
            IList<Park> parksList = parkDAO.GetAllParks();
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Select a Park for Further Details:");
            for(int i = 0; i < parksList.Count;i++)
            {
                Console.WriteLine($"{i+1}) {parksList[i].Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Q) - Quit");

            return parksList;
            
        }
    }
}
