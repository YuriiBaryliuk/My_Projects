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
    }
}
