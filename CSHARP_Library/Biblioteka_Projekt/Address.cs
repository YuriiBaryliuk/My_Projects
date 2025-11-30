namespace Biblioteka_Projekt
{
    internal class Address
    {
        public string m_streetName{ get; private set; }
        public string m_houseNumber{ get; private set; }
        public string m_flatNumber{ get; private set; }
        public Address(string streetName, string houseNumber, string flatNumber = "-")
        {
            m_streetName = streetName;
            m_houseNumber = houseNumber;
            m_flatNumber = flatNumber;
        }
        public string printAddress(){
            return $"Street name: {m_streetName}, House number: {m_houseNumber}, Flat number: {m_flatNumber}";
        }

        public override string ToString()
        {
            return MyReformatting.firstLetterToUpper(m_streetName) + ", " + m_houseNumber.ToUpper() + ", " + m_flatNumber.ToUpper();
        }

        public static Address toAddress(string str)
        {
            string[] address = str.Split(',');
            string streetName = address[0];
            string houseNumber = address[1].Substring(1);
            string flatNumber = address[2].Substring(1);
            return new Address(streetName, houseNumber, flatNumber);
        }
    }
}
