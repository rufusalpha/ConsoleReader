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
        private Dictionary<ulong, string> Logs = new Dictionary<ulong, string>();
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
                ulong counter = 0, id = 0;
                string desc = "";

                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        continue;
                    }

                    if (counter != 0 && line.Contains("(Filename: "))
                    {
                        //Entry temp = new Entry(tempIdenteifier, tempDescription);
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
            foreach (KeyValuePair<ulong, string> entry in Logs)
            {
                Console.WriteLine($"{entry.Key} \t{entry.Value}");
            }
        }
        public void PrintEntry(ulong index)
        {
            if (index < (ulong)Logs.Count)
                Console.WriteLine($"{index} \t{Logs[index]}");
            else
            {
                throw new ArgumentOutOfRangeException("PrintEntry(index)");
            }
        }
        public ulong Count()
        {
            return (ulong)Logs.Count;
        }
        public KeyValuePair<ulong, string> GetEntry(ulong index)
        {
            if (index < (ulong)Logs.Count)
            {
                return new KeyValuePair<ulong, string>(index, Logs[index]);    
            }
            else
            {
                throw new ArgumentOutOfRangeException("GetEntry(index)");
            }
        }
    }
}

