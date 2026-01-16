namespace Biblioteka_Projekt
{
    static internal class IOFileManager
    {
        //-----------------Readers-----------------//
        public static List<Reader> initReaderRegFromFileDB(string path)
        {
            if (!File.Exists(path)){
                Logs.writeLog("Can't find readers database (Readers_DB.txt)");
                return new List<Reader>();
            }
            try{
                string[] lineArray = File.ReadAllLines(path);
                if (lineArray.Length == 0)
                {
                    Logs.writeLog("Readers_DB is empty");
                    return new List<Reader>();
                }
                List<Reader> tempReaderRegister = new List<Reader>();
                int i = 1;
                while(i < lineArray.Length)
                {
                    string name, surname, phone, email;
                    char gender;
                    DateTime dateOfBirth;
                    Address address;
                    DateTime dateOfRegistration;

                    name = dropEntityKeys(lineArray[i++]);
                    surname = dropEntityKeys(lineArray[i++]);
                    gender = dropEntityKeys(lineArray[i++])[0];
                    dateOfBirth = DateTime.Parse(dropEntityKeys(lineArray[i++]));
                    address = Address.toAddress(dropEntityKeys(lineArray[i++]));
                    phone = dropEntityKeys(lineArray[i++]);
                    email = dropEntityKeys(lineArray[i++]);
                    dateOfRegistration = DateTime.Parse(dropEntityKeys(lineArray[i++]));
                    ++i;

                    Reader newR = new Reader(name, surname, gender, dateOfBirth, address, phone, email, dateOfRegistration);
                    tempReaderRegister.Add(newR);
                }
                return tempReaderRegister;
            }
            catch(Exception e)
            {
                Logs.writeLog(e.Message + " Readers were not initialized properly.");
                return new List<Reader>();
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
            tempStr += "Name: " + reader.m_name + "\n";
            tempStr += "Surname: " + reader.m_surname + "\n";
            tempStr += "Gender: " + reader.m_gender + "\n";
            tempStr += "Date Of Birth: " + reader.m_dateOfBirth.ToString("yyyy-MM-dd") + "\n";
            tempStr += "Address: " + reader.m_address.ToString() + "\n";
            tempStr += "Phone Number: " + reader.m_phoneNumber + "\n";
            tempStr += "Email Address: " + reader.m_email + "\n";
            tempStr += "Date Of Registration: " + reader.m_dateOfRegistration.ToString() + "\n";

            File.AppendAllText(path, tempStr);
        }

        //-----------------Books-----------------//

        public static List<Book> initBookRegFromFileDB(string path)
        {
            if (!File.Exists(path)){
                Logs.writeLog("Can't find books database (Books_DB.txt)");
                return new List<Book>();
            }
            try
            {
                string[] lineArray = File.ReadAllLines(path);
                if (lineArray.Length == 0)
                {
                    Logs.writeLog("Books_DB is empty");
                    return new List<Book>();
                }
                List<Book> tempBookRegister = new List<Book>();
                int i = 1;
                while(i < lineArray.Length)
                {
                    string author, title, description;
                    int yearOfRelease, genre;

                    author = dropEntityKeys(lineArray[i++]);
                    title = dropEntityKeys(lineArray[i++]);
                    yearOfRelease = Convert.ToInt32(dropEntityKeys(lineArray[i++]));
                    genre = Convert.ToInt32(dropEntityKeys(lineArray[i++]));
                    description = dropEntityKeys(lineArray[i++]);
                    ++i;

                    Book newB = new Book(author, title, yearOfRelease, genre, description);
                    tempBookRegister.Add(newB);
                }
                return tempBookRegister;
            }catch (Exception e)
            {
                Logs.writeLog(e.Message + " Books were not initialized properly.");
                return new List<Book>();
            }
        }

        public static void writeBookToFile(string path, Book book)
        {
            string tempStr = "";
            tempStr += "ID: " + book.m_ID + "\n";
            tempStr += "Author: " + book.m_author + "\n";
            tempStr += "Title: " + book.m_title + "\n";
            tempStr += "Date Of Release: " + book.m_yearOfRelease.ToString() + "\n";
            tempStr += "GenreID: " + book.m_genreID.ToString() + "\n";
            tempStr += "Description: " + book.m_description + "\n";

            File.AppendAllText(path, tempStr);
        }
        private static string dropEntityKeys(string line) 
        {
            string[] keyAndValue = line.Split(":", 2);
            string value = keyAndValue[1].Substring(1);
            return value;
        }

        //-------------SQL------------//

        public static bool readCommand(string fullResourceName, ref string command)
        {
            bool noException = true;
            
                try{
                var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName);
                StreamReader reader = new StreamReader(stream);
                command = reader.ReadToEnd();
                }catch(Exception ex)
                {
                    Logs.writeLog($"Can't read {fullResourceName}: {ex.Message}");
                    noException = false;
                }
            return noException;
        }
    }
}
