using System.Data.Common;

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

        public static string updateArrears()
        {
            return @"delete from Arrears
                    insert into Arrears
                    select L.Reader_id, DATEDIFF(day, LoanDate, GETDATE())
                    from [Currently Loaned] CL
                    inner join Loans L
                    on CL.Loan_id = L.Loan_id
                    where DATEDIFF(day, LoanDate, GETDATE()) > 14";
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
            return $"delete from [{tableName}]";
        }

        public static string uncheckAllConstraint(string tableName)
        {
            return $"alter table [{tableName}] nocheck constraint all";
        }

        public static string checkAllConstraint(string tableName)
        {
            return $"alter table [{tableName}] check constraint all";
        }
        public static string checkIfRecordsExist(string tableName)
        {
            return $"select count(*) from [{tableName}]";
        }
        public static string checkIfRecordExistUsingID(string tableName, string columnName, int ID)
        {
            return $"select count(*) from [{tableName}] where {columnName} = {ID}";
        }
        public static string checkReadersCurrentlyLoaningID(int ID)
        {
            return $"select count(Reader_id) " +
                   $"from [Currently Loaned] CL " +
                   $"join Loans L " +
                   $"on CL.Loan_id = L.Loan_id " +
                   $"where L.Reader_id = {ID}";
        }
        public static string checkBooksCurrentlyLoaningID(int ID)
        {
            return $"select count(Book_id) " +
                   $"from [Currently Loaned] CL " +
                   $"join Loans L " +
                   $"on CL.Loan_id = L.Loan_id " +
                   $"where L.Book_id = {ID}";
        }

        public static string deleteRecord(int ID, string tableName, string columnName)
        {
            return $"delete from [{tableName}] where [{columnName}] = {ID}";
        }

        public static string loanBook(Dictionary<string, int> loanDict, string description)
        {
            return $"insert into Loans values ({loanDict["Reader_id"]}, {loanDict["Staff_id"]}, {loanDict["Book_id"]}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{description}')";
        }
        public static string receiveBook(Dictionary<string, int> recDict, string description)
        {
            return $"insert into Recievings values ({recDict["Loan_id"]}, {recDict["Staff_id"]}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{description}')";
        }

        public static string getMaxElement(string tableName, string columnName)
        {
            return $"select max({columnName}) from [{tableName}]";
        }

        public static string insertValueIntoTable<T>(T value, string tableName)
        {
            if (typeof(T) == typeof(int))
                return $"declare @variable int = {value} " +
                    $"insert into [{tableName}] values (@variable)";
            else if (typeof(T) == typeof(string))
                return $"declare @variable varchar = {value} " +
                    $"insert into [{tableName}] values (@variable)";
            else return "";
        }

        public static string printTable(string tableName, string orderBy_columnName, bool ascending)
        {
            if (ascending == true)
                return $"select * from [{tableName}] order by [{orderBy_columnName}]";
            else
                return $"select * from [{tableName}] order by [{orderBy_columnName}] desc";
        }
        public static string addStaff()
        {
            return @"insert into Staff (LastName, FirstName, Title) 
                     values (@LastName, @FirstName, @Title)";
        }

        public static string findRecord_String(string tableName, string columnName)
        {
            return $"select * from [{tableName}] where [{columnName}] like '%' + @Val + '%'";
        }
        public static string findRecord_Int(string tableName, string columnName)
        {
            return $"select * from [{tableName}] where [{columnName}] = @Val";
        }
        public static string findRecordByID(string tableName, string columnName, int ID)
        {
            return $"select * from [{tableName}] where [{columnName}] = {ID}";
        }
    }
}