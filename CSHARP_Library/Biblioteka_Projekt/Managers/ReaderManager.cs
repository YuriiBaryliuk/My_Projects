namespace Biblioteka_Projekt
{
    internal class ReaderManager : ManagerBase<Reader>
    {
        public ReaderManager() : base(MyConstants.file_ReaderDB)
        {
            register = IOFileManager.initReaderRegFromFileDB(pathToDB);
        }
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

        public override Reader getLastMember()
        {
            if (register.Count != 0)
                return register.Last();
            return null;
        }
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
        public override void deleteAll<Reader>()
        {
            this.register.Clear();
            IOFileManager.clearAllFile(pathToDB);
        }
        public override int getLastId<Reader>()
        {
            if (register.Count != 0)
                return register.Last().m_ID;
            return 0;
        }
    }
}
