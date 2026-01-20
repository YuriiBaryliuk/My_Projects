namespace Biblioteka_Projekt
{
    internal class BookManager : ManagerBase<Book>
    {
        public BookManager() : base(MyConstants.file_BooksDB)
        {
            register = IOFileManager.initBookRegFromFileDB(pathToDB);
        }
        public override void printRegister()
        {
            if (register.Count != 0)
                foreach (Book i in register)
                    i.printData();
            else
            {
                Logs.writeLog("Trying to get elements from Books register. Books register is empty.");  //?
                Console.WriteLine("Books register is empty");
            }
        }
        public override Book getLastMember()
        {
            if (register.Count != 0)
                return register.Last();
            return null;
        }

        public override void inputAndSave<Book>()
        {
            try{
            addToRegister(InputManager.InputBook());
            IOFileManager.writeBookToFile(pathToDB, getLastMember());
            }catch(Exception e)
            {
                Logs.writeLog(e.Message);
                Console.WriteLine("Can't write a book to Database");
            }
        }

        public override void deleteAll<Book>()
        {
            this.register.Clear();
            IOFileManager.clearAllFile(pathToDB);
        }

        public override int getLastId<Book>()
        {
            if (register.Count != 0)
                return register.Last().m_ID;
            return 0;
        }
    }
}
