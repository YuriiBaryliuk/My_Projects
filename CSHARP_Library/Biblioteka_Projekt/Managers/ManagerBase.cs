// Abstract class that provides functionality for file-database managers
namespace Biblioteka_Projekt
{
    internal abstract class ManagerBase<T> : IManager<T>
    {
        // Member of type List<T> that will store objects of type T
        protected List<T> register;

        // Member that will store path to file-database
        protected string pathToDB;
        protected ManagerBase(string filename)
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            pathToDB = Path.Combine(root, filename);
        }

        // Adds object to register
        public void addToRegister(T obj) => register.Add(obj);
        
        // Prints all members of a register
        public abstract void printRegister();

        // Definition of method that needs to provide input from user and save to file functionality (from IManager)
        public abstract void inputAndSave<T>();

        // Definition of method that needs to return last added to register T object
        public abstract T getLastMember();

        // Definition of method that needs to provide deletion of all members in file (from IManager)
        public abstract void deleteAll<T>();

        // Definition of method that needs to return an ID of last added register's object (from IManager)
        public abstract int getLastId<T>();
    }
}
