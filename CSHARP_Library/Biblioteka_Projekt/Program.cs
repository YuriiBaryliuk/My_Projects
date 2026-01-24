using Biblioteka_Projekt;
using Microsoft.Data.SqlClient;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
//--------------------Using file Database--------------------//
/*
        ReaderManager rm = new ReaderManager();
        BookManager bm = new BookManager();
        BaseMenu baseMenu = new BaseMenu(rm, bm);
        baseMenu.MainMenu();
*/
//--------------------Using SQL Database--------------------//        
        SQLManager sqlManager = new SQLManager(MyConstants.connectionToMaster);
        
        if (!sqlManager.getInitError()){
            Console.WriteLine("Proper Initialization");
            StartMenu.StartingMenu(sqlManager);
        }
        else
            Console.WriteLine("Not Proper Initialization");
    }

}