using System;

namespace UCR_App
{
    internal class Application
    {
        static readonly string rootFile = Environment.ExpandEnvironmentVariables("%appdata%") + @"\..\LocalLow\Empyrean\House Flipper Game\Player.log";
        
        static void Main()
        {
            var path = Path.GetFullPath(rootFile);
            Console.WriteLine("Hello, World :D \n{0}\n{1}",rootFile,path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Not Workie ;(");
            }
            else
            {
                Console.WriteLine("Press ENTER");
                Console.ReadLine();
                string[] lines = File.ReadAllLines(path);
                ulong counter = 0;
                foreach( string line in lines )
                {
                    
                    if (!(line == "" || line.Contains("(Filename: ")))
                        Console.WriteLine("{0}\t{1}", counter++.ToString(), line);
                    
                }
            }
            
        }
    }
}