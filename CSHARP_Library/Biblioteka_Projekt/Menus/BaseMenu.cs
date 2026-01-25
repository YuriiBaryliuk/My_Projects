namespace Biblioteka_Projekt
{
    internal class BaseMenu : IMenu
    {
        ManagerBase<Reader> rm;
        ManagerBase<Book> bm;

        public BaseMenu(ManagerBase<Reader> rm, ManagerBase<Book> bm)
        {
            this.rm = rm;
            this.bm = bm;
        }
        public void MainMenu()
        {
            Console.WriteLine("-------------Main Menu-------------");
            MenuContainer.mainMenu_Base();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Add");
                    Add();
                    break;
                case "2":
                    Console.WriteLine("Delete All");
                    DeleteAll();
                    break;
                case "3":
                    Console.WriteLine("See Last");
                    SeeLast();
                    break;
                case "4":
                    Console.WriteLine("Show");
                    Show();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
            MainMenu();
        }

        public void Add()
        {
            MenuContainer.addOp_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Add reader");
                    this.rm.inputAndSave<Reader>();
                    break;
                case "2":
                    Console.WriteLine("Add book");
                    this.bm.inputAndSave<Book>();
                    break;
                default:
                    break;
            }
        }
        public void DeleteAll()
        {
            MenuContainer.deleteOp_Staff();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Delete reader");
                    this.rm.deleteAll<Reader>();
                    break;
                case "2":
                    Console.WriteLine("Delete book");
                    this.bm.deleteAll<Book>();
                    break;
                default:
                    break;
            }
        }
        public void SeeLast()
        {
            MenuContainer.seeLastOp_Base();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Show last reader");
                    this.rm.getLastMember().printData();
                    break;
                case "2":
                    Console.WriteLine("Show last book");
                    this.bm.getLastMember().printData();
                    break;
                default:
                    break;
            }
        }
        public void Show()
        {
            MenuContainer.showAllOp_Base();
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Show all readers");
                    this.rm.printRegister();
                    break;
                case "2":
                    Console.WriteLine("Show all books");
                    this.bm.printRegister();
                    break;
                default:
                    break;
            }
        }
    }
}
