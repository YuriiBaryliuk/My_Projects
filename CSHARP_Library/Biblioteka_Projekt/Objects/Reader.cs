// Reader type of data that represent reader
// Inherited from Person
// Has 3 members that store:
    // Next reader id (special counter for IDs),
    // Reader ID,
    // Date of registration
    
// Has 1 method
// Has 1 constructor

using System;
using System.Text;

namespace Biblioteka_Projekt
{    
    internal class Reader : Person
    {
        static int nextID = 1;
        public int m_ID { get; private set; }
        public DateTime m_dateOfRegistration{ get; private set; }

        public Reader(string name, string surname, char gender, DateTime dateOfBirth, Address address, string phoneNumber, string email, DateTime dateOfRegistration) :
            base(name, surname, gender, dateOfBirth, address, phoneNumber, email)
        {
            m_dateOfRegistration = dateOfRegistration;
            m_ID = nextID++;
        }

        public override void printData()
        {
            Console.WriteLine($"ID: {m_ID}\nName: {m_name}\nSurname: {m_surname}\n" +
                $"Gender: {m_gender}\nAge: {m_age}\n" +
                $"Address: {m_address.printAddress()}\n" +
                $"Phone number: {m_phoneNumber}\n" +
                $"Email address: {m_email}\n" +
                $"Registration date: {m_dateOfRegistration.ToString()}\n");
        }
    }
}
