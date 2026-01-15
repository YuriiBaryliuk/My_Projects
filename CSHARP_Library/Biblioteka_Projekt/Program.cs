using Biblioteka_Projekt;
using Microsoft.Data.SqlClient;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
        /*
        string connection = "Server=(local);Database=master;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        SQLManager sqlManager = new SQLManager(connection);
        if (sqlManager.getInitError())
            Console.WriteLine("Not Proper Initialization");
        else
            Console.WriteLine("Proper Initialization");
        */
        //ReaderManager rm = new ReaderManager();
        //rm.printReadersRegister();
        //rm.inputReader();
        //rm.printReadersRegister();
        //BookManager bm = new BookManager();
        //bm.printBooksRegister();
        //bm.inputBook();
        //bm.printBooksRegister();

        foreach (var name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            Console.WriteLine(name);

        using var man = Assembly.GetExecutingAssembly().GetManifestResourceStream("Biblioteka_Projekt.CheckTables.sql");
        StreamReader sr = new StreamReader(man);
        string command = sr.ReadToEnd();
        Console.WriteLine(command);
    }

}