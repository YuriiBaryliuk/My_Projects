using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class InputManager
    {
        //-----------------Readers-----------------//
        public static Reader InputReader()
        {
            string name, surname;
            char gender;
            DateTime dateOfBirth;
            Address address;
            string phoneNumber, email;

            Console.WriteLine("Enter Reader's data below");
            name = MyReformatting.firstLetterToUpper(singleInputInit("name", InputCheck.checkPersonName));
            surname = MyReformatting.firstLetterToUpper(singleInputInit("surname", InputCheck.checkPersonSurame));
            gender = char.ToUpper(singleInputInit("gender (m/f)", InputCheck.checkPersonGender)[0]);
            dateOfBirth = inputReaderDate();
            address = inputReaderAddress();
            phoneNumber = MyReformatting.toPhoneNumberPL(singleInputInit("phone", InputCheck.checkPersonPhone));
            email = singleInputInit("email", InputCheck.checkPersonEmail);

            return new Reader(name, surname, gender, dateOfBirth, address, phoneNumber, email, DateTime.Now);
        }
        private static string singleInputInit(string parameterName, Predicate<string> initValue)
        {
            string param;
            while(true){
            param = initAndCheckIfNull(parameterName);
                if(!initValue(param))
                {
                    Logs.writeLog($"Entered item: \"{parameterName}\" is not compatible with regular expression");
                    Console.WriteLine($"Entered {parameterName} is not valid, try again");
                    continue;
                }
                break;
            }
            return param;
        }

        private static string initAndCheckIfNull(string parameterName)
        {
            string? param;
            while (true)
            {
                Console.Write($"Enter a {parameterName}: ");
                param = Console.ReadLine();

                if (string.IsNullOrEmpty(param))
                {
                    Logs.writeLog($"Entered item: \"{parameterName}\" is empty or null");
                    Console.WriteLine($"Entered {parameterName} is not valid, try again");
                    continue;
                }
                break;
            }
            return param;
        }

        private static DateTime inputReaderDate()
        {
            string? day, month, year;
            while (true)
            {
                Console.WriteLine("Write a date of birth below");
                Console.Write("Day: ");
                day = Console.ReadLine();
                Console.Write("Month: ");
                month = Console.ReadLine();
                Console.Write("Year: ");
                year = Console.ReadLine();

                if (string.IsNullOrEmpty(day) || string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year))
                {
                    Logs.writeLog("Entered Reader's date of birth is empty or null");
                    Console.WriteLine("Entered date of birh is not valid, try again");
                    continue;
                }
                else if (!InputCheck.checkDate(year, month, day))
                {
                    Logs.writeLog("Entered Reader's date of birth is not compatible with regular expression");
                    Console.WriteLine("Entered date of birth is not valid, try again");
                    continue;
                }
                else
                    break;
            }
            return new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
        }
       
        private static Address inputReaderAddress()
        {
            string? streetName, houseNumber, flatNumber, city;
            while (true)
            {
                Console.WriteLine("Write an address below");
                Console.Write("Street Name: ");
                streetName = Console.ReadLine();
                Console.Write("House Number: ");
                houseNumber = Console.ReadLine();
                Console.Write("Flat Number: ");
                flatNumber = Console.ReadLine();
                Console.Write("City:");
                city = Console.ReadLine();

                if (string.IsNullOrEmpty(streetName) || string.IsNullOrEmpty(houseNumber) || string.IsNullOrEmpty(flatNumber) || string.IsNullOrEmpty(city))
                {
                    Logs.writeLog("Entered Reader's address is empty or null");
                    Console.WriteLine("Entered address is not valid, try again");
                    continue;
                }
                else if (!InputCheck.checkPersonAddress(streetName, houseNumber, flatNumber))
                {
                    Logs.writeLog("Entered Reader's address is not compatible with regular expression");
                    Console.WriteLine("Entered address is not valid, try again");
                    continue;
                }
                else
                    break;
            }
            return new Address(MyReformatting.firstLetterToUpper(city), MyReformatting.firstLetterToUpper(streetName), MyReformatting.firstLetterToUpper(houseNumber), MyReformatting.firstLetterToUpper(flatNumber));
        }

        //-----------------Books-----------------//
        public static Book InputBook()
        {
            string author, title;
            int yearOfRelease, genreID;
            string? description;

            Console.WriteLine("Enter Book's data below");
            author = MyReformatting.firstLetterToUpper(singleInputInit("author", InputCheck.checkAuthorName));
            title = MyReformatting.firstLetterToUpper(initAndCheckIfNull("title"));
            yearOfRelease = initBookYear();
            genreID = initGenre();
            Console.Write("Enter description: ");
            description = Console.ReadLine();
            if(string.IsNullOrEmpty(description))
                return new Book(author, title, yearOfRelease, genreID);
            else{
                MyReformatting.firstLetterToUpper(description);
                return new Book(author, title, yearOfRelease, genreID, description);
            }
        }

        private static int initBookYear()
        {
            string bookYear;
            const string parameterName = "Year of book release";
            while(true){
            bookYear = initAndCheckIfNull(parameterName);
            
                if(!InputCheck.checkBookYear(bookYear))
                {
                    Logs.writeLog($"Entered item: \"{parameterName}\" is not compatible with regular expression");
                    Console.WriteLine($"Entered {parameterName} is not valid, try again");
                    continue;
                }
                break;
            }
            return Convert.ToInt32(bookYear);
        }

        private static int initGenre()
        {
            string genreID;
            const string parameterName = "Genre ID";
            while(true){
            genreID = initAndCheckIfNull(parameterName);

                if(!InputCheck.checkBookGenre(genreID))
                {
                    Logs.writeLog($"Entered item: \"{parameterName}\" is not compatible with regular expression");
                    Console.WriteLine($"Entered {parameterName} is not valid, try again");
                    continue;
                }
                break;
            }
            return Convert.ToInt32(genreID);
        }

        //--------------------SQL--------------------------

        public static Dictionary<string, int> loanBook()
        {
            Dictionary<string, int> loanDict = new Dictionary<string, int>();
            int reader_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Reader id"));
            int staff_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Your id"));
            int book_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Book id"));

            loanDict.Add("Reader_id", reader_id);
            loanDict.Add("Staff_id", staff_id);
            loanDict.Add("Book_id", book_id);

            return loanDict;
        }

        public static Dictionary<string, int> receiveBook()
        {
            Dictionary<string, int> recDict = new Dictionary<string, int>();
            int loan_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Loan id"));
            int staff_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Your id"));

            recDict.Add("Loan_id", loan_id);
            recDict.Add("Staff_id", staff_id);

            return recDict;
        }
    }
}
