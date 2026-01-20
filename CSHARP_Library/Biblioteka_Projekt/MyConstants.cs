namespace Biblioteka_Projekt
{
    static internal class MyConstants
    {
        public const int numberOfGenres = 10;
        // Files
        public const string file_ReaderDB = "Readers_DB.txt";
        public const string file_BooksDB = "Books_DB.txt";
        // SQL
        public const string connectionToMaster = "Server=.;Database=master;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        public const string connectionToCSLibrary = "Server=.;Database=CSLibrary;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        public const string appNamespace = "Biblioteka_Projekt.";
        public const string resourceSQL_CheckTables = appNamespace + "CheckTables.sql";
        public const string resourceSQL_TablesCreation = appNamespace + "TablesCreation.sql";
        public const string resourceSQL_TablesInsertion = appNamespace + "TablesInsertion.sql";
        public static readonly string[] columnNames_Book = { "Book_id", "Author", "Title", "YearOfRelease", "Genre_id", "Description" };
        public static readonly string[] columnNames_Reader = { "Reader_id", "LastName", "FirstName", "Gender", "BirthDate", "Address", "City", "Phone", "Email", "Registered" };
        public static readonly string[] columnNames_Staff = { "Staff_id", "LastName", "FirstName", "Title"};
        public static readonly string[] columnNames_Arrears = { "Reader_id", "Days"};
        public static readonly string[] columnNames_CurrentlyLoaned = {"Loan_id"};
        public static readonly string[] columnNames_Genre = { "Genre_id", "GenreName"};
        public static readonly string[] columnNames_Loans = { "Loan_id", "Reader_id", "Staff_id", "Book_id", "LoanDate", "Note"};
        public static readonly string[] columnNames_Payments = { "Payment_id", "Reader_id", "Amount", "PaymentDate"};
        public static readonly string[] columnNames_Recievings = { "Loan_id", "Staff_id", "RecievingDate", "Note"};
    }
}
