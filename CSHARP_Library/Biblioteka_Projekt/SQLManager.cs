using Microsoft.Data.SqlClient;
using System.IO;

namespace Biblioteka_Projekt
{
    internal class SQLManager
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
        }

        private void initSQL()  // Database and Tables creation and initialization
        {
            string TableCreationCommand = "";
            string TableInsertionCommand = "";
            executeQuaery(SQLCommandContainer.createDB(), "Can't create Database"); // Database creation
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


        private bool executeQuaery(string command, string message = "Message")  // true if everything is okay
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(this.m_connection)){
                sqlConnect.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnect))
                        sqlCommand.ExecuteNonQuery();
            }
            }catch(SqlException ex){
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
                using (SqlConnection sqlConnect = new SqlConnection(this.m_connection)){
                sqlConnect.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(command, sqlConnect))
                        myVariable = (T)Convert.ChangeType(sqlCommand.ExecuteScalar(), typeof(T));
            }
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
        //private bool checkIfDBExists()
        //{
        //    bool isExists = false;
        //    try
        //    {
        //    using (SqlConnection sqlConnect = new SqlConnection(this.m_connection)){
        //        sqlConnect.Open();
        //        string myCommand = @"select count(*) 
        //                            from sys.databases
        //                            where name = 'CSLibrary'";

        //        using (SqlCommand sqlCommand = new SqlCommand(myCommand, sqlConnect))
        //            isExists = Convert.ToBoolean(sqlCommand.ExecuteScalar());
        //    }
        //    }catch(SqlException ex){
        //        Logs.writeLog("Can not check Database: " + ex.Message);
        //        Console.WriteLine("Can not check Database");
        //        this.initError = true;
        //    }catch(Exception ex)
        //    {
        //        Logs.writeLog("Can not check Database: " + ex.Message);
        //        Console.WriteLine("Can not check Database");
        //        this.initError = true;
        //    }
        //    return isExists;
        //}

        
    }
}
