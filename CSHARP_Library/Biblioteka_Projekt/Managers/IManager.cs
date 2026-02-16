// Interface defines methods for managers to work with databases
namespace Biblioteka_Projekt
{
    internal interface IManager<T>
    {
        // Meanings for every manager:
        // Needs to implement method to read user's input and provide saving
        void inputAndSave<T>();

        // Needs to implement method that will delete all records from targeted database
        void deleteAll<T>();

        // Needs to implement method that will return id of last added element
        int getLastId<T>();
    }
}
