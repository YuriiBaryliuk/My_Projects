using System;
using System.Text;

namespace Biblioteka_Projekt
{
    static internal class IOFileManager
    {
        public static List<Reader> initReaderRegFromFileDB(string path)
        {
            if (!File.Exists(path)){
                Logs.writeLog("Can't find readers database (Readers_DB.txt)");
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
                        Logs.writeLog("Readers_DB is empty");
                        return new List<Reader>();
                    }
                    try{
                        List<Reader> tempReaderRegister = new List<Reader>();
                        int i = 1;
                        while(i < lineArrLen)
                        {
                            string name, surname, phone;
                            DateOnly dateOfBirth;
                            Address address;
                            DateTime dateOfRegistration;

                            name = dropReaderKeys(lineArray[i++]);
                            surname = dropReaderKeys(lineArray[i++]);
                            dateOfBirth = DateOnly.Parse(dropReaderKeys(lineArray[i++]));
                            address = Address.toAddress(dropReaderKeys(lineArray[i++]));
                            phone = dropReaderKeys(lineArray[i++]);
                            dateOfRegistration = DateTime.Parse(dropReaderKeys(lineArray[i++]));
                            ++i;

                            Reader newR = new Reader(name, surname, dateOfBirth, address, phone, dateOfRegistration);
                            tempReaderRegister.Add(newR);
                        }
                        return tempReaderRegister;
                    }
                    catch (Exception e)
                    {
                        Logs.writeLog(e.Message + " Readers were not initialized properly.");
                        return new List<Reader>();
                    }
                }
            }
        }

        public static void writeLogToFile(string logDirectory, string log){
            string dateTimeOfLog = DateTime.Now.ToString();
            File.AppendAllText(logDirectory, "\n" + dateTimeOfLog + ": " + log);
        }

        public static void writeReaderToFile(string path, Reader reader)
        {
            string tempStr = "";
            tempStr += "ID: " + reader.m_ID + "\n";
            tempStr += "Name: " + MyReformatting.firstLetterToUpper(reader.m_name) + "\n";
            tempStr += "Surname: " + MyReformatting.firstLetterToUpper(reader.m_surname) + "\n";
            tempStr += "Date Of Birth: " + reader.m_dateOfBirth.ToString("yyyy-MM-dd") + "\n";
            tempStr += "Address: " + reader.m_address.ToString() + "\n";
            tempStr += "Phone Number: " + MyReformatting.toPhoneNumberPL(reader.m_phoneNumber) + "\n";
            tempStr += "Date Of Registration: " + reader.m_dateOfRegistration.ToString() + "\n";

            File.AppendAllText(path, tempStr);
        }

        private static string dropReaderKeys(string line) 
        {
            string[] keyAndValue = line.Split(":", 2);
            string value = keyAndValue[1].Substring(1);
            return value;
        }
    }
}
