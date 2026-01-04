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
            DateOnly dateOfBirth;
            Address address;
            string phoneNumber, email;

            Console.WriteLine("Enter Reader's data below");
            name = singleInputInit("name", InputCheck.checkPersonName);
            surname = singleInputInit("surname", InputCheck.checkPersonSurame);
            dateOfBirth = inputReaderDate();
            address = inputReaderAddress();
            phoneNumber = singleInputInit("phone", InputCheck.checkPersonPhone);
            email = singleInputInit("email", InputCheck.checkPersonEmail);

            Reader r = new Reader(name, surname, dateOfBirth, address, phoneNumber, email, DateTime.Now);
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
            string author, title, genre;
            int yearOfRelease;

            Console.WriteLine("Enter Book's data below");
            author = singleInputInit("author", InputCheck.checkAuthorName);
            title = initAndCheckIfNull("title");
            yearOfRelease = initBookYear();
            genre = initAndCheckIfNull("genre");

            Book b = new Book(author, title, yearOfRelease, genre);
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

        //private static string inputReaderName()
        //{
        //    string? name;
        //    while (true)
        //    {
        //        Console.Write("Enter a name: ");
        //        name = Console.ReadLine();

        //        if (string.IsNullOrEmpty(name))
        //        {
        //            Logs.writeLog("Entered Reader's name is empty or null");
        //            Console.WriteLine("Entered name is not valid, try again");
        //            continue;
        //        }
        //        else if(!InputCheck.checkPersonName(name)){
        //            Logs.writeLog("Entered Reader's name is not compatible with regular expression");
        //            Console.WriteLine("Entered name is not valid, try again");
        //            continue;
        //        }
        //        else
        //            break;
        //    }
        //    return name;
        //}

        //private static string inputReaderSurname()
        //{
        //    string? surname;
        //    while (true)
        //    {
        //        Console.Write("Enter a surname: ");
        //        surname = Console.ReadLine();

        //        if (string.IsNullOrEmpty(surname))
        //        {
        //            Logs.writeLog("Entered Reader's surname is empty or null");
        //            Console.WriteLine("Entered surname is not valid, try again");
        //            continue;
        //        }
        //        else if(!InputCheck.checkPersonName(surname)){
        //            Logs.writeLog("Entered Reader's surname is not compatible with regular expression");
        //            Console.WriteLine("Entered surname is not valid, try again");
        //            continue;
        //        }
        //        else
        //            break;
        //    }
        //    return surname;
        //}
        
        //private static string inputReaderPhone()
        //{
        //    string? phone;
        //    while (true)
        //    {
        //        Console.Write("Enter a phone: ");
        //        phone = Console.ReadLine();

        //        if (string.IsNullOrEmpty(phone))
        //        {
        //            Logs.writeLog("Entered Reader's phone is empty or null");
        //            Console.WriteLine("Entered phone is not valid, try again");
        //            continue;
        //        }
        //        else if(!InputCheck.checkPersonPhone(phone)){
        //            Logs.writeLog("Entered Reader's phone is not compatible with regular expression");
        //            Console.WriteLine("Entered phone is not valid, try again");
        //            continue;
        //        }
        //        else
        //            break;
        //    }
        //    return phone;
        //}

        //private static bool checkForNull(string userInput)
        //{
        //    if (string.IsNullOrEmpty(userInput))
        //    {
        //        Console.WriteLine("Input is null or empty");
        //        return false;
        //    }
        //    return true;
        //}
    }
}
