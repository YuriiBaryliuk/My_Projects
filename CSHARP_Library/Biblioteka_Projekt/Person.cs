using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class Person
    {
        public string m_name { get; private set; }
        public string m_surname { get; private set; }
        public DateOnly m_dateOfBirth { get; private set; }
        public int m_age{ get; private set; }
        public Address m_address{ get; private set; }
        public string m_phoneNumber{ get; private set; }
        public string m_email{ get; private set; }

        public Person(string name, string surname, DateOnly dateOfBirth, Address address, string phoneNumber, string email)
        {
            m_name = name;
            m_surname = surname;
            m_dateOfBirth = dateOfBirth;
            m_age = calculateAge();
            m_address = address;
            m_phoneNumber = phoneNumber;
            m_email = email;
        }

        private int calculateAge()
        {
            DateOnly tempDate = new DateOnly(m_dateOfBirth.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (m_dateOfBirth <= tempDate)
                return DateTime.Now.Year - m_dateOfBirth.Year;
            return DateTime.Now.Year - m_dateOfBirth.Year - 1;
        }

        public virtual void printData()
        {
            Console.WriteLine($"Name: {m_name}\nSurname: {m_surname}\nAge: {m_age}\n" +
                $"Address: {m_address.printAddress()}\n" +
                $"Phone number: {m_phoneNumber}\n" +
                $"Email Address: {m_email}");
        }
    }
}
