using Biblioteka_Projekt;
using Microsoft.Data.SqlClient;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
        
        //ReaderManager rm = new ReaderManager();
        //rm.printRegister();
        //rm.inputAndSave<Reader>();
        //rm.printRegister();

        //BookManager bm = new BookManager();
        //bm.printRegister();
        //bm.inputAndSave<Book>();
        //bm.printRegister();

        
        SQLManager sqlManager = new SQLManager(MyConstants.connectionToMaster);
        
        if (!sqlManager.getInitError()){
            Console.WriteLine("Proper Initialization");

            StaffMenu staffMenu = new StaffMenu(sqlManager);
            staffMenu.MainMenu();
        }

        else
            Console.WriteLine("Not Proper Initialization");

       
    }

}