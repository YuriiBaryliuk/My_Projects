// Manager for Reader objects that works with file database for readers
namespace Biblioteka_Projekt
{
    internal class ReaderManager : ManagerBase<Reader>
    {
        public ReaderManager() : base(MyConstants.file_ReaderDB)
        {
            // Initialize inner register for readers
            // Register is List type
            // Register is initialized from file database for readers and will contain all readers as Reader objects
            register = IOFileManager.initReaderRegFromFileDB(pathToDB);
        }

        // Method prints initialized register
        public override void printRegister()
        {
            if (register.Count != 0)
                foreach (Reader i in register)
                    i.printData();
            else
            {
                Logs.writeLog("Trying to get elements from Readers register. Readers register is empty.");
                Console.WriteLine("Readers register is empty");
            }
        }

        // Get last member from register list
        public override Reader getLastMember()
        {
            if (register.Count != 0)
                return register.Last();
            return null;
        }
        // Method directs action of:
        // (1) User input for adding a reader
        // (2) Saving it in a register (as a last member) and file database
        public override void inputAndSave<Reader>()
        {
            try{
            addToRegister(InputManager.InputReader());
            IOFileManager.writeReaderToFile(pathToDB, getLastMember());
            }catch(Exception e)
            {
                Logs.writeLog(e.Message);
                Console.WriteLine("Can't write a reader to Database");
            }
        }

        // Deletes all readers from the register and file DB
        public override void deleteAll<Reader>()
        {
            this.register.Clear();
            IOFileManager.clearAllFile(pathToDB);
        }

        // Gets an id of the last member in the register
        public override int getLastId<Reader>()
        {
            if (register.Count != 0)
                return register.Last().m_ID;
            return 0;
        }
    }
}
