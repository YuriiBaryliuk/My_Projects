// Manager for Book objects that works with file database for books
namespace Biblioteka_Projekt
{
    internal class BookManager : ManagerBase<Book>
    {
        public BookManager() : base(MyConstants.file_BooksDB)
        {   
            // Initialize inner register for books
            // Register is List type
            // Register is initialized from file database for books and will contain all books as a Book objects
            register = IOFileManager.initBookRegFromFileDB(pathToDB);
        }

        // Method prints initialized register
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

        // Get last member from register list
        public override Book getLastMember()
        {
            if (register.Count != 0)
                return register.Last();
            return null;
        }

        // Method directs action of:
        // (1) User input for adding a book
        // (2) Saving it in a register (as a last member) and file database
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

        // Deletes all books from the register and file DB
        public override void deleteAll<Book>()
        {
            this.register.Clear();
            IOFileManager.clearAllFile(pathToDB);
        }

        // Gets an id of the last member in the register
        public override int getLastId<Book>()
        {
            if (register.Count != 0)
                return register.Last().m_ID;
            return 0;
        }
    }
}
