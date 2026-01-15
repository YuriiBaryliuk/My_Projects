using Microsoft.Data.SqlClient;
using System.IO;

namespace Biblioteka_Projekt
{
    internal class SQLManager
    {
        private string m_connection;
        private bool initError = false;
        public bool getInitError() { return initError; }
        public SQLManager(string connection)
        {
            m_connection = connection;
            if(!checkDatabaseExist())
                initSQL();
            initError = checkTablesExist();
        }

        private void initSQL()
        {
            const string fileName = "CSLibrary_Create.sql";
            string command = "";
            if(IOFileManager.readCommand(fileName, ref command))
                initError = executeQuaery(command, "Can't initialize database");
        }

        private bool checkDatabaseExist()
        {
            bool databaseExists = false;
            string command = @"select count(*) 
                               from sys.databases
                               where name = 'CSLibrary'";
            databaseExists = executeQuaeryWithReturn<bool>(command, "Can't find database");
            return databaseExists;
        }
        private bool checkTablesExist()
        {
            const string fileName = "CheckTables.sql";
            string command = "";
            if (IOFileManager.readCommand(fileName, ref command))
                return executeQuaeryWithReturn<bool>(command, "Can't find tables");  // if all tables exists => 0
            return true;
        }


        private bool executeQuaery(string command, string message = "Message")
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
