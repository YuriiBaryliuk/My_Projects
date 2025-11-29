using System;
using System.Text;

namespace Biblioteka_Projekt
{
    public struct AddressStruct{
        public string s_streetName{ get; private set; }
        public string s_houseNumber{ get; private set; }
        public string s_flatNumber{ get; private set; }
        public AddressStruct(string streetName, string houseNumber, string flatNumber = "-")
        {
            s_streetName = streetName;
            s_houseNumber = houseNumber;
            s_flatNumber = flatNumber;
        }
        public string getAddress(){
            return $"Street name: {s_streetName}, House number: {s_houseNumber}, Flat number: {s_flatNumber}";
        }
    }
    internal class Person
    {
        public string m_name { get; private set; }
        public string m_surname { get; private set; }
        public DateTime m_dateOfBirth { get; private set; }
        public int m_age{ get; private set; }
        public AddressStruct m_address{ get; private set; }
        public string m_phoneNumber{ get; private set; }

        public Person(string name, string surname, DateTime dateOfBirth, AddressStruct address, string phoneNumber)
        {
            m_name = name;
            m_surname = surname;
            m_dateOfBirth = dateOfBirth;
            m_age = calculateAge();
            m_address = address;
            m_phoneNumber = phoneNumber;
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
            Console.WriteLine($"Name: {m_name}\nSurname: {m_surname}\nAge: {m_age}\n" +
                $"Address: {m_address.getAddress()}\n" +
                $"Phone number: {m_phoneNumber}\n");
        }
    }
}
