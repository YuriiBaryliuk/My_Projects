using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class InputManager
    {
        //-----------------Readers-----------------//
        public static void InputReader(ReaderManager rm)
        {
            string name, surname;
            char gender;
            DateOnly dateOfBirth;
            Address address;
            string phoneNumber, email;

            Console.WriteLine("Enter Reader's data below");
            name = singleInputInit("name", InputCheck.checkPersonName);
            surname = singleInputInit("surname", InputCheck.checkPersonSurame);
            gender = singleInputInit("gender (m/f)", InputCheck.checkPersonGender)[0];
            dateOfBirth = inputReaderDate();
            address = inputReaderAddress();
            phoneNumber = singleInputInit("phone", InputCheck.checkPersonPhone);
            email = singleInputInit("email", InputCheck.checkPersonEmail);

            Reader r = new Reader(name, surname, gender, dateOfBirth, address, phoneNumber, email, DateTime.Now);
            rm.addToReadersRegister(r);
            rm.loadReaderToFile(r);
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

        private static DateOnly inputReaderDate()
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
            return new DateOnly(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
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
            return new Address(city, streetName, houseNumber, flatNumber);
        }

        //-----------------Books-----------------//
        public static void InputBook(BookManager bm)
        {
            string author, title;
            int yearOfRelease, genreID;
            string? description;

            Console.WriteLine("Enter Book's data below");
            author = singleInputInit("author", InputCheck.checkAuthorName);
            title = initAndCheckIfNull("title");
            yearOfRelease = initBookYear();
            genreID = initGenre();
            Console.Write("Enter description: ");
            description = Console.ReadLine();
            Book b;
            if(string.IsNullOrEmpty(description))
                b = new Book(author, title, yearOfRelease, genreID);
            else
                b = new Book(author, title, yearOfRelease, genreID, description);
            bm.addToBooksRegister(b);
            bm.loadBookToFile(b);
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
    }
}
