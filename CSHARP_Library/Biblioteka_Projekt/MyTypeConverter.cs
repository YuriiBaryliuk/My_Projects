using System;
using System.Text;

namespace Biblioteka_Projekt
{
    static internal class MyTypeConverter
    {
        public static DateTime fromStringToDateTime(string str)
        {
            int day, month, year;

            string[] date = str.Split('.');
            day = Convert.ToInt16(date[0]);
            month = Convert.ToInt16(date[1]);
            year = Convert.ToUInt16(date[2]);
            return new DateTime(year, month, day);
        }
        public static AddressStruct fromStringToAddress(string str)
        {
            string[] address = str.Split(',');
            string streetName = address[0].Substring(1);
            string houseNumber = address[1].Substring(1);
            string flatNumber = address[2].Substring(1);
            return new AddressStruct(streetName, houseNumber, flatNumber);
        }
    }
}
