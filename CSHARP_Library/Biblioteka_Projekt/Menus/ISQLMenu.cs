// Special interface that inherited from IMenu interface
// Adds (to already defined in IMenu methods) own method definitions for SQL Menu options
namespace Biblioteka_Projekt
{
    internal interface ISQLMenu : IMenu
    {
        // Delete record from table
        void Delete();

        // Show records from passed table in some order
        void ShowWithOrder(string tableName, string[] columns);

        // Find record
        void Find();

        // Find record using ID
        void FindByID();
    }
}
