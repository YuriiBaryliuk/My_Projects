// Main menu created for Staff members of the library
// It includes options that Staff can use
namespace Biblioteka_Projekt
{
    internal class StaffMenu : ISQLMenu
    {
        // Has SQLManager as a member
        public SQLManager sqlManager { get; private set; }

        // Initialize using SQLManager
        public StaffMenu(SQLManager sqlManager)
        {
            this.sqlManager = sqlManager;
        }

        // Main menu method implementation for all options to choose
        public void MainMenu()
        {
            Console.WriteLine("-------------Main Menu-------------");
            MenuContainer.mainMenu_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Add");
                    Add();
                    break;
                case "2":
                    Console.WriteLine("Delete");
                    Delete();
                    break;
                case "3":
                    Console.WriteLine("Loan");
                    Loan();
                    break;
                case "4":
                    Console.WriteLine("Receive");
                    Receive();
                    break;
                case "5":
                    Console.WriteLine("See last");
                    SeeLast();
                    break;
                case "6":
                    Console.WriteLine("Show");
                    Show();
                    break;
                case "7":
                    Console.WriteLine("Find");
                    Find();
                    break;
                case "8":
                    Console.WriteLine("Find by ID");
                    FindByID();
                    break;
                case "9":
                    Console.WriteLine("Calculate arrears");
                    CalculateArrears();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
            MainMenu();
        }

        // Add option implementation for adding objects into table of SQL database
        public void Add()
        {
            MenuContainer.addOp_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Add reader");
                    this.sqlManager.inputAndSave<Reader>();
                    break;
                case "2":
                    Console.WriteLine("Add book");
                    this.sqlManager.inputAndSave<Book>();
                    break;
                default:
                    break;
            }
        }

