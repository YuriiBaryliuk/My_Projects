// Main interface that declare methods for Menu operations
namespace Biblioteka_Projekt
{
    internal interface IMenu
    {
        // Options:
        // Add obect to database
        void Add();

        // Show all objects from database (table)
        void Show();

        // Delete all records from database (table)
        void DeleteAll();

        // Show last added obect
        void SeeLast();

        // Show main menu
        void MainMenu();
    }
}
