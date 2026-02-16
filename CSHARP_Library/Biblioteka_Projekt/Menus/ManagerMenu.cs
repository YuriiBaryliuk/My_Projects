namespace Biblioteka_Projekt
{
    internal class ManagerMenu : ISQLMenu
    {
        public string password{ get; private set; }
        SQLManager sqlManager;
        public ManagerMenu(SQLManager sqlManager)
        {
            this.password = getPassword();
            this.sqlManager = sqlManager;
        }

        private string getPassword()
        {
            Random rand = new Random();
            string password = "";
            for(int i = 0; i < 5; ++i)
            {
                int tempVal = rand.Next(0, 9);
                password += tempVal.ToString();
            }
            return password;
        }
        public void MainMenu()
        {
            Console.WriteLine("-------------Main Menu-------------");
            MenuContainer.mainMenu_Manager();
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
                    Console.WriteLine("Delete All");
                    DeleteAll();
                    break;
                case "4":
                    Console.WriteLine("See last");
                    SeeLast();
                    break;
                case "5":
                    Console.WriteLine("Show");
                    Show();
                    break;
                case "6":
                    Console.WriteLine("Find");
                    Find();
                    break;
                case "7":
                    Console.WriteLine("Find by ID");
                    FindByID();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
            MainMenu();
        }

        public void Add()
        {
            MenuContainer.addOp_Manager();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Add staff");
                    this.sqlManager.inputAndSave<Staff>();
                    break;
                default:
                    break;
            }
        }

        public void Delete()
        {
            MenuContainer.deleteOp_Manager();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Delete staff");
                    this.sqlManager.deleteRecord<Staff>(InputManager.enterNum());
                    break;
                default:
                    break;
            }
        }

        public void DeleteAll()
        {
            MenuContainer.deleteAll_Manager();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    this.sqlManager.deleteAll<Reader>();
                    break;
                case "2":
                    this.sqlManager.deleteAll<Book>();
                    break;
                case "3":
                    this.sqlManager.deleteAll<Staff>();
                    break;
                default:
                    break;
            }
        }

        public void SeeLast()
        {
            MenuContainer.seeLastOp_Manager();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("See last staff member");
                    this.sqlManager.findRecordByID(MyConstants.tableName_Staff, MyConstants.columnNames_Staff, this.sqlManager.getLastId<Staff>());
                    break;
                default:
                    break;
            }
        }

        public void Show()
        {
            Console.WriteLine("Show all staff members");
            this.sqlManager.printTable(MyConstants.tableName_Staff, MyConstants.columnNames_Staff);
        }

        public void Find()
        {
            Console.WriteLine("Find staff member");
            MenuContainer.printColumns_FindStaff();
            string? option = Console.ReadLine();
            string staffValue = InputManager.enterStringFindTarget();
            try{
                switch (option)
                {
                    case "1":
                        this.sqlManager.findRecords(MyConstants.tableName_Staff, MyConstants.columnNames_Staff, 1, staffValue);
                        break;
                    case "2":
                        this.sqlManager.findRecords(MyConstants.tableName_Staff, MyConstants.columnNames_Staff, 2, staffValue);
                        break;
                    case "3":
                        this.sqlManager.findRecords(MyConstants.tableName_Staff, MyConstants.columnNames_Staff, 3, staffValue);
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

        public void FindByID()
        {
            int ID = InputManager.enterNum();
            this.sqlManager.findRecordByID(MyConstants.tableName_Staff, MyConstants.columnNames_Staff, ID);
        }

        public void ShowWithOrder(string tableName, string[] columns){ }
    }
}
