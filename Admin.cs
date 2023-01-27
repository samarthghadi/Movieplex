using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MoviePlexTheatre
{   

    class Admin
    {
        public void addmovies()
        {
            // making instance of class user
            User u = new User();

            // declaring some variables
            int i, num, noOfMovies;
            int a = 0;
            string satisfied;
           
            // defining connection string and making new sql connection
            string connectionString = "Data Source = DESKTOP-CFP5ITJ; Initial Catalog = movies_db; Integrated Security = True";
            using (SqlConnection dbCon = new SqlConnection(connectionString))//Create a connection
            {

                dbCon.Open(); //Open a connection
                
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Welcome MoviePlex Administrator");
                Console.ResetColor();

            MyLabel_notsatisfied:  // making label

                // defining sql query/command
                SqlCommand cmd1 = new SqlCommand(@"Delete from movies_tbl", dbCon);
                cmd1.ExecuteNonQuery(); // executing the query


                try
                {
                    
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\nEnter how many movies are playing today? ");
                    Console.ResetColor();
     
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    // getting the total no. of movies playing today from admin
                    noOfMovies = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();

                    if (noOfMovies <= 10) // check if no of movies are in range .i.e, 1 - 10
                    {
                        // making 2D array of movies
                        string[,] movies = new string[noOfMovies, 2];
                        
                        // making array of ordinals
                        string[] ordinals = { "First", "Second", "Third", "Fourth", "Fifth", "Sixth",
                                                "Seventh", "Eigth", " Ninth", "tenth"};

            MyLabelInvalidCategory:
                        try 
                        {
                            // getting values input for movies
                            for (i = a; i < noOfMovies; i++)
                            {
                                
                                Console.Write("\nPlease Enter The {0} Movie's Name : ", ordinals[i]);
                                movies[i, 0] = (Console.ReadLine()).Trim();
                              
                                // CHECKING IF MOVIE NAME IS NOT NULL
                                if ((movies[i,0]).Trim() == "")
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("\nMovie name Cannot be Empty");
                                    Console.ResetColor();
                                    goto MyLabelInvalidCategory;
                                }
                               
                                else 
                                {
                                    Console.Write("Please Enter The Age Limit or Rating For {0} movie : ", ordinals[a]);
                                    movies[i, 1] = (Console.ReadLine()).Trim();
                                    // checking if entered age or rating is valid or not
                                    if (movies[i, 1] == "R" || movies[i, 1] == "G" || movies[i, 1] == "PG" || movies[i, 1] == "PG-13"
                                       || movies[i, 1] == "NC-17" || Convert.ToInt32(movies[i, 1]) > 0 && Convert.ToInt32(movies[i, 1]) <= 100)
                                    {
                                        num = i + 1;
                                        // inserting data into database
                                        cmd1.CommandText = "INSERT INTO movies_tbl(num,movie_name,age_or_rating) VALUES('" + num + "','" + movies[i, 0].Replace("'", "''") + "','" + movies[i, 1] + "') ";
                                        cmd1.ExecuteNonQuery(); // executing query
                                        a = a + 1;
                                    }
                                    else
                                    {
                                        // throw exception
                                        u.InValidEnteredValue();

                                    }
                                }
                            }
                        }

                        // exception handling
                        catch (Exception) 
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nPlease enter a valid Age or Rating\n");
                            Console.ResetColor();
                            goto MyLabelInvalidCategory;
                        }


        MyLabelsatisfiedException:

                        // printing the movies list
                        for (i = 0; i < noOfMovies; i++)
                        {
                            a = 0;
                            Console.WriteLine("{0}. {1} {{{2}}}", i + 1, movies[i, 0], movies[i, 1]);

                        }
       
                        try
                        {

                            Console.Write("\nYour Movies Playing Today Are Listed Above. Are You satisfied? (");
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Y");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("/");
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("N");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ResetColor();
                            Console.Write(")? ");
                            Console.ResetColor();

                            satisfied = Console.ReadLine();

                            if (satisfied == "Y" || satisfied == "y")
                            {
                                
                                Console.WriteLine("\n");
                                u.CheckUser();

                            }
                            else if (satisfied == "N" || satisfied == "n")
                            {
                                Console.WriteLine(""); // blank space
                                goto MyLabel_notsatisfied;
                                
                            }
                            else 
                            {  
                                // throw exception
                                u.InValidEnteredValue();
                            }
                        }
                        // handling exception
                        catch(Exception) 
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nPlease enter a option among Y/N or y/n Only\n");
                            Console.ResetColor();
                            goto MyLabelsatisfiedException;
                        }

                    }
                    else
                    {
                        // throw exception
                        u.InValidEnteredValue();
                    }
                }
                // handling exception
                catch(Exception) 
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Please enter no of movies playing in number format( Between 1 to 10 ) \n");
                    Console.ResetColor();
                    goto MyLabel_notsatisfied;

                }

            }
        }
    }
}
