using ConsoleTables;
public class ViewFlights
{
    public static void Menu()
    {
        Console.WriteLine("Welcome to Book a flight!");
        ConsoleTable Table = new ConsoleTable("Column 1", "Column 2", "Column 3");
        Table.AddRow(1, 2, 3).AddRow("Test1", "Test2", "Test3");
        Console.WriteLine(Table);
    }
    // Create method filter flight by catagory

    // Create mehtod view flight information
}