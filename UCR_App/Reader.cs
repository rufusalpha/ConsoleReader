using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace UCR_App
{
    public class Reader
    {
        private Dictionary<int, string> Logs = new Dictionary<int, string>();
        private string FilePath { get; }

        public Reader(string path)
        {
            FilePath = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                throw new FileLoadException($"File not found or unable to be loaded: {FilePath}");
            }
            else
            {
                string[] lines = File.ReadAllLines(path);
                int counter = 0, id = 0;
                string desc = "";

                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        continue;
                    }

                    if (counter != 0 && line.Contains("(Filename: "))
                    {
                        Logs.Add(id++, desc);

                        counter = 0;
                        desc = "";
                        continue;
                    }

                    desc += line + '\n';
                    counter++;
                }
            }
        }

        public void PrintAll()
        {
            foreach (KeyValuePair<int, string> entry in Logs)
            {
                Console.WriteLine($"{entry.Key} \t{entry.Value}");
            }
        }
        public void PrintEntry(int index)
        {
            if (index < Logs.Count)
                Console.WriteLine($"{index} \t{Logs[index]}");
            else
            {
                throw new ArgumentOutOfRangeException("PrintEntry(index)");
            }
        }
        public int Count()
        {
            return Logs.Count;
        }
        public KeyValuePair<int, string> GetEntry(int index)
        {
            if (index < Logs.Count)
            {
                return new KeyValuePair<int, string>(index, Logs[index]);    
            }
            else
            {
                throw new ArgumentOutOfRangeException("GetEntry(index)");
            }
        }
    }

    public class DataBase
    {
        
    }
}

