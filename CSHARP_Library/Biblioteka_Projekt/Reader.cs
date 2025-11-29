using System;
using System.Text;

namespace Biblioteka_Projekt
{    
    internal class Reader : Person
    {
        static int nextID = 1;
        public int m_ID { get; private set; }
        public DateTime m_dateOfRegistration{ get; private set; }

        public Reader(string name, string surname, DateTime dateOfBirth, AddressStruct address, string phoneNumber) :
            base(name, surname, dateOfBirth, address, phoneNumber)
        {
            m_dateOfRegistration = DateTime.Now;
            m_ID = nextID++;
        }

        public override void printData()
        {
            Console.WriteLine($"ID: {m_ID}\nName: {m_name}\nSurname: {m_surname}\nAge: {m_age}\n" +
                $"Address: {m_address.getAddress()}\n" +
                $"Phone number: {m_phoneNumber}\n" +
                $"Registration date: {m_dateOfRegistration.Day}.{m_dateOfRegistration.Month}.{m_dateOfRegistration.Year}\n");
        }
    }
}
