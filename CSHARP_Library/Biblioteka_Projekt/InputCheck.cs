using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Biblioteka_Projekt
{
    internal class InputCheck
    {
        const string regexNamePattern = @"^[A-Za-zŁŚŻąćęłńóśżź]+$";
        const string regexSurnamePattern = @"^[A-Za-zĆŁŚŻąćęłńóśżź]{2,}$";
        const string regexStreetNamePattern = @"^[A-Za-z0-9\s,.\-/]{2,100}$";
        const string regexHouseFlatPattern = @"^[A-Za-z0-9ĆŁŚŻąćęłńóśżź]{1,10}$";
        const string regexPhonePattern = @"^\d{9}$";

        public static bool checkPersonName(string name)
        {
            name = char.ToUpper(name[0]) + name.Substring(1);
            if(Regex.IsMatch(name, regexNamePattern))
                return true;
            return false;
        }
        public static bool checkPersonSurame(string surname)
        {
            surname = char.ToUpper(surname[0]) + surname.Substring(1);
            if(Regex.IsMatch(surname, regexSurnamePattern))
                return true;
            return false;
        }
        public static bool checkPersonDate(string year, string month, string day)
        {
            string fullDate = year + "-" + month + "-" + day;
            if (DateOnly.TryParse(fullDate, out DateOnly res))
                return true;
            return false;
        }
        public static bool checkPersonAddress(string streetName, string houseNumber, string flatNumber)
        {
            if (Regex.IsMatch(streetName, regexStreetNamePattern) 
                || Regex.IsMatch(houseNumber, regexHouseFlatPattern)
                || Regex.IsMatch(flatNumber, regexHouseFlatPattern))
                return true;
            return false;
        }
        public static bool checkPersonPhone(string phone)
        {
            if(Regex.IsMatch(phone, regexPhonePattern))
                return true;
            return false;
        }

        //private static void capitalizeFirstLetter(ref string param)
        //{
        //    param = char.ToUpper(param[0]) + param.Substring(1);
        //}
    }
}
