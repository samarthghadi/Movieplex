using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePlexTheatre
{
    class User
    {
        // code that throws exception
        public void InValidEnteredValue()
        {
            throw new Exception();
        }

        public void CheckUser() 
        {
           

            int userSelection, i;
            string adminPassword = "admin";
            string enteredPassword = "";

            // making instances
            Admin a = new Admin();
            Guest g = new Guest();

            
        MyLabel_startapp:
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(String.Format("{0," + (Console.WindowWidth / 2 + 18) +"}", "************************************"));
            Console.WriteLine(String.Format("{0," + (Console.WindowWidth / 2 + 18) + "}","*** Welcome To MoviePlex Theatre ***"));
            Console.WriteLine(String.Format("{0," + (Console.WindowWidth / 2 + 18) + "}", "************************************"));
            Console.ResetColor();
           
        MyLabel_user:
            try
            {

                
                Console.WriteLine("Please Select From The Following Options:");
                Console.WriteLine("1: Administrator");
                Console.WriteLine("2: Guests");
                Console.WriteLine("\nSelection:");

                // getting user value
                userSelection = Convert.ToInt32(Console.ReadLine());

                // validating the user
                if (userSelection == 1)
                {
                     Console.WriteLine("Please Enter The Admin Password:");
                     enteredPassword = Console.ReadLine();
                    
                    if (enteredPassword == adminPassword)
                    {
                        a.addmovies();
                    }

                    else
                    {
                        // asking again to enter correct password when entered wrong password
                        for (i = 4; i >= 1; i--)

                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You entered an Invalid password.");
                            Console.ResetColor();
                            Console.WriteLine("\nYou have {0} more attempts to enter the correct password OR " +
                                                 "Press B to back to the previous screen.", i);

                            Console.WriteLine("Please Enter The Admin Password:");
                            enteredPassword = Console.ReadLine();

                            if (enteredPassword == adminPassword)
                            {
                                a.addmovies();

                            }
                            else if (enteredPassword == "B")
                            {
                                goto MyLabel_user;
                            }
                        }

                        goto MyLabel_startapp;

                    }
                }

                // checking if user is guest
                else if (userSelection == 2)
                {
                    // giving control to guest
                    g.GuestUser();
                }
                else
                {
                    // throw exception
                    InValidEnteredValue();
                }
            }
            // handle exception
            catch (Exception)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please Select A valid User Option Number\n");
                Console.ResetColor();
                goto MyLabel_user;
            }
        }
    }
}
