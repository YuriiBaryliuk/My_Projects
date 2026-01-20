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
        
        if (sqlManager.getInitError())
            Console.WriteLine("Not Proper Initialization");
        else
            Console.WriteLine("Proper Initialization");

        //sqlManager.inputAndSave<Reader>();
        //sqlManager.loanBook();
        //sqlManager.ReceiveBook();

        sqlManager.printTable("Reader", MyConstants.columnNames_Reader);

        /*
        Book b = new Book("Author", "Title", 2000, 5);
        using SqlConnection connection = new SqlConnection(MyConstants.connectionToCSLibrary);
        using SqlCommand command = new SqlCommand(SQLCommandContainer.addBook(), connection);
        connection.Open();
                //command.Parameters.AddWithValue("@Book_id", b.m_ID);
                command.Parameters.AddWithValue("@Author", b.m_author);
                command.Parameters.AddWithValue("@Title", b.m_title);
                command.Parameters.AddWithValue("@YearOfRelease", b.m_yearOfRelease);
                command.Parameters.AddWithValue("@Genre_id", b.m_genreID);
                command.Parameters.AddWithValue("@BookDescription", b.m_description);

        command.ExecuteNonQuery();
        */
        //foreach (var name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
        //    Console.WriteLine(name);

       

        /*
        using var man = Assembly.GetExecutingAssembly().GetManifestResourceStream("Biblioteka_Projekt.CheckTables.sql");
        StreamReader sr = new StreamReader(man);
        string command = sr.ReadToEnd();
        Console.WriteLine(command);
        */
    }

}