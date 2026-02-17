// Special class created for showing starting menu when program is starting running
namespace Biblioteka_Projekt
{
    internal static class StartMenu
    {
        // Method gives user options to choose a type of logging in
        public static void StartingMenu(SQLManager sqlManager)
        {
            Console.WriteLine("Log in as\n1. Staff\n2. Manager");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    StaffMenu staffMenu = new StaffMenu(sqlManager);
                    staffMenu.MainMenu();
                    break;
                case "2":
                    ManagerMenu managerMenu = new ManagerMenu(sqlManager);
                    Console.WriteLine(managerMenu.password);    // Comment to hide password
                    if (checkPassword(managerMenu))
                    {
                        Console.WriteLine("Password is correct");
                        managerMenu.MainMenu();
                    }
                    else Console.WriteLine("Password incorrect");
                    break;
                default:
                    break;
            }
        }

        // Inner method that checks if the Manager's password is correct
        private static bool checkPassword(ManagerMenu managerMenu)
        {
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();
            return password == managerMenu.password;
        }
    }
}
