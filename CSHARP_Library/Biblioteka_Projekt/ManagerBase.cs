namespace Biblioteka_Projekt
{
    internal abstract class ManagerBase<T> : IManager<T>
    {
        protected List<T> register;
        protected string pathToDB;
        protected ManagerBase(string filename)
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            pathToDB = Path.Combine(root, filename);
        }
        public void addToRegister(T obj) => register.Add(obj);
        public abstract void printRegister();
        public abstract void inputAndSave<T>();
        public abstract T getLastMember();

    }
}
