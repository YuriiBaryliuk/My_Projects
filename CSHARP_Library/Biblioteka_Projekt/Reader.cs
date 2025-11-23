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
    internal class Reader
    {
        static int nextID = 1;
        public int m_ID { get; private set; }
        public string m_name { get; private set; }
        public string m_surname { get; private set; }
        DateTime m_dateOfBirth;
        public int m_age{ get; private set; }
        public AddressStruct m_address{ get; private set; }
        public string m_phoneNumber{ get; private set; }
        public DateTime m_dateOfRegistration{ get; private set; }

        public Reader(string name, string surname, DateTime dateOfBirth, AddressStruct address, string phoneNumber)
        {
            m_name = name;
            m_surname = surname;
            m_dateOfBirth = dateOfBirth;
            m_age = calculateAge();
            m_address = address;
            m_phoneNumber = phoneNumber;
            m_dateOfRegistration = DateTime.Now;
            m_ID = nextID++;
        }

        public void printData()
        {
            Console.WriteLine($"ID: {m_ID}\nName: {m_name}\nSurname: {m_surname}\nAge: {m_age}\n" +
                $"Address: {m_address.getAddress()}\n" +
                $"Phone number: {m_phoneNumber}\n" +
                $"Registration date: {m_dateOfRegistration.Day}.{m_dateOfRegistration.Month}.{m_dateOfRegistration.Year}\n");
        }

        private int calculateAge()
        {
            DateTime tempDate = new DateTime(m_dateOfBirth.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (m_dateOfBirth <= tempDate)
                return DateTime.Now.Year - m_dateOfBirth.Year;
            return DateTime.Now.Year - m_dateOfBirth.Year - 1;
        }
    }
}