        // Menu for deleting single record
        public void Delete()
        {
            MenuContainer.deleteOp_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Delete reader");
                    this.sqlManager.deleteRecord<Reader>(InputManager.enterNum());
                    break;
                case "2":
                    Console.WriteLine("Delete book");
                    this.sqlManager.deleteRecord<Book>(InputManager.enterNum());
                    break;
                default:
                    break;
            }
        }

        // Inner method that launches loan book functionality
        private void Loan()
        {
            this.sqlManager.loanBook();
        }

        // Inner method that launches receiving book functionality
        private void Receive()
        {
            this.sqlManager.ReceiveBook();
        }

        // See last added member option implementation to print information about last added object
        public void SeeLast()
        {
            MenuContainer.seeLastOp_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("See last reader");
                    this.sqlManager.findRecordByID(MyConstants.tableName_Reader, MyConstants.columnNames_Reader, this.sqlManager.getLastId<Reader>());
                    break;
                case "2":
                    Console.WriteLine("See last book");
                    this.sqlManager.findRecordByID(MyConstants.tableName_Book, MyConstants.columnNames_Book, this.sqlManager.getLastId<Book>());
                    break;
                default:
                    break;
            }
        }

        // Show option implementation to print all objects from table
        public void Show()
        {
            MenuContainer.showOp_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine(MyConstants.tableName_Reader);
                    this.sqlManager.printTable(MyConstants.tableName_Reader, MyConstants.columnNames_Reader);
                    ShowWithOrder(MyConstants.tableName_Reader, MyConstants.columnNames_Reader);
                    break;
                case "2":
                    Console.WriteLine(MyConstants.tableName_Book);
                    this.sqlManager.printTable(MyConstants.tableName_Book, MyConstants.columnNames_Book);
                    ShowWithOrder(MyConstants.tableName_Book, MyConstants.columnNames_Book);
                    break;
                case "3":
                    Console.WriteLine(MyConstants.tableName_Loans);
                    this.sqlManager.printTable(MyConstants.tableName_Loans, MyConstants.columnNames_Loans);
                    ShowWithOrder(MyConstants.tableName_Loans, MyConstants.columnNames_Loans);
                    break;
                case "4":
                    Console.WriteLine(MyConstants.tableName_Receivings);
                    this.sqlManager.printTable(MyConstants.tableName_Receivings, MyConstants.columnNames_Recievings);
                    ShowWithOrder(MyConstants.tableName_Receivings, MyConstants.columnNames_Recievings);
                    break;
                case "5":
                    Console.WriteLine(MyConstants.tableName_CurrentlyLoaned);
                    this.sqlManager.printTable(MyConstants.tableName_CurrentlyLoaned, MyConstants.columnNames_CurrentlyLoaned);
                    ShowWithOrder(MyConstants.tableName_CurrentlyLoaned, MyConstants.columnNames_CurrentlyLoaned);
                    break;
                case "6":
                    Console.WriteLine(MyConstants.tableName_Genre);
                    this.sqlManager.printTable(MyConstants.tableName_Genre, MyConstants.columnNames_Genre);
                    ShowWithOrder(MyConstants.tableName_Genre, MyConstants.columnNames_Genre);
                    break;
                case "7":
                    Console.WriteLine(MyConstants.tableName_Arrears);
                    this.sqlManager.printTable(MyConstants.tableName_Arrears, MyConstants.columnNames_Arrears);
                    ShowWithOrder(MyConstants.tableName_Arrears, MyConstants.columnNames_Arrears);
                    break;
                case "8":
                    Console.WriteLine(MyConstants.tableName_Payments);
                    this.sqlManager.printTable(MyConstants.tableName_Payments, MyConstants.columnNames_Payments);
                    ShowWithOrder(MyConstants.tableName_Payments, MyConstants.columnNames_Payments);
                    break;
                default:
                    break;
            }
        }

        // Show records with order option implementation to show a table with some ordering
        public void ShowWithOrder(string tableName, string[] columns)
        {
            Console.WriteLine("Show this table in order?");
            MenuContainer.showWithOrder();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Ordering table ascending");
                    ShowTableWithOrder(tableName, columns);
                    break;
                case "2":
                    Console.WriteLine("Ordering table descending");
                    ShowTableWithOrder(tableName, columns, false);
                    break;
                default:
                    break;
            }
        }

        // Inner method that supports ShowWithOrder method
        private void ShowTableWithOrder(string tableName, string[] columnNames, bool ascending = true)
        {
            Console.WriteLine("Choose ordering column");
            MenuContainer.printColumns(columnNames);
            int columnNum = InputManager.enterNum("your option");
            if (columnNum - 1 >= 0 && columnNum - 1 < columnNames.Length)
                this.sqlManager.printTable(tableName, columnNames, columnNum - 1, ascending);
            else Console.WriteLine("You choosed wrong column");
        }

        // Find record option implementation to find record from a table
        public void Find()
        {
            MenuContainer.showOp_Staff();
            string? option = Console.ReadLine();
            try{
            switch (option)
                {
                    case "1":
                        MenuContainer.printColumns_FindReader();
                        findValueByColumn(MyConstants.tableName_Reader, MyConstants.columnNames_Reader);
                        break;
                    case "2":
                        MenuContainer.printColumns_FindBook();
                        findValueByColumn(MyConstants.tableName_Book, MyConstants.columnNames_Book);
                        break;
                    case "3":
                        MenuContainer.printColumns_FindLoans();
                        findValueByColumn(MyConstants.tableName_Loans, MyConstants.columnNames_Loans);
                        break;
                    case "4":
                        MenuContainer.printColumns_FindReceivings();
                        findValueByColumn(MyConstants.tableName_Receivings, MyConstants.columnNames_Recievings);
                        break;
                    case "5":
                        MenuContainer.printColumns_FindGenre();
                        findValueByColumn(MyConstants.tableName_Genre, MyConstants.columnNames_Genre);
                        break;
                    case "6":
                        MenuContainer.printColumns_FindPayments();
                        findValueByColumn(MyConstants.tableName_Payments, MyConstants.columnNames_Payments);
                        break;
                    default:
                        break;
                }
            }catch(Exception e)
            {
                Console.WriteLine("Can't find a value");
                Logs.writeLog(e.Message);
            }
        }

        // Two inner mthods that extend functionality of Find method:

        // Inner method that supports Find method
        private void findValueByColumn(string tableName, string[] columns)
        {
            string? option = Console.ReadLine();
            if(string.IsNullOrEmpty(option))
                throw(new Exception());
            switch (tableName)
            {
                case MyConstants.tableName_Reader:
                    string readerValue = InputManager.enterStringFindTarget();
                    this.sqlManager.findRecords<string>(tableName, columns, getColumnIDUsingTable(tableName, option), readerValue);
                    break;
                case MyConstants.tableName_Book:
                    if (getColumnIDUsingTable(MyConstants.tableName_Book, option) == 1 || getColumnIDUsingTable(MyConstants.tableName_Book, option) == 2)
                    {
                        int bookValue = InputManager.enterNum("Value to find");
                        this.sqlManager.findRecords<int>(tableName, columns, getColumnIDUsingTable(MyConstants.tableName_Book, option), bookValue);
                    }
                    else if (getColumnIDUsingTable(MyConstants.tableName_Book, option) == 3 || getColumnIDUsingTable(MyConstants.tableName_Book, option) == 4
                            || getColumnIDUsingTable(MyConstants.tableName_Book, option) == 5)
                    {
                        int bookValue = InputManager.enterNum("Value to find");
                        this.sqlManager.findRecords<int>(tableName, columns, getColumnIDUsingTable(MyConstants.tableName_Book, option), bookValue);
                    }
                    break;
                case MyConstants.tableName_Loans:
                    int loansValue = InputManager.enterNum("Value to find");
                    this.sqlManager.findRecords<int>(tableName, columns, getColumnIDUsingTable(tableName, option), loansValue);
                    break;
                case MyConstants.tableName_Receivings:
                    int receivingValue = InputManager.enterNum("Value to find");
                    this.sqlManager.findRecords<int>(tableName, columns, getColumnIDUsingTable(tableName, option), receivingValue);
                    break;
                case MyConstants.tableName_Genre:
                    int genreValue = InputManager.enterNum("Value to find");
                    this.sqlManager.findRecords<int>(tableName, columns, getColumnIDUsingTable(tableName, option), genreValue);
                    break;
                case MyConstants.tableName_Payments:
                    int paymentValue = InputManager.enterNum("Value to find");
                    this.sqlManager.findRecords<int>(tableName, columns, getColumnIDUsingTable(tableName, option), paymentValue);
                    break;
                default:
                    break;
            }
        }

        // Inner method that supports another inner method - findValueByColumn
        private int getColumnIDUsingTable(string tableName, string option)
        {
            if(tableName == MyConstants.tableName_Reader)
                switch (option)
                {
                    case "1":
                        return 1;
                    case "2":
                        return 2;
                    case "3":
                        return 5;
                    case "4":
                        return 6;
                    case "5":
                        return 7;
                    case "6":
                        return 8;
                    default:
                        return 1;
                }
            if(tableName == MyConstants.tableName_Book)
            {
                switch (option)
                {
                    case "1":
                        return 1;
                    case "2":
                        return 2;
                    case "3":
                        return 3;
                    case "4":
                        return 4;
                    case "5":
                        return 5;
                    default:
                        return 1;
                }
            }
            if(tableName == MyConstants.tableName_Loans)
            {
                switch (option)
                {
                    case "1":
                        return 1;
                    case "2":
                        return 2;
                    case "3":
                        return 3;
                    default:
                        return 1;
                }
            }
            if(tableName == MyConstants.tableName_Receivings)
            {
                return 1;
            }
            if(tableName == MyConstants.tableName_Genre)
            {
                return 1;
            }
            if(tableName == MyConstants.tableName_Payments)
            {
                return 1;
            }
            return 1;
        }

        // Find record option implementation to find record from a table by ID
        public void FindByID()
        {
            MenuContainer.showOp_Staff();
            string? option = Console.ReadLine();
            Console.WriteLine();
            int ID = InputManager.enterNum();
            switch (option)
            {
                case "1":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Reader, MyConstants.columnNames_Reader, ID);
                    break;
                case "2":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Book, MyConstants.columnNames_Book, ID);
                    break;
                case "3":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Loans, MyConstants.columnNames_Loans, ID);
                    break;
                case "4":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Receivings, MyConstants.columnNames_Recievings, ID);
                    break;
                case "5":
                    this.sqlManager.findRecordByID(MyConstants.tableName_CurrentlyLoaned, MyConstants.columnNames_CurrentlyLoaned, ID);
                    break;
                case "6":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Genre, MyConstants.columnNames_Genre, ID);
                    break;
                case "7":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Arrears, MyConstants.columnNames_Arrears, ID);
                    break;
                case "8":
                    this.sqlManager.findRecordByID(MyConstants.tableName_Payments, MyConstants.columnNames_Payments, ID);
                    break;
            }
        }

        // Inner method, created for main menu. Directs operation processing for arrears calculation
        private void CalculateArrears()
        {
            int ID = InputManager.enterNum();
            this.sqlManager.calculateArrears(ID);
        }

        // Method inherited from interface, but can't be used for Staff members
        public void DeleteAll(){ }
    }
}
