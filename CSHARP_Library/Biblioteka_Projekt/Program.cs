using Biblioteka_Projekt;
using Microsoft.Data.SqlClient;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
        
        ReaderManager rm = new ReaderManager();
        rm.printRegister();
        rm.inputAndSave<Reader>();
        rm.printRegister();

        //BookManager bm = new BookManager();
        //bm.printRegister();
        //bm.inputAndSave<Book>();
        //bm.printRegister();




        //foreach (var name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
        //    Console.WriteLine(name);

       /*
        SQLManager sqlManager = new SQLManager(MyConstants.connectionToMaster);
        if (sqlManager.getInitError())
            Console.WriteLine("Not Proper Initialization");
        else
            Console.WriteLine("Proper Initialization");
        */

        /*
        using var man = Assembly.GetExecutingAssembly().GetManifestResourceStream("Biblioteka_Projekt.CheckTables.sql");
        StreamReader sr = new StreamReader(man);
        string command = sr.ReadToEnd();
        Console.WriteLine(command);
        */
    }

}