using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MoviePlexTheatre
{
    class Guest
    {

        public void GuestUser()
        {
            // making intance
            User u = new User();

            int requiredAge, enteredAge, selectedMovie;
            string age_or_rating = "";
            string doNext = "";
            string doNext1 = "";
            string tbl_count = "";

        MyLabelGuest:
            // defining connection string and making new sql connection
            string connectionString = "Data Source = DESKTOP-CFP5ITJ; Initial Catalog = movies_db; Integrated Security = True";
            using (SqlConnection dbCon1 = new SqlConnection(connectionString))//Create a connection
            {
                dbCon1.Open();//Open a connection


            MyLabelUnderAge:
                // defining sql query
                SqlCommand cmd2 = new SqlCommand(@"SELECT count(*) FROM movies_tbl", dbCon1);
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    // getting total no. of movies playing
                    while (reader.Read())
                    {
                        tbl_count = $"{ reader.GetValue(0)}";
                    }
                    reader.Close();
                }
                if (Convert.ToInt32(tbl_count) > 0)
                {

                    // giving cmd2 new query
                    cmd2.CommandText = "SELECT * FROM movies_tbl";
                    // executing query using reader
                    using (SqlDataReader reader = cmd2.ExecuteReader())
                    {
                        Console.WriteLine("There are {0} movies playing today. Please choose from the following movies: \n", tbl_count);
                        //Loop over the reader to get the lines
                        while (reader.Read())
                        {
                            string tbl_num = $"{ reader.GetValue(0)}";
                            string tbl_movie_name = $"{ reader.GetValue(1)}";
                            string tbl_age_or_rating = $"{ reader.GetValue(2)}";
                            // printing the movies list
                            Console.WriteLine("{0}. {1} {{{2}}}", tbl_num, tbl_movie_name, tbl_age_or_rating);
                        }

                    }

                    try
                    {

                        // getting movie no 
                        Console.Write("\nWhich Movie Would You Like To Watch: ");
                        selectedMovie = Convert.ToInt32(Console.ReadLine());
                        if (selectedMovie > 10 && selectedMovie <= 0)
                        {
                            // throw exception 
                            u.InValidEnteredValue();
                        }

                    }
                    // handling exception
                    catch (Exception)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nPlease select and Enter a valid Movie Number from Above Options\n");
                        Console.ResetColor();
                        goto MyLabelUnderAge;
                    }

                    // making new sql command

                    SqlCommand cmd3 = new SqlCommand("SELECT age_or_rating FROM movies_tbl where num = '" + selectedMovie + "'", dbCon1);
                    using (SqlDataReader reader1 = cmd3.ExecuteReader())
                    {
                        // getting age required for the selected movie
                        while (reader1.Read())
                        {
                            age_or_rating = $"{ reader1.GetValue(0)}";
                        }
                        // giving required age a int value based on rating with the help of switch case
                        switch (age_or_rating)
                        {
                            case "G":
                                requiredAge = 1;
                                break;
                            case "PG":
                                requiredAge = 10;
                                break;
                            case "PG-13":
                                requiredAge = 13;
                                break;
                            case "R":
                                requiredAge = 15;
                                break;
                            case "NC-17":
                                requiredAge = 17;
                                break;
                            default:
                                requiredAge = Convert.ToInt32(age_or_rating);
                                break;

                        }
                    }
                MyLabelInvalidtAge:
                        
                            try
                            {
                                Console.Write("Please Enter Your Age For Verfication: ");
                                enteredAge = Convert.ToInt32(Console.ReadLine());

                                if (enteredAge > 150 || enteredAge <= 0)
                                {
                            
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\nPlease enter an age between 1 to 100\n");
                                Console.ResetColor();
                                goto MyLabelInvalidtAge;
                                }

                            }
                            // handle exception
                            catch (Exception)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\n You Entered an invalid Age\n");
                                Console.ResetColor();
                MyLabelGoBack:       
                                Console.WriteLine("Press 'A' to try again OR 'B' to go back to Movies List");
                                string loc = Console.ReadLine();
                                if(loc == "A") 
                                {
                                    goto MyLabelInvalidtAge;
                                }
                                else if (loc == "B")
                                {
                                    goto MyLabelUnderAge;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("\nPlease Select a Valid Option\n");
                                    Console.ResetColor();
                                    goto MyLabelGoBack;       
                                }
                            }
                    
                    if (enteredAge >= requiredAge)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nEnjoy The Movie!");
                        Console.ResetColor();

                    MyLabeldoNext:




                        Console.Write("\nPress ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("M");
                        Console.ResetColor();
                        Console.WriteLine(" to go back to the Guest Main Menu.");
                        Console.Write("Press ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("S");
                        Console.ResetColor();
                        Console.WriteLine(" to go back to the Start Page");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        doNext = Console.ReadLine();
                        Console.ResetColor();

                        if (doNext == "M")
                        {
                            Console.WriteLine("");
                            goto MyLabelGuest;
                        }

                        else if (doNext == "S")
                        {
                            Console.WriteLine("");
                            u.CheckUser();
                        }

                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nPlease enter a Valid Action Option");
                            Console.ResetColor();
                            goto MyLabeldoNext;
                        }
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nSorry! But required age criteria does not match\n");
                        Console.ResetColor();
                        goto MyLabelUnderAge;
                    }
                }
                // if no movies are playing today
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nSorry! No movies are Playing Today\n");
                    Console.ResetColor();


        MyLabelNoMovies:
                    Console.Write("Press ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("S");
                    Console.ResetColor();
                    Console.WriteLine(" to go back to the Start Page");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    doNext1 = Console.ReadLine();
                    Console.ResetColor();


                    if (doNext1 == "S")
                    {
                        Console.WriteLine("");
                        u.CheckUser();
                    }

                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nPlease enter a Valid Action Option\n");
                        Console.ResetColor();
                        goto MyLabelNoMovies;
                    }


                }
            }
        }
    }
}
