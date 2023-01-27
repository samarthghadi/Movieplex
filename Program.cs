using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MoviePlexTheatre
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Giving title to console
            Console.Title = "MoviePlex Theatre Application By ANKIT TAYAL and SAMARTH GHADI";
            
            // making instance of the class 
            User u = new User();
            
            // calling method of class user
            u.CheckUser();
            Console.ReadLine();
        }
                    
        
    }
}

