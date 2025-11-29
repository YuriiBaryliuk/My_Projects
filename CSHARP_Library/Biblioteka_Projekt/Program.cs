using Biblioteka_Projekt;

internal class Program
{
    private static void Main(string[] args)
    {
        ReaderManager rm = new ReaderManager();
        rm.printReadersRegister();

        //Console.WriteLine("Start of adding Reader:");

        //string name, surname;
        //DateTime dateTime;
        //AddressStruct address;
        //string phoneNumber;

        //Console.Write("Enter a name: ");
        //name = Console.ReadLine();

        //Console.Write("Enter a surname: ");
        //surname = Console.ReadLine();

        //Console.WriteLine("Enter a date:");
        //dateTime = returnDate();

        //Console.WriteLine("Enter an address:");
        //address = returnAddress();

        //Console.Write("Enter phone number: ");
        //phoneNumber = Console.ReadLine();

        //Console.WriteLine();

        //rm.addToReadersRegister(new Reader(name, surname, dateTime, address, phoneNumber));
        //rm.addToReadersFileDB();

        //rm.printReadersRegister();

        InputManager.InputReader();

    }

    public static DateTime getDate()
    {
        int day, month, year;
        Console.Write("Day: ");
        day = Convert.ToInt16(Console.ReadLine());
        Console.Write("Month: ");
        month = Convert.ToInt16(Console.ReadLine());
        Console.Write("Year: ");
        year = Convert.ToInt16(Console.ReadLine());

        return new DateTime(year, month, day);
    }

    public static AddressStruct getAddress()
    {
        string streetName, houseNumber, flatNumber;
        Console.Write("Enter a street name: ");
        streetName = Convert.ToString(Console.ReadLine());
        
        Console.Write("Enter a house number: ");
        houseNumber = Convert.ToString(Console.ReadLine());
        
        Console.Write("Enter a flat number: ");
        flatNumber = Convert.ToString(Console.ReadLine());

        return new AddressStruct(streetName, houseNumber, flatNumber);
    }
}