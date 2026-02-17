// Supportive class that returns appropriate format of strings to save in database

using System.Reflection.PortableExecutable;

namespace Biblioteka_Projekt
{
    static internal class MyReformatting
    {
        public static string firstLetterToUpper(string origin) => 
            char.ToUpper(origin[0]) + origin[1..];
        public static string toPhoneNumberPL(string phone) =>
            "+48 " + phone[..3] + " " + phone.Substring(3, 3) + " " + phone[6..];
    }
}
