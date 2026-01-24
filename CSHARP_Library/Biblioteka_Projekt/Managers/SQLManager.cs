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
                insertReader(InputManager.InputReader());
            }
            else if (typeof(T) == typeof(Book)){
                insertBook(InputManager.InputBook());
            }
            else if (typeof(T) == typeof(Staff))
            {
                insertStaff(InputManager.inputStaff());
            }
        }

        private void insertReader(Reader r, string message = "Can't insert a reader")
        {
            try{
            using SqlConnection sqlConnect = new SqlConnection(m_connection);
            using SqlCommand command = new SqlCommand(SQLCommandContainer.addReader(), sqlConnect);
                sqlConnect.Open();
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
            using SqlConnection sqlConnect = new SqlConnection(m_connection);
            using SqlCommand command = new SqlCommand(SQLCommandContainer.addBook(), sqlConnect);
                sqlConnect.Open();
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

        private void insertStaff(Staff s, string message = "Can't insert a staff member")
        {
            try{
            using SqlConnection sqlConnect = new SqlConnection(m_connection);
            using SqlCommand command = new SqlCommand(SQLCommandContainer.addStaff(), sqlConnect);
                sqlConnect.Open();
                command.Parameters.AddWithValue("@LastName", s.m_surname);
                command.Parameters.AddWithValue("@FirstName", s.m_name);
                command.Parameters.AddWithValue("@Title", s.m_title);

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
                if (!(executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist(MyConstants.tableName_Arrears), "Can't delete records") ||
                    executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist(MyConstants.tableName_CurrentlyLoaned), "Can't delete records"))){
                    executeQuaery(SQLCommandContainer.uncheckAllConstraint(MyConstants.tableName_Loans), "Can't uncheck constraint");
                    executeQuaery(SQLCommandContainer.deleteAllRows(MyConstants.tableName_Reader), "Can't delete a table");
                    executeQuaery(SQLCommandContainer.checkAllConstraint(MyConstants.tableName_Loans), "Can't check constraint");
                }
                else Console.WriteLine("Can't delete Readers from the table\nSome readers are still loan books");
            }
            else if (typeof(T) == typeof(Book)){
                if(!executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordsExist(MyConstants.tableName_CurrentlyLoaned), "Can't delete records")){
                    executeQuaery(SQLCommandContainer.uncheckAllConstraint(MyConstants.tableName_Loans), "Can't uncheck constraint");
                    executeQuaery(SQLCommandContainer.deleteAllRows(MyConstants.tableName_Book), "Can't delete a table");
                    executeQuaery(SQLCommandContainer.checkAllConstraint(MyConstants.tableName_Loans), "Can't check constraint");
                }
                else Console.WriteLine("Can't delete Books from the table\nSome books are still loaned");
            }
            else if (typeof(T) == typeof(Staff)){
                executeQuaery(SQLCommandContainer.deleteAllRows(MyConstants.tableName_Staff), "Can't delete Staff members");
            }
        }
        public void deleteRecord<T>(int ID)
        {
            try{
                if (typeof(T) == typeof(Reader))
                {
                    if (!(executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordExistUsingID(MyConstants.tableName_Arrears, MyConstants.columnNames_Arrears[0], ID), "Can't delete a record") ||
                        executeQuaeryWithReturn<bool>(SQLCommandContainer.checkReadersCurrentlyLoaningID(ID), "Can't delete a record"))){
                        executeQuaery(SQLCommandContainer.uncheckAllConstraint(MyConstants.tableName_Loans), "Can't uncheck constraints");
                        executeQuaery(SQLCommandContainer.deleteRecord(ID, MyConstants.tableName_Reader, MyConstants.columnNames_Reader[0]), "Can't delete a record");
                        executeQuaery(SQLCommandContainer.checkAllConstraint(MyConstants.tableName_Loans), "Can't check constraints");
                    }
                    else Console.WriteLine("Can't delete a reader from the table\nReader still loanes books");
                }
                else if (typeof(T) == typeof(Book)){
                    if(!executeQuaeryWithReturn<bool>(SQLCommandContainer.checkBooksCurrentlyLoaningID(ID), "Can't delete a record")){
                        executeQuaery(SQLCommandContainer.uncheckAllConstraint(MyConstants.tableName_Loans), "Can't uncheck constraints");
                        executeQuaery(SQLCommandContainer.deleteRecord(ID, MyConstants.tableName_Book, MyConstants.columnNames_Book[0]), "Can't delete a record");
                        executeQuaery(SQLCommandContainer.checkAllConstraint(MyConstants.tableName_Loans), "Can't check constraints");
                    }
                    else Console.WriteLine("Can't delete book from the table\nBook is still loaned");
                }
                else if (typeof(T) == typeof(Staff)){
                    executeQuaery(SQLCommandContainer.deleteRecord(ID, MyConstants.tableName_Staff, MyConstants.columnNames_Staff[0]), "Can't delete a Staff");
                }
            }catch(Exception e){
                Console.WriteLine("Can't delete record");
                Logs.writeLog(e.Message);
            }
        }

        public void loanBook()
        {
            Dictionary<string, int> loanDict = InputManager.loanBook();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            if (executeQuaeryWithReturn<bool>(SQLCommandContainer.checkBooksCurrentlyLoaningID(loanDict["Book_id"]), "Can't loan a book"))
                return;
            if (executeQuaery(SQLCommandContainer.loanBook(loanDict, string.IsNullOrEmpty(description) ? "-" : description), "Can't process operation"))
                insertValueIntoTable<int>(executeQuaeryWithReturn<int>(SQLCommandContainer.getMaxElement(MyConstants.tableName_Loans, MyConstants.columnNames_Loans[0]), "Can't add book to currently loaned books"), MyConstants.tableName_CurrentlyLoaned);
        }
        public void ReceiveBook()
        {
            Dictionary<string, int> recDict = InputManager.receiveBook();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            if (executeQuaery(SQLCommandContainer.receiveBook(recDict, string.IsNullOrEmpty(description) ? "-" : description), "Can't process operation"))
                executeQuaery(SQLCommandContainer.deleteRecord(recDict[MyConstants.columnNames_Loans[0]], MyConstants.tableName_CurrentlyLoaned, MyConstants.columnNames_Loans[0]), "Can't receive a book");
        }

        public int getLastId<T>()
        {
            if (typeof(T) == typeof(Reader))
                return executeQuaeryWithReturn<int>(SQLCommandContainer.getMaxElement(MyConstants.tableName_Reader, MyConstants.columnNames_Reader[0]), "Can't get last element");
            else if (typeof(T) == typeof(Book))
                return executeQuaeryWithReturn<int>(SQLCommandContainer.getMaxElement(MyConstants.tableName_Book, MyConstants.columnNames_Book[0]), "Can't get last element");
            else return 0;
        }

        private bool insertValueIntoTable<T>(T value, string tableName)
        {
            return executeQuaery(SQLCommandContainer.insertValueIntoTable<T>(value, tableName), "Can't insert a record");
        }

        public void printTable(string tableName, string[] columnNames, int orderBy = 0, bool ascending = true, string message = "Can't read data from a table")
        {
            try
            {
                using SqlConnection sqlConnect = new SqlConnection(this.m_connection);
                sqlConnect.Open();
                using SqlCommand sqlCommand = new SqlCommand(SQLCommandContainer.printTable(tableName, columnNames[orderBy], ascending), sqlConnect);
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

        public void findRecords<T>(string tableName, string[] columnNames, int columnID, T value, string message = "Can't find a record")
        {
            try
            {
                using SqlConnection sqlConnect = new SqlConnection(this.m_connection);
                sqlConnect.Open();
                SqlCommand sqlCommand = null;
                if (typeof(T) == typeof(int))
                    sqlCommand = new SqlCommand(SQLCommandContainer.findRecord_Int(tableName, columnNames[columnID]), sqlConnect);
                else if (typeof(T) == typeof(string))
                    sqlCommand = new SqlCommand(SQLCommandContainer.findRecord_String(tableName, columnNames[columnID]), sqlConnect);
                else
                {
                    Console.WriteLine(message);
                    Logs.writeLog("Can't process this type of data: " + typeof(T));
                    return;
                }
                sqlCommand.Parameters.AddWithValue("@Val", value);

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
        public void findRecordByID(string tableName, string[] columnNames, int ID, string message = "Can't find a record")
        {
            try
            {
                using SqlConnection sqlConnect = new SqlConnection(this.m_connection);
                sqlConnect.Open();
                using SqlCommand sqlCommand = new SqlCommand(SQLCommandContainer.findRecordByID(tableName, columnNames[0], ID), sqlConnect);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foreach (string columnName in columnNames)
                        {
                            Console.WriteLine($"{columnName}: {reader[columnName]}");
                        }
                    }
                }
                else Console.WriteLine($"There are no records for id: {ID}");

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

        public void calculateArrears(int ID, string message = "Can't calculate belongings")
        {
            try{
                if (!executeQuaeryWithReturn<bool>(SQLCommandContainer.checkIfRecordExistUsingID(MyConstants.tableName_Arrears, MyConstants.columnNames_Arrears[0], ID), "Can't find Reader")){
                    Console.WriteLine("This reader has no arrears");
                    return;
                }
                else
                {
                    int belongings = MyConstants.arrearsRate * executeQuaeryWithReturn<int>(SQLCommandContainer.getDays(ID), "Can't calculate belongings");
                    Console.WriteLine($"Belongings for reader #{ID}: {belongings} zł");
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
