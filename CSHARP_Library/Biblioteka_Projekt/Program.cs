using Biblioteka_Projekt;

internal class Program
{
    private static void Main(string[] args)
    {
        ReaderManager rm = new ReaderManager();
        rm.printReadersRegister();

        rm.inputReader();
        rm.printReadersRegister();
    }

}