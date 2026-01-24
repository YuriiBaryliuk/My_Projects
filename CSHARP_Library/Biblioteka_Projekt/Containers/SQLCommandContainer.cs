// This container stores SQL commands context
using System.Data.Common;

namespace Biblioteka_Projekt
{
    internal static class SQLCommandContainer
    {
        // Initialization commands
        public static string createDB() // Command to create database
        {
            return @"USE MASTER
                    if exists (select * from sysdatabases where name='CSLibrary')
		                    drop database CSLibrary
                    CREATE DATABASE CSLibrary
                    SET DATEFORMAT ymd";
        }

        public static string checkDBexist() // Command to check if database exists
        {
            return @"select count(*) 
                    from sys.databases
                    where name = 'CSLibrary'";
        }

        public static string updateArrears()    // Command to update Arrears when starting program
        {
            return @"delete from Arrears
                    insert into Arrears
                    select L.Reader_id, DATEDIFF(day, LoanDate, GETDATE()) - 14
                    from [Currently Loaned] CL
                    inner join Loans L
                    on CL.Loan_id = L.Loan_id
                    where DATEDIFF(day, LoanDate, GETDATE()) > 14";
        }

        // Insertion commands
        public static string addReader()    // Add reader to DB
        {
            return @"insert into Reader (LastName, FirstName, Gender, BirthDate, [Address], City, Phone, Email, Registered) 
                     values (@LastName, @FirstName, @Gender, @BirthDate, @ReaderAddress, @City, @Phone, @Email, @Registered)";
        }
        public static string addBook()      // Add book to DB
        {
            return @"insert into Book (Author, Title, YearOfRelease, Genre_id, [Description]) 
                     values (@Author, @Title, @YearOfRelease, @Genre_id, @BookDescription)";
        }
        public static string loanBook(Dictionary<string, int> loanDict, string description) // Insert values into Loans table when loaning a book
        {
            return $"insert into Loans values ({loanDict["Reader_id"]}, {loanDict["Staff_id"]}, {loanDict["Book_id"]}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{description}')";
        }
        public static string receiveBook(Dictionary<string, int> recDict, string description)   // Insert values into Recievings when receiving a book
        {
            return $"insert into Recievings values ({recDict["Loan_id"]}, {recDict["Staff_id"]}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{description}')";
        }

        public static string insertValueIntoTable<T>(T value, string tableName) // Insert values into a table filtering by type name (int, string)
        {
            if (typeof(T) == typeof(int))
                return $"declare @variable int = {value} " +
                    $"insert into [{tableName}] values (@variable)";
            else if (typeof(T) == typeof(string))
                return $"declare @variable varchar = {value} " +
                    $"insert into [{tableName}] values (@variable)";
            else return "";
        }
        public static string addStaff() // Adding staff member into a Staff table
        {
            return @"insert into Staff (LastName, FirstName, Title) 
                     values (@LastName, @FirstName, @Title)";
        }

        // Delete commands
        public static string deleteAllRows(string tableName)    // Delete all rows in the table
        {
            return $"delete from [{tableName}]";
        }
        public static string deleteRecord(int ID, string tableName, string columnName)  // Delete record from the table
        {
            return $"delete from [{tableName}] where [{columnName}] = {ID}";
        }

        // Constraint commands
        public static string uncheckAllConstraint(string tableName) // Uncheck all constraints from the tables
        {
            return $"alter table [{tableName}] nocheck constraint all";
        }

        public static string checkAllConstraint(string tableName) // Check all constraints from the tables
        {
            return $"alter table [{tableName}] check constraint all";
        }

        // Select commands
        public static string checkIfRecordsExist(string tableName)  // Chekc if table has records
        {
            return $"select count(*) from [{tableName}]";
        }
        public static string checkIfRecordExistUsingID(string tableName, string columnName, int ID) // Check if record exists in the table using ID
        {
            return $"select count(*) from [{tableName}] where {columnName} = {ID}";
        }
        public static string checkReadersCurrentlyLoaningID(int ID) // Check which readers are currently loaning books
        {
            return $"select count(Reader_id) " +
                   $"from [Currently Loaned] CL " +
                   $"join Loans L " +
                   $"on CL.Loan_id = L.Loan_id " +
                   $"where L.Reader_id = {ID}";
        }
        public static string checkBooksCurrentlyLoaningID(int ID)   // Check which books are currently loaned
        {
            return $"select count(Book_id) " +
                   $"from [Currently Loaned] CL " +
                   $"join Loans L " +
                   $"on CL.Loan_id = L.Loan_id " +
                   $"where L.Book_id = {ID}";
        }
        public static string getMaxElement(string tableName, string columnName) // Get max element from a table
        {
            return $"select max({columnName}) from [{tableName}]";
        }
        public static string printTable(string tableName, string orderBy_columnName, bool ascending)    // Print table with ascending/descending option (last parameter: true -> prints ascending)
        {
            if (ascending == true)
                return $"select * from [{tableName}] order by [{orderBy_columnName}]";
            else
                return $"select * from [{tableName}] order by [{orderBy_columnName}] desc";
        }

        public static string findRecord_String(string tableName, string columnName) // Find varchar record in passed table
        {
            return $"select * from [{tableName}] where [{columnName}] like '%' + @Val + '%'";
        }
        public static string findRecord_Int(string tableName, string columnName)
        {
            return $"select * from [{tableName}] where [{columnName}] = @Val";      // Find int record in passed table
        }
        public static string findRecordByID(string tableName, string columnName, int ID)    // Find record by ID
        {
            return $"select * from [{tableName}] where [{columnName}] = {ID}";
        }
        public static string getDays(int ID)    // Get days from Arrears using reader ID
        {
            return $"select Days from Arrears where Reader_id = {ID}";
        }
    }
}