using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Biblioteka_Projekt
{
    internal class InputCheck
    {
        const string regexNamePattern = @"^[A-Za-zŁŚŻąćęłńóśżź]+$";
        const string regexSurnamePattern = @"^[A-Za-zĆŁŚŻąćęłńóśżź]{2,}$";
        const string regexStreetNamePattern = @"^[A-Za-z0-9ĆŁŚŻąćęłńóśżź\s,.\-/]{2,100}$";
        const string regexHouseFlatPattern = @"^[A-Za-z0-9ĆŁŚŻąćęłńóśżź]{1,10}$";
        const string regexPhonePattern = @"^\d{9}$";
        const string regexAuthorPattern = @"^[A-Za-z0-9\s,.\-/&]{2,100}$";
        const string regexReleaseYearPattern = @"^\d{1,4}$";
        const string regexEmailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

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
        public static bool checkDate(string year, string month = "01", string day = "01")
        {
            string fullDate = year + "-" + month + "-" + day;
            if (DateOnly.TryParse(fullDate, out DateOnly res))
                if (res <= DateOnly.FromDateTime(DateTime.Now))
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

        public static bool checkPersonEmail(string email)
        {
            if (Regex.IsMatch(email, regexEmailPattern))
                return true;
            return false;
        }

        public static bool checkAuthorName(string author)
        {
            if(Regex.IsMatch(author, regexAuthorPattern))
                return true;
            return false;
        }

        public static bool checkBookYear(string year)
        {
            if(Regex.IsMatch(year, regexReleaseYearPattern))
                if (Convert.ToInt32(year) <= DateTime.Now.Year)
                    return true;
            return false;
        }

        //private static void capitalizeFirstLetter(ref string param)
        //{
        //    param = char.ToUpper(param[0]) + param.Substring(1);
        //}
    }
}
