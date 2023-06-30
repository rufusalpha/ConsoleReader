using System;

namespace UCR_App
{
    internal class Application
    {
        static readonly string rootFile = Environment.ExpandEnvironmentVariables("%appdata%") + @"\..\LocalLow\Empyrean\House Flipper Game\Player.log";
        
        static void Main()
        {
            Console.WriteLine("SQLite test");
            
            
            
        }
    }
}