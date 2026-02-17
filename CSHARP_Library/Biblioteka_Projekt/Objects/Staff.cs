// Staff type of data that represent staff
// Inherited from Person
// Has 1 member that stores title of the worker
// Has 1 method
// Has 1 constructor

namespace Biblioteka_Projekt
{
    internal class Staff : Person
    {
        public string m_title{ get; private set; }
        public Staff(string name, string surname, string title) : base(name, surname)
        {
            this.m_name = name;
            this.m_surname = surname;
            this.m_title = title;
        }

        public override void printData()
        {
            Console.WriteLine($"Name: {this.m_name}\nSurname: {m_surname}\nTitle: {m_title}");
        }
    }
}
