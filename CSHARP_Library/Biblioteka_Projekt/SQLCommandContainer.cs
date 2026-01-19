namespace Biblioteka_Projekt
{
    internal static class SQLCommandContainer
    {
        public static string createDB()
        {
            return @"USE MASTER
                    if exists (select * from sysdatabases where name='CSLibrary')
		                    drop database CSLibrary
                    CREATE DATABASE CSLibrary
                    SET DATEFORMAT ymd";
        }

        public static string checkDBexist()
        {
            return @"select count(*) 
                    from sys.databases
                    where name = 'CSLibrary'";
        }

        public static string addReader()
        {
            return @"insert into Reader (LastName, FirstName, Gender, BirthDate, [Address], City, Phone, Email, Registered) 
                     values (@LastName, @FirstName, @Gender, @BirthDate, @ReaderAddress, @City, @Phone, @Email, @Registered)";
        }
        public static string addBook()
        {
            return @"insert into Book (Author, Title, YearOfRelease, Genre_id, [Description]) 
                     values (@Author, @Title, @YearOfRelease, @Genre_id, @BookDescription)";
        }

        public static string deleteAllRows(string tableName)
        {
            return $"delete from {tableName}";
        }

        public static string uncheckAllConstraint(string tableName)
        {
            return $"alter table {tableName} nocheck constraint all";
        }

        public static string checkAllConstraint(string tableName)
        {
            return $"alter table {tableName} check constraint all";
        }
        public static string checkIfRecordsExist(string tableName)
        {
            return $"select count(*) from {tableName}";
        }

        public static string deleteRecord(int ID, string tableName)
        {
            return $"delete from {tableName} where {tableName}_id = {ID}";
        }
    }
}
