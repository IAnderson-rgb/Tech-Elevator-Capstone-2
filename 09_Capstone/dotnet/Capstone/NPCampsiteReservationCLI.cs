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
                        parks[parkChoice - 1].ToString();
                        Console.WriteLine(selectedPark.ToString());
                        Console.WriteLine();
                        ParksMenu(parks[parkChoice-1]);
                        
                    }
                }

                else {
                    Console.WriteLine("Invalid Inuput");
                }

                ViewParksListMenu();
             }
        }

        /// <summary>
        /// Display Menu Options
        /// </summary>
        /// /// <param name="parkSelected">Park where reservation information can be gotten</param>
        private void ParksMenu(Park parkSelected)
        {
            const int Option_ViewCampgrounds = 1;
            const int Option_SearchForReservation = 2;
            const int Option_Return = 3;

            IList<Site> siteList = new List<Site>();

            while (true)
            {
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search Park for Reservation");
                Console.WriteLine("3) Return to previous menu");

                string command = Console.ReadLine();

                int commandSelected = -1;

                if (int.TryParse(command, out commandSelected))
                {

                    if (commandSelected > Option_Return)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numerical input.");
                    }

                    else if (commandSelected == Option_ViewCampgrounds)
                    {
                        return;
                    }
                    else if (commandSelected == Option_SearchForReservation)
                    {
                        string[] reservationDates = getReservationDatesFromUser();
                        
                        siteList = siteDAO.GetAvailableReservationsWholePark(parkSelected, reservationDates[0], reservationDates[1]);
                        
                        for (int i = 0; i < siteList.Count; i++) {
                            Console.WriteLine($"{i + 1}) " + siteList[i].ToString());    
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numerical input.");
                }




                return;
            }
        }

        /// <summary>
        /// Gets dates from user via CLI. Returns start date in position [0] and end date in position [1]
        /// </summary>
        private string[] getReservationDatesFromUser()
        {
            string[] reservationDates = new string[2];

            bool isDate = false;

            

            while (!isDate)
            {
                int year;
                int month;
                int day;



                Console.Write("What is the arrival Date YYYY-MM-DD: ");
                reservationDates[0] = Console.ReadLine();

                if (int.TryParse(reservationDates[0].Substring(0, 4), out year) && int.TryParse(reservationDates[0].Substring(5, 2), out month) && int.TryParse(reservationDates[0].Substring(8, 2), out day))
                {
                    isDate = ValidDate(year, month, day);//check date is in valid range
                }
                else 
                {
                    Console.WriteLine("Invalid date format. What is the arrival Date YYYY-MM-DD: ");
                }

                
                Console.Write("What is the departure Date YYYY-MM-DD: ");
                reservationDates[1] = Console.ReadLine();
                
                if (int.TryParse(reservationDates[1].Substring(0, 4), out year) && int.TryParse(reservationDates[1].Substring(5, 2), out month) && int.TryParse(reservationDates[1].Substring(8, 2), out day))
                {
                    isDate = ValidDate(year, month, day);//check date is in valid range
                }
                else
                {
                    Console.WriteLine("Invalid date format. What is the arrival Date YYYY-MM-DD: ");
                }
            }
            return reservationDates;
        }

        /// <summary>
        /// Check that date is within a year of today and that the month and day are make a valid date.
        /// </summary>
        private static bool ValidDate(int year, int month, int day)
        {
            bool isDate=false;
            if (year < DateTime.Today.Year || year > DateTime.Today.Year + 1)
            {
                Console.WriteLine("Invalid year. Please enter a year no more than one year in the future.");
            }
            else if (month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month. Please enter a number 1-12 as the month.");
            }
            else if (day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                Console.WriteLine("Invalid number of days. Please enter a valid number for days.");
            }

            else { isDate = true; }

            return isDate;
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
