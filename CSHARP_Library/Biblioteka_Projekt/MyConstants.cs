namespace Biblioteka_Projekt
{
    static internal class MyConstants
    {
        public const int numberOfGenres = 10;
        public const string connectionToMaster = "Server=.;Database=master;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        public const string connectionToCSLibrary = "Server=.;Database=CSLibrary;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        public const string appNamespace = "Biblioteka_Projekt.";
        public const string resourceSQL_CheckTables = appNamespace + "CheckTables.sql";
        public const string resourceSQL_TablesCreation = appNamespace + "TablesCreation.sql";
        public const string resourceSQL_TablesInsertion = appNamespace + "TablesInsertion.sql";
    }
}
