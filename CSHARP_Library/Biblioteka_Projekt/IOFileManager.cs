using System;
using System.Text;

namespace Biblioteka_Projekt
{
    static internal class IOFileManager
    {
        public static List<Reader> initReaderRegFromFileDB(string path)
        {
            if (!File.Exists(path)){
                Console.WriteLine("Can't find readers database (Readers_DB.txt)");
                return new List<Reader>();
            }
            else
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string[] lineArray = File.ReadAllLines(path);
                    int lineArrLen = lineArray.Length;

                    if (lineArrLen == 0)
                    {
                        Console.WriteLine("Readers_DB is empty");
                        return new List<Reader>();
                    }

                    List<Reader> tempReaderRegister = new List<Reader>();
                    for(int i = 1; i < lineArrLen; i+=MyConstants.readerParameters + 1)
                    {
                        string name, surname, phone;
                        DateTime dateOfBirth;
                        AddressStruct address;

                        name = dropReaderKeys(lineArray[i]);
                        surname = dropReaderKeys(lineArray[i + 1]);
                        dateOfBirth = MyTypeConverter.fromStringToDateTime(dropReaderKeys(lineArray[i + 2]));
                        address = MyTypeConverter.fromStringToAddress(dropReaderKeys(lineArray[i + 3]));
                        phone = dropReaderKeys(lineArray[i + 4]);

                        //Console.WriteLine($"{name}\n{surname}\n{dateOfBirth}\n{address}\n{phone}\n");

                        Reader newR = new Reader(name, surname, dateOfBirth, address, phone);
                        tempReaderRegister.Add(newR);
                    }

                    return tempReaderRegister;
                }
            }
        }

        public static void writeLogToFile(string logDirectory, string log){
            string dateTimeOfLog = DateTime.Now.ToString();
            File.AppendAllText(logDirectory, "\n" + dateTimeOfLog + ": " + log);
        }

        public static void writeReaderToFile(ReaderManager reader)
        {
            if (!File.Exists(reader.readers_db_path))
            {
                File.Create(reader.readers_db_path);
            }
            Reader? tempReader = reader.getLastReader();
            if (tempReader != null)
            {
                string tempStr = "";
                tempStr += "\nID: " + tempReader.m_ID + "\n";
                tempStr += "Name: " + tempReader.m_name + "\n";
                tempStr += "Surname: " + tempReader.m_surname + "\n";
                tempStr += "DateOfBirth: " + Convert.ToString(tempReader.m_dateOfBirth.Day) + 
                    "." + Convert.ToString(tempReader.m_dateOfBirth.Month) +
                    "." + Convert.ToString(tempReader.m_dateOfBirth.Year) + "\n";
                tempStr += "Address: " + tempReader.m_address.s_streetName +
                    ", " + tempReader.m_address.s_streetName +
                    ", " + tempReader.m_address.s_flatNumber + "\n";
                tempStr += "Phone number: " + tempReader.m_phoneNumber;

                File.AppendAllText(reader.readers_db_path, tempStr);
            }
        }

        private static string dropReaderKeys(string line) 
        { 
            string[] keyAndValue = line.Split(':');
            string value = keyAndValue[1].Substring(1);
            return value;
        }
    }
}
