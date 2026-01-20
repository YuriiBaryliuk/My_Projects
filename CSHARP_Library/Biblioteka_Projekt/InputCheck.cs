using System;
using System.Globalization;
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

        public static bool checkPersonGender(string gender)
        {
            if (char.TryParse(gender, out char result))
                if (char.ToLower(result) == 'm' || char.ToLower(result) == 'f')
                    return true;
            return false;
        }
        public static bool checkDate(string year, string month = "01", string day = "01")
        {
            day = day.Length == 1 ? "0" + day : day;
            month = month.Length == 1 ? "0" + month : month;
            string fullDate = day + "-" + month + "-" + year;
            if (DateOnly.TryParseExact(fullDate, "dd-MM-yyyy", out DateOnly result))
                if (result <= DateOnly.FromDateTime(DateTime.Now))
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

        public static bool checkBookGenre(string genreID)
        {
            if (int.TryParse(genreID, out int result))
                if (result > 0 && result <= MyConstants.numberOfGenres)
                    return true;
            return false;
        }

        public static int checkIfNum(string strNum)
        {
            if (int.TryParse(strNum, out int result))
                return result;
            else
            {
                Logs.writeLog("Can not convert string to int");
                return 0;
            }
        }

        //private static void capitalizeFirstLetter(ref string param)
        //{
        //    param = char.ToUpper(param[0]) + param.Substring(1);
        //}
    }
}
