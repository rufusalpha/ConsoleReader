using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Text;
using System.Data.SQLite;


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
        private SQLiteConnection Connection;
        
        public DataBase(string file)
        {
            if (file != "" && file != null)
                Connection = new SQLiteConnection("Data source=" + file);
            else
                throw new ArgumentException("DataBase(file) DataBase file cannot be null or empty");

            OpenConnection();
        }
        public DataBase()
        {
            Connection = new SQLiteConnection("Data source=database.db");
            
            OpenConnection();
        }

        private void OpenConnection()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("DataBase Connection could not be opened");
            }
            
            Console.WriteLine("DEBUG - Connection with file " + Connection.FileName + " OPEN");
        }
        public void CloseConnection()
        {
            Console.WriteLine("DEBUG - Connection with file " + Connection.FileName + " CLOSED");
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }


        public void CreateTable(string name, params string[] fields )
        {
            SQLiteCommand cmd = Connection.CreateCommand();
            string txt = "CREATE TABLE " + name + " (";

            if (fields.Length % 2==1)
                throw new ArgumentException("CREATE TABLE: Number of types does not match number of fileds");

            for (int i = 0; i < fields.Length; i+=2)
                txt += fields[i] + " " + fields[i + 1] + ",";
            
            txt = txt.Substring(0, txt.Length - 1) + ")";

            try
            {
                cmd.CommandText = txt;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new SQLiteException(e.Message);
            }
            
        }
        public void InsertInto(string table, int numberOfFields, params string[] values)
        {
            SQLiteCommand cmd = Connection.CreateCommand();
            string txt = "INSERT INTO " + table + " (";

            if (numberOfFields < values.Length && values.Length % numberOfFields == 0)
            {
                for (int i = 0; i < numberOfFields; i++)
                {
                    txt += values[i] + ",";
                }
                txt = txt.Substring(0, txt.Length - 1) + ") VALUES (";
                
                int counter = 0;
                for (int i = numberOfFields; i < values.Length; i++)
                {
                    txt += values[i];
                    if (++counter < numberOfFields)
                    {
                        txt += ",";
                    }
                    else
                    {
                        txt += "), (";
                        counter = 0;
                    }
                }
            
                txt = txt.Substring(0, txt.Length - 3);
                
                try
                {
                    Console.WriteLine(txt);
                    cmd.CommandText = txt;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new SQLiteException(e.Message);
                }
            }
            else
            {
                throw new ArgumentException("INSERT INTO: Number of fileds does not match number of values");
            }
            
            
        }

        public string ReadData(string table, string conditions, params string[] fields)
        {
            SQLiteCommand cmd = Connection.CreateCommand();
            SQLiteDataReader reader;
            string output = "";
                
            string txt = "SELECT ";

            if (fields.Length == 0)
            {
                txt += "*";
            }
            else
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    txt += fields[i] + ",";
                }

                txt = txt.Substring(0, txt.Length - 1);
            }

            txt = txt + " FROM " + table + " " + conditions;

            Console.WriteLine(txt);

            try
            {
                cmd.CommandText = txt;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    output += reader.GetString(0) + '\n';

                }

            }
            catch (Exception e)
            {
                throw new DataException(e.Message);
            }

            return output;
        }
    }
}

