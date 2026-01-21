using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class Person
    {
        public string m_name { get; protected set; }
        public string m_surname { get; protected set; }
        public char m_gender { get; private set; }
        public DateTime m_dateOfBirth { get; private set; }
        public int m_age{ get; private set; }
        public Address m_address{ get; private set; }
        public string m_phoneNumber{ get; private set; }
        public string m_email{ get; private set; }

        public Person(string name, string surname)
        {
            m_name = name;
            m_surname = surname;
        }
        public Person(string name, string surname, char gender, DateTime dateOfBirth, Address address, string phoneNumber, string email)
        {
            m_name = name;
            m_surname = surname;
            m_gender = gender;
            m_dateOfBirth = dateOfBirth;
            m_age = calculateAge();
            m_address = address;
            m_phoneNumber = phoneNumber;
            m_email = email;
        }

        private int calculateAge()
        {
            DateTime tempDate = new DateTime(m_dateOfBirth.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (m_dateOfBirth <= tempDate)
                return DateTime.Now.Year - m_dateOfBirth.Year;
            return DateTime.Now.Year - m_dateOfBirth.Year - 1;
        }

        public virtual void printData()
        {
            Console.WriteLine($"Name: {m_name}\nSurname: {m_surname}\n" +
                $"Gender: {m_gender}\nAge: {m_age}\n" +
                $"Address: {m_address.printAddress()}\n" +
                $"Phone number: {m_phoneNumber}\n" +
                $"Email Address: {m_email}");
        }
    }
}
