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
        //public Reader? getLastReader()
        //{
        //    if (readersRegister.Count != 0)
        //        return readersRegister.ElementAt(readersRegister.Count - 1);
        //    else{
        //        Logs.writeLog("Trying to get last element from Readers register. Readers register is empty.");
        //        Console.WriteLine("Readers register is empty");
        //        return null;
        //    }
        //}

        public void printReadersRegister()
        {
            if (readersRegister.Count != 0)
                foreach (Reader i in readersRegister)
                    i.printData();
            else
            {
                Logs.writeLog("Trying to get elements from Readers register. Readers register is empty.");
                Console.WriteLine("Readers register is empty");
            }
        }

        public void inputReader()
        {
            InputManager.InputReader(this);
        }

        public void loadReaderToFile(Reader reader)
        {
            IOFileManager.writeReaderToFile(readers_db_path, reader);
        }
    }
}
