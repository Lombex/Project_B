using ConsoleTables;
public class ViewFlights
{
    public static void Menu()
    {
        Console.WriteLine("Welcome to Book a flight!");

        ConsoleTableOptions options = new ConsoleTableOptions
        {
            EnableCount = false
        };
        ConsoleTable Table = new ConsoleTable("Row", "", "", "", "", "", "", "", "");
        Table.Options.EnableCount = options.EnableCount;
        for (int row = 1; row < 31; row++)
        {
            Table.AddRow("", "A" + row, "B" + row, "", "C" + row, "D" + row, "", "E" + row, "F" + row);
        }

        Console.WriteLine(Table.ToString());

    }
    // Create method filter flight by catagory

    // Create mehtod view flight information
}