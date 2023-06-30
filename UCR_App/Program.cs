using System;
using System.Data.SQLite;

namespace UCR_App
{
    internal class Application
    {
        static readonly string rootFile = Environment.ExpandEnvironmentVariables("%appdata%") + @"\..\LocalLow\Empyrean\House Flipper Game\Player.log";
        
        static void Main()
        {
            Console.WriteLine("SQLite test");
            DataBase db = new DataBase();

            try
            {
                string txt = db.ReadData("aaa", "");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            



            db.CloseConnection();
        }
    }
}