namespace Biblioteka_Projekt
{
    internal class BookManager
    {
        List<Book> booksRegister;
        public string books_db_path{ get; private set; }
        public BookManager()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            books_db_path = Path.Combine(root, "Books_DB.txt");     //?
            booksRegister = IOFileManager.initBookRegFromFileDB(books_db_path);
        }
        public void addToBooksRegister(Book book) => booksRegister.Add(book);
        public Book? getLastBook()
        {
            if (booksRegister.Count != 0)
                return booksRegister.ElementAt(booksRegister.Count - 1);
            else{
                Logs.writeLog("Trying to get last element from Books register. Books register is empty.");  //?
                Console.WriteLine("Books register is empty");   //?
                return null;
            }
        }
        public void printBooksRegister()
        {
            foreach (Book i in booksRegister)   //bookRegister?
                i.printData();
        }
        public void inputBook()
        {
            InputManager.InputBook(this);   //InputBook
        }
        public void loadBookToFile(Book book)
        {
            IOFileManager.writeBookToFile(books_db_path, book);
        }
    }
}
