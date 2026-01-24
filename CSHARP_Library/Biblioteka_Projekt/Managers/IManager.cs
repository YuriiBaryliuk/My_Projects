namespace Biblioteka_Projekt
{
    internal interface IManager<T>
    {
        void inputAndSave<T>();
        void deleteAll<T>();
        int getLastId<T>();
    }
}
