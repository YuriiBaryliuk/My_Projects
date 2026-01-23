namespace Biblioteka_Projekt
{
    internal interface IMenu
    {
        
        void Add();
        void Delete();
        void Show();
        void ShowWithOrder(string tableName, string[] columns);
        void Find();
        void FindByID();
        void MainMenu();
    }
}
