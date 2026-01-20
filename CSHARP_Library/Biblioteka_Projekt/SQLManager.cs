using Microsoft.Data.SqlClient;
using System.IO;

namespace Biblioteka_Projekt
{
    internal class SQLManager : IManager<Book>, IManager<Reader>
    {
        private string m_connection;
        private bool initError = false; // Database initialization error (true if Database was not initialized properly)
        public bool getInitError() { return initError; }
        public SQLManager(string connection)
        {
            m_connection = connection;
            if (!checkDatabaseExist())
            {
                initSQL();
            }
            else{
                m_connection = MyConstants.connectionToCSLibrary;
            }
            initError = !checkTablesExist();    // Checking tables existion
            initError = !updateArrears();       // Update Arrears
        }

        private void initSQL()  // Database and Tables creation and initialization
        {
            string TableCreationCommand = "";
            string TableInsertionCommand = "";
            if(!executeQuaery(SQLCommandContainer.createDB(), "Can't create Database")) // Database creation
            {
                this.initError = true;
                return;
            }
            this.m_connection = MyConstants.connectionToCSLibrary; // Changing connection from master to CSLibrary
            // Reading commands from sql files
            if (IOFileManager.readCommand(MyConstants.resourceSQL_TablesCreation, ref TableCreationCommand) &&
                IOFileManager.readCommand(MyConstants.resourceSQL_TablesInsertion, ref TableInsertionCommand))
            // initError initialization to chaeck if there was an error
            // using inversion (!), because method returns false if there is an error. and initError is true if there was an error in initializaton
                initError = !(executeQuaery(TableCreationCommand, "Can't create Tables") &&
                            executeQuaery(TableInsertionCommand, "Can't insert Tables"));
        }

        private bool checkDatabaseExist()   // returns true if DB exists
        {
            bool databaseExists = false;
            string command = SQLCommandContainer.checkDBexist();
            databaseExists = executeQuaeryWithReturn<bool>(command, "Can't find database");
            return databaseExists;
        }
        private bool checkTablesExist()     // returns true if all tables exist
        {
            const string fileName = MyConstants.resourceSQL_CheckTables;
            string command = "";
            if (IOFileManager.readCommand(fileName, ref command))
                return executeQuaeryWithReturn<bool>(command, "Can't find tables");  // if all tables exists => 0
            return false;
        }

        private bool updateArrears()
        {
            return executeQuaery(SQLCommandContainer.updateArrears(), "Can't update arrears");
        }

        private bool executeQuaery(string command, string message = "Message")  // true if everything is okay
        {
            try
            {
                using SqlConnection sqlConnect = new SqlConnection(this.m_connection);
                sqlConnect.Open();
                using SqlCommand sqlCommand = new SqlCommand(command, sqlConnect);
                sqlCommand.ExecuteNonQuery();
            }catch(SqlException ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
                return false;
            }catch(Exception ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
                return false;
            }
            return true;
        }
        private T executeQuaeryWithReturn<T>(string command, string message = "Message")
        {
            T myVariable = default(T);
            try
            {
                using SqlConnection sqlConnect = new SqlConnection(this.m_connection);
                sqlConnect.Open();
                using SqlCommand sqlCommand = new SqlCommand(command, sqlConnect);
                myVariable = (T)Convert.ChangeType(sqlCommand.ExecuteScalar(), typeof(T));
            }catch(SqlException ex){
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }catch(Exception ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }
            return myVariable;
            
        }

        public void inputAndSave<T>()
        {
            if (typeof(T) == typeof(Reader))
            {
                Console.WriteLine("Reader");
                insertReader(InputManager.InputReader());
            }
            else if (typeof(T) == typeof(Book)){
                Console.WriteLine("Book");
                insertBook(InputManager.InputBook());
            }
        }

        private void insertReader(Reader r, string message = "Can't insert a reader")
        {
            try{
            using SqlConnection connection = new SqlConnection(m_connection);
            using SqlCommand command = new SqlCommand(SQLCommandContainer.addReader(), connection);
                connection.Open();
                //command.Parameters.AddWithValue("@Reader_id", r.m_ID);
                command.Parameters.AddWithValue("@LastName", r.m_surname);
                command.Parameters.AddWithValue("@FirstName", r.m_name);
                command.Parameters.AddWithValue("@Gender", r.m_gender);
                command.Parameters.AddWithValue("@BirthDate", r.m_dateOfBirth);
                command.Parameters.AddWithValue("@ReaderAddress", r.m_address.toSqlString());
                command.Parameters.AddWithValue("@City", r.m_address.m_city);
                command.Parameters.AddWithValue("@Phone", r.m_phoneNumber);
                command.Parameters.AddWithValue("@Email", r.m_email);
                command.Parameters.AddWithValue("@Registered", r.m_dateOfRegistration);

                command.ExecuteNonQuery();
            }catch(SqlException ex){
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }catch(Exception ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }
        }

