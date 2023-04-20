using ConsoleTables;
public class ViewFlights
{
    public static void Menu()
    {
        Console.WriteLine("Welcome to Book a flight!");
        ConsoleTable Table = new ConsoleTable("Row", "A", "B", "", "C", "D", "", "E", "F");
        for (int row = 1; row < 30; row++)
        {
            Table.AddRow("Seat Number", row, row, "", row, row, "", row, 1).AddRow("TESt", "Test1", "Test2", "", "Test3", "Test3", "", "Test3", "Test3");
        }

        Console.WriteLine(Table);

    }
    // Create method filter flight by catagory

    // Create mehtod view flight information
}