using System;

namespace UCR_App
{
    internal class Application
    {
        static readonly string rootFile = Environment.ExpandEnvironmentVariables("%appdata%") + @"\..\LocalLow\Empyrean\House Flipper Game\Player.log";
        
        static void Main()
        {
            try
            {
                Reader console = new Reader(rootFile);
                KeyValuePair<int, string> entry;
                
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
                
                console.PrintAll(); 
                console.PrintEntry(3);
                entry = console.GetEntry(3);
                Console.WriteLine($"{entry.Key} \t{entry.Value}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}