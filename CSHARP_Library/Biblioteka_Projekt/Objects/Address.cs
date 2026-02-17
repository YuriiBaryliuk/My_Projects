// Address type of data that represent real-word addresses
// Has 4 members that store: Street name, House number, Flat number, City
// Has 4 methods
// Has 1 constructor
namespace Biblioteka_Projekt
{
    internal class Address
    {
        public string m_streetName{ get; private set; }
        public string m_houseNumber{ get; private set; }
        public string m_flatNumber{ get; private set; }
        public string m_city{ get; private set; }
        public Address(string city, string streetName, string houseNumber, string flatNumber = "-")
        {
            m_streetName = streetName;
            m_houseNumber = houseNumber;
            m_flatNumber = flatNumber;
            m_city = city;
        }
        public string printAddress(){
            return $"Street name: {m_streetName}, House number: {m_houseNumber}, Flat number: {m_flatNumber}, City: {m_city}";
        }

        // Method is using to store address data into file database system
        public override string ToString()
        {
            return m_streetName + ", " + 
                m_houseNumber + ", " + 
                m_flatNumber + ", " + 
                m_city;
        }

        // Method is using to store address data into SQL database system
        public string toSqlString()
        {
            return m_streetName + ", " +
                m_houseNumber + ", " +
                m_flatNumber;
        }

        // Method that converts string address into object (using in file database)
        public static Address toAddress(string str)
        {
            string[] address = str.Split(',');
            string streetName = address[0];
            string houseNumber = address[1].Substring(1);
            string flatNumber = address[2].Substring(1);
            string city = address[3].Substring(1);
            return new Address(city, streetName, houseNumber, flatNumber);
        }
    }
}
