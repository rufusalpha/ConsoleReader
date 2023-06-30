using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace UCR_App
{
    public struct Entry
    {
        public string Identifier;
        public string Description;

        public Entry( string identifier, string description )
        {
            Identifier = identifier;
            Description = description;
        }

        public override string ToString()
        {
            return $"ID: {Identifier}\n \tDesc: {Description}";
        }
    }
    
    public class Reader
    {
        private List<Entry> Logs = new List<Entry>();
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
                ulong counter = 0;
                string tempIdenteifier="", tempDescription="";
                
                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    if (counter != 0 && line.Contains("(Filename: "))
                    {
                        Entry temp = new Entry(tempIdenteifier, tempDescription);
                        Logs.Add(temp);
                        counter = 0;
                        tempIdenteifier = "";
                        tempDescription = "";
                        continue;
                    }
                    
                    if (counter == 0)
                    {
                        tempIdenteifier = line;
                    }
                    tempDescription += line + '\n';
                    counter++;
                }
            }
        }
        public void GetAll()
        {
            ulong i = 0;
            foreach (Entry entry in Logs)
            {
                Console.WriteLine( $"{i++}\t{entry.ToString()}");
            }
        }
    }
}

