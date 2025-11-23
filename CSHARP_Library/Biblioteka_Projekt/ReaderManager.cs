using System;
using System.Text;
using System.IO;

namespace Biblioteka_Projekt
{
    internal class ReaderManager
    {
        List<Reader> readersRegister;
        public string readers_db_path{ get; private set; }
        public ReaderManager()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            readers_db_path = Path.Combine(root, "Readers_DB.txt");
            readersRegister = IOFileManager.initReaderRegFromFileDB(readers_db_path);
        }

        public void addToReadersRegister(Reader reader) => readersRegister.Add(reader);

        public void printReadersRegister()
        {
            foreach (Reader i in readersRegister)
                i.printData();
        }
    }
}