        private void insertBook(Book b, string message = "Can't insert a book")
        {
            try{
            using SqlConnection connection = new SqlConnection(m_connection);
            using SqlCommand command = new SqlCommand(SQLCommandContainer.addBook(), connection);
                connection.Open();
                //command.Parameters.AddWithValue("@Book_id", b.m_ID);
                command.Parameters.AddWithValue("@Author", b.m_author);
                command.Parameters.AddWithValue("@Title", b.m_title);
                command.Parameters.AddWithValue("@YearOfRelease", b.m_yearOfRelease);
                command.Parameters.AddWithValue("@Genre_id", b.m_genreID);
                command.Parameters.AddWithValue("@BookDescription", b.m_description);

                command.ExecuteNonQuery();
            }catch(SqlException ex){
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }catch(Exception ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }
        }
        public void deleteAll<T>()
        {
            if (typeof(T) == typeof(Reader))
            {
                if (!(executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist("Arrears")) ||
                    executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist("[Currently Loaned]")))){
                    executeQuaery(SQLCommandContainer.uncheckAllConstraint("Loans"));
                    executeQuaery(SQLCommandContainer.deleteAllRows("Reader"), "Can't delete a table");
                    executeQuaery(SQLCommandContainer.checkAllConstraint("Loans"));
                }
                else Console.WriteLine("Can't delete Readers from the table\nSome readers are still loan books");
            }
            else if (typeof(T) == typeof(Book)){
                if(!executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist("[Currently Loaned]"))){
                    executeQuaery(SQLCommandContainer.uncheckAllConstraint("Loans"));
                    executeQuaery(SQLCommandContainer.deleteAllRows("Book"), "Can't delete a table");
                    executeQuaery(SQLCommandContainer.checkAllConstraint("Loans"));
                }
                else Console.WriteLine("Can't delete Books from the table\nSome books are still loaned");
            }
        }

        public void deleteRecord<T>(int ID)
        {
            if (typeof(T) == typeof(Reader))
            {
                if (!(executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist("Arrears")) ||
                    executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist("[Currently Loaned]")))){
                    executeQuaery(SQLCommandContainer.uncheckAllConstraint("Loans"));
                    executeQuaery(SQLCommandContainer.deleteRecord(ID, "Reader", "Reader_id"), "Can't delete a record");
                    executeQuaery(SQLCommandContainer.checkAllConstraint("Loans"));
                }
                else Console.WriteLine("Can't delete a reader from the table\nReader still loanes books");
            }
            else if (typeof(T) == typeof(Book)){
                if(!executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist("[Currently Loaned]"))){
                    executeQuaery(SQLCommandContainer.uncheckAllConstraint("Loans"));
                    executeQuaery(SQLCommandContainer.deleteRecord(ID, "Book", "Book_id"), "Can't delete a record");
                    executeQuaery(SQLCommandContainer.checkAllConstraint("Loans"));
                }
                else Console.WriteLine("Can't delete book from the table\nBook is still loaned");
            }
        }

        public void loanBook()
        {
            Dictionary<string, int> loanDict = InputManager.loanBook();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            if (executeQuaery(SQLCommandContainer.loanBook(loanDict, string.IsNullOrEmpty(description) ? "-" : description), "Can't process operation"))
                insertValueIntoTable<int>(executeQuaeryWithReturn<int>(SQLCommandContainer.getMaxElement("Loans", "Loan_id"), "Can't add book to currently loaned books"), "Currently Loaned");
        }
        public void ReceiveBook()
        {
            Dictionary<string, int> recDict = InputManager.receiveBook();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            if (executeQuaery(SQLCommandContainer.receiveBook(recDict, string.IsNullOrEmpty(description) ? "-" : description), "Can't process operation"))
                executeQuaery(SQLCommandContainer.deleteRecord(recDict["Loan_id"], "Currently Loaned", "Loan_id"));
        }

        public int getLastId<T>()
        {
            if (typeof(T) == typeof(Reader))
                return executeQuaeryWithReturn<int>(SQLCommandContainer.getMaxElement("Reader", "Reader_id"), "Can't get last element");
            else if (typeof(T) == typeof(Book))
                return executeQuaeryWithReturn<int>(SQLCommandContainer.getMaxElement("Book", "Book_id"), "Can't get last element");
            else return 0;
        }

        public bool insertValueIntoTable<T>(T value, string tableName)
        {
            return executeQuaery(SQLCommandContainer.insertValueIntoTable<T>(value, tableName));
        }

        public void printTable(string tableName, string[] columnNames, string message = "Can't read data from a table")
        {
            try
            {
                using SqlConnection sqlConnect = new SqlConnection(this.m_connection);
                sqlConnect.Open();
                using SqlCommand sqlCommand = new SqlCommand(SQLCommandContainer.printTable(tableName), sqlConnect);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                Console.WriteLine($"--------------{tableName} table--------------");

                while (reader.Read())
                {
                    foreach(string columnName in columnNames)
                    {
                        Console.WriteLine($"{columnName}: {reader[columnName]}");
                    }
                    Console.WriteLine();
                }

            }catch(SqlException ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }catch(Exception ex)
            {
                Logs.writeLog(message + ": " + ex.Message);
                Console.WriteLine(message);
            }
        }
    }
}
