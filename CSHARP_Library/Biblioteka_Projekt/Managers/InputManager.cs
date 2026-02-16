// This manager is responsible for user's input
// It provides all necessary methods for input
// Checks input directing it to appropriate instance
using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class InputManager
    {
        //-----------------Readers-----------------//
        // Method provides opportunity to enter Reader info and returning creating object (Reader)
        public static Reader InputReader()
        {
            // 1. All necessary variables (needed to create Reader object) are created
            // 2. Each of them is initialized. Initialization can differ:
                
                // - name, surname, phone (description/used class):
                    // (1) Checking user input untill it will be right / InputCheck
                    // (2) Using universal method (singleInputInit) - provide realization of user input / InputManager
                    // (3) After correct input each is reformatted into appropriate form (e.g. first letter of name is converted to upper case) / MyReformatting
                // - gender is initialized the same, except 3-rd step - its using char.ToUpper instead
                // - address, dateOfBirth are using custom methods defined inside in InputManager

            // 3. Returning Reader object created from listed variables

           
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
        
        // Univarsal method that is used to provide user's input functionality
        // Parameters to pass:
            // (1) String type - parameterName is the name of parameter is using for messages
            // (2) Predicate<string> checking if the input is right
        // Method working that way:
            // - Using universal method (initAndCheckIfNull) initialize user's input
            // - Check if value can pass using predicate
            // - If user input is inappropriate - try again (realized using while loop)
            // - If user input is appropriate - return inputted data
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

        // Universal method that is used to return non-null user input
        // Parameter is the name of what user need to provide (using for massages)
        // User will be asked to write data until it will be not empty or not null
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

        // Method to input user's date of birth
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
        
        // Method to input user's address
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
        
        // Method provides opportunity to enter Book info and returning creating object (Reader)
        public static Book InputBook()
        {
        // 1. All necessary variables (needed to create Book object) are created
        // 2. Each of them is initialized. Initialization can differ:
                
            // - author, title (description/used class):
                // (1) Checking user input untill it will be right / InputCheck
                // (2) Using universal method (singleInputInit) - provide realization of user input / InputManager
                // (3) After correct input each is reformatted into appropriate form (e.g. first letter of title is converted to upper case) / MyReformatting
            // - genre, yearOfRelease are using custom methods defined inside in InputManager
            // - description initialized directly in this method

        // 3. Returning Book object created from listed variables (including description if it is not null)
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

        // Method to input user's date of birth
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

        // Method to input user's genre (ID)
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

        //--------------------STAFF------------------------
        
        // Method provides opportunity to enter Staff info and returning creating object (Staff)
        public static Staff inputStaff()
        {
        // 1. All necessary variables (needed to create Staff object) are created
        // 2. Each of them is initialized:
                
            // - name, surname, title (description/used class):
                // (1) Checking user input untill it will be right / InputCheck
                // (2) Using universal method (singleInputInit) - provide realization of user input / InputManager
                // (3) After correct input each is reformatted into appropriate form (e.g. first letter of title is converted to upper case) / MyReformatting
            
        // 3. Returning Staff object created from listed variables
            string name, surname, title;
            
            Console.WriteLine("Enter Reader's data below");
            name = MyReformatting.firstLetterToUpper(singleInputInit("name", InputCheck.checkPersonName));
            surname = MyReformatting.firstLetterToUpper(singleInputInit("surname", InputCheck.checkPersonSurame));
            title = MyReformatting.firstLetterToUpper(singleInputInit("title", InputCheck.checkStaffTitle));

            return new Staff(name, surname, title);
        }

        //--------------------SQL--------------------------

        // Method is used to provide functionality of inputs for loaning a book
        // Returns dictionary with appropriate entered id's
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

        // Method is used to provide functionality of inputs for receiving a book
        // Returns dictionary with appropriate entered id's
        public static Dictionary<string, int> receiveBook()
        {
            Dictionary<string, int> recDict = new Dictionary<string, int>();
            int loan_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Loan id"));
            int staff_id = InputCheck.checkIfNum(InputManager.initAndCheckIfNull("Your id"));

            recDict.Add("Loan_id", loan_id);
            recDict.Add("Staff_id", staff_id);

            return recDict;
        }

        // Supportive method that returns ID (or any number) if user's input is correct
        // Else returns zero
        public static int enterNum(string name = "ID")
        {
            string ID = initAndCheckIfNull(name);
            return InputCheck.checkIfNum(ID);
        }

        // Supportive method that is used to input (and checking it) value, that user needs to find in database
        public static string enterStringFindTarget()
        {
            string value = initAndCheckIfNull("Value to find");
            return value;
        }
    }
}
