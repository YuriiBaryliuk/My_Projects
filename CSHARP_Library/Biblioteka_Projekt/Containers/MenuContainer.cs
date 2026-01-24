// Container for static methods to print menu options
namespace Biblioteka_Projekt
{
    internal static class MenuContainer
    {
        private static string toMainMenu()
        {
            return "\nAny key: Return to main menu";
        }
        public static void printColumns(string[] columnNames)   // Prints all rows for table (passing table's columns)
        {
            for (int i = 0; i < columnNames.Length; ++i)
                Console.WriteLine($"{i + 1}. {columnNames[i]}");
        }

        //-------------Staff-------------
        public static void mainMenu_Staff()     // Show main menu for staff
        {
            Console.WriteLine("1. Add\n2. Delete\n3. Loan\n4. Receive\n5. See last\n6. Show\n7. Find\n8. Find by ID\n9. Calculate Arrears\n0. Exit");
        }
        public static void addOp_Staff()        // Show menu for add operation
        {
            Console.WriteLine("1. Add reader\n2. Add book" + toMainMenu());
        }
        public static void deleteOp_Staff()     // Show menu for delete a record operation
        {
            Console.WriteLine("1. Delete reader\n2. Delete book" + toMainMenu());
        }
        public static void seeLastOp_Staff()    // Show menu for see last record operation
        {
            Console.WriteLine("1. Added reader\n2. Added book" + toMainMenu());
        }
        public static void showOp_Staff()       // Show menu to choose which table to print on screen
        {
            Console.WriteLine($"1. {MyConstants.tableName_Reader}\n" +
                $"2. {MyConstants.tableName_Book}\n" +
                $"3. {MyConstants.tableName_Loans}\n" +
                $"4. {MyConstants.tableName_Receivings}\n" +
                $"5. {MyConstants.tableName_CurrentlyLoaned}\n" +
                $"6. {MyConstants.tableName_Genre}\n" +
                $"7. {MyConstants.tableName_Arrears}\n" +
                $"8. {MyConstants.tableName_Payments}" + toMainMenu());
        }
        public static void showWithOrder()      // Show menu options to choose in which order table will be displayed (asc/desc)
        {
            Console.WriteLine("1. Show with order (ascending)\n2. Show with order (descending)" + toMainMenu());
        }
        public static void findOp_Staff()       // Show menu to choose from which table user need to find a record
        {
            Console.WriteLine($"1. {MyConstants.tableName_Reader}\n" +
                $"2. {MyConstants.tableName_Book}\n" +
                $"3. {MyConstants.tableName_Loans}\n" +
                $"4. {MyConstants.tableName_Receivings}\n" +
                $"5. {MyConstants.tableName_Genre}\n" +
                $"6. {MyConstants.tableName_Payments}" + toMainMenu());
        }
        public static void printColumns_FindReader()    // Show menu to choose from which column user needs to find a value (table Reader)
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Reader[1]}\n" +
                $"2. {MyConstants.columnNames_Reader[2]}\n" +
                $"3. {MyConstants.columnNames_Reader[5]}\n" +
                $"4. {MyConstants.columnNames_Reader[6]}\n" +
                $"5. {MyConstants.columnNames_Reader[7]}\n" +
                $"6. {MyConstants.columnNames_Reader[8]}");
        }
        public static void printColumns_FindBook()      // Show menu to choose from which column user needs to find a value (table Book)
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Book[1]}\n" +
                $"2. {MyConstants.columnNames_Book[2]}\n" +
                $"3. {MyConstants.columnNames_Book[3]}\n" +
                $"4. {MyConstants.columnNames_Book[4]}\n" +
                $"5. {MyConstants.columnNames_Book[5]}");
        }
        public static void printColumns_FindLoans()     // Show menu to choose from which column user needs to find a value (table Loans)
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Loans[1]}\n" +
                $"2. {MyConstants.columnNames_Loans[2]}\n" +
                $"3. {MyConstants.columnNames_Loans[3]}");
        }
        public static void printColumns_FindReceivings()    // Show menu to choose from which column user needs to find a value (table Recievings)
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Recievings[1]}");
        }
        public static void printColumns_FindGenre()         // Show menu to choose from which column user needs to find a value (table Genres)
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Genre[1]}");
        }
        public static void printColumns_FindPayments()      // Show menu to choose from which column user needs to find a value (table Payments)
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Payments[1]}");
        }

        //-------------Manager-------------
        public static void mainMenu_Manager()   // Show main menu for manager
        {
            Console.WriteLine("1. Add staff\n2. Delete staff\n3. Delete All\n4. See last\n5. Show\n6. Find\n7. Find by ID\n0. Exit");
        }
        public static void addOp_Manager()      // Show menu to add staff member
        {
            Console.WriteLine("1. Add staff" + toMainMenu());
        }
        public static void deleteOp_Manager()   // Show menu to delete staff member
        {
            Console.WriteLine("1. Delete staff" + toMainMenu());
        }
        public static void deleteAll_Manager()  // Show menu to delete all records from tables (Reader, Book, Staff)
        {
            Console.WriteLine("1. Delete all Readers\n2. Delete all Books\nDelete all Staff members");
        }
        public static void seeLastOp_Manager()  // Show last added staff member
        {
            Console.WriteLine("1. Added staff member" + toMainMenu());
        }
        public static void printColumns_FindStaff() // Show columns to find a staff member using value
        {
            Console.WriteLine($"1. {MyConstants.columnNames_Staff[1]}\n" +
                $"2. {MyConstants.columnNames_Staff[2]}\n" +
                $"3. {MyConstants.columnNames_Staff[3]}");
        }

        //-------------Working with files
        public static void mainMenu_Base()          // Show main menu
        {
            Console.WriteLine("1. Add\n2. Delete All\n3. See last\n4. Show\n0. Exit");
        }
        public static void seeLastOp_Base()         // Show last added record
        {
            Console.WriteLine("1. See last Reader\n2. See last Book" + toMainMenu());
        }
        public static void showAllOp_Base()         // Show all records
        {
            Console.WriteLine("1. Show all Readers\n2. Show all books" + toMainMenu());
        }
    }
}
