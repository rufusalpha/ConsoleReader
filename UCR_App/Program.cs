using System;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore.Storage;

namespace UCR_App
{
    internal class Application
    {
        static readonly string rootFile = Environment.ExpandEnvironmentVariables("%appdata%") + @"\..\LocalLow\Empyrean\House Flipper Game\Player.log";
        
        static void Main()
        {
            Console.WriteLine("SQLite test");
            DatabaseContext db = new DatabaseContext();
            
            Console.WriteLine(db.DbPath);
            
            db.Tests.Add(new Test { Desc = "test1" } );
            db.Tests.Add(new Test { Desc = "test2" } ); 
            db.SaveChanges();

            var query = db.Tests;
            foreach (var line in query)
            {
                Console.WriteLine($"{line.IdTest} {line.Desc}");
            }
        }
    }
}