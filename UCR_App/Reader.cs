using System;

namespace UCR_App
{
    public readonly struct Entry
    {
        private string Identifier { get; }
        private string Description { get; }

        public Entry( string identifier, string description  )
        {
            Identifier = identifier;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Identifier}\n{Description}";
        }
    }

    public class Reader
    {
        private List<Entry> Logs;
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
                    if (line.Contains("(Filename: "))
                    {
                        Logs.Add(new Entry(tempIdenteifier, tempDescription));
                        counter = 0;
                    }
                    
                    if (counter == 0)
                    {
                        tempIdenteifier = line;
                    }
                    tempDescription += line;
                    counter++;
                }
            }
        }

        public Reader(string path, string separator)
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
                    if (line.Contains(separator))
                    {
                        Logs.Add(new Entry(tempIdenteifier, tempDescription));
                        counter = 0;
                    }
                    
                    if (counter == 0)
                    {
                        tempIdenteifier = line;
                    }
                    tempDescription += line;
                    counter++;
                }
            }
        }
    }
}

