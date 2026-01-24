namespace Biblioteka_Projekt
{
    internal interface ISQLMenu : IMenu
    {
        void Delete();
        void ShowWithOrder(string tableName, string[] columns);
        void Find();
        void FindByID();
        void MainMenu();
    }
}
