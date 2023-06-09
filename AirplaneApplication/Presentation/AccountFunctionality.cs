using Project.Presentation;

public class AccountFunctionality
{
    public static string HidePassword()
    {
        string password = "";
        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter)
        {
            if (info.Key != ConsoleKey.Backspace)
            {
                Console.Write("*");
                password += info.KeyChar;
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    password = password.Substring(0, password.Length - 1);
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            }
            info = Console.ReadKey(true);
        }
        if (password == "q")
        {
            if (!Quit())
            {
                Console.Clear();
                Console.Write("Please enter your password\n>> ");
                password = HidePassword();
            }
        }
        Console.WriteLine();
        return password;
    }


    public static string GetInput(string prompt = "")
    {
        Console.Write(prompt + "\n>> ");
        string? input = Console.ReadLine();
        switch (input)
        {
            case "q":
                if (!Quit()) return GetInput(prompt);
                return GetInput(prompt);
            case "":
                return GetInput(prompt);
            default:
                return input!;
        }

    }

    private static bool Quit()
    {
        string Quit = GetInput("\nAre you sure you want to quit? (y/n)");
        switch (Quit)
        {
            case "y":
            case "Y":
                Console.WriteLine("Bye!");
                System.Environment.Exit(0);
                ErrorMessage("Program failed to exit. Close manually or try again.");
                return true;
            // unnecessary return because it'll have shut down but the compiler whines on
            default:
                return false;
        }
    }

    public static void ErrorMessage()
    {
        Console.Clear();
        PrintBanner();
        Console.WriteLine("\nInvalid input, please try again.\n(Press Enter to continue)");
        Console.ReadLine();
    }
    public static void ErrorMessage(string message)
    {
        Console.Clear();
        PrintBanner();
        Console.WriteLine("\n" + message + "\n(Press Enter wait 1 second)");
        Thread.Sleep(1000);
    }

    static private int banner_number = 1;
    static private List<string> banner_options = new List<string>()
    {
        " _____       _   _               _                            _      _ _  \n|  __ \\     | | | |             | |                     /\\   (_)    | (_) \n| |__) |___ | |_| |_ ___ _ __ __| | __ _ _ __ ___      /  \\   _ _ __| |_ _ __   ___  ___ \n|  _  // _ \\| __| __/ _ \\ '__/ _` |/ _` | '_ ` _ \\    / /\\ \\ | | '__| | | '_ \\ / _ \\/ __|\n| | \\ \\ (_) | |_| ||  __/ | | (_| | (_| | | | | | |  / ____ \\| | |  | | | | | |  __/\\_  \\\n|_|  \\_\\___/ \\__|\\__\\___|_|  \\__,_|\\__,_|_| |_| |_| /_/    \\_\\_|_|  |_|_|_| |_|\\___||___/",
        " ____   __  ____  ____  ____  ____  ____   __   _  _     __   __  ____  __    __  __ _  ____  ____ \r\n(  _ \\ /  \\(_  _)(_  _)(  __)(  _ \\(    \\ / _\\ ( \\/ )   / _\\ (  )(  _ \\(  )  (  )(  ( \\(  __)/ ___)\r\n )   /(  O ) )(    )(   ) _)  )   / ) D (/    \\/ \\/ \\  /    \\ )(  )   // (_/\\ )( /    / ) _) \\___ \\\r\n(__\\_) \\__/ (__)  (__) (____)(__\\_)(____/\\_/\\_/\\_)(_/  \\_/\\_/(__)(__\\_)\\____/(__)\\_)__)(____)(____/",
        "   __       _   _               _                     _   _      _ _                 \r\n  /__\\ ___ | |_| |_ ___ _ __ __| | __ _ _ __ ___     /_\\ (_)_ __| (_)_ __   ___  ___ \r\n / \\/// _ \\| __| __/ _ \\ '__/ _` |/ _` | '_ ` _ \\   //_\\\\| | '__| | | '_ \\ / _ \\/ __|\r\n/ _  \\ (_) | |_| ||  __/ | | (_| | (_| | | | | | | /  _  \\ | |  | | | | | |  __/\\__ \\\r\n\\/ \\_/\\___/ \\__|\\__\\___|_|  \\__,_|\\__,_|_| |_| |_| \\_/ \\_/_|_|  |_|_|_| |_|\\___||___/",
        "    ____        __  __                __                   ___    _      ___                \r\n   / __ \\____  / /_/ /____  _________/ /___ _____ ___     /   |  (_)____/ (_)___  ___  _____\r\n  / /_/ / __ \\/ __/ __/ _ \\/ ___/ __  / __ `/ __ `__ \\   / /| | / / ___/ / / __ \\/ _ \\/ ___/\r\n / _, _/ /_/ / /_/ /_/  __/ /  / /_/ / /_/ / / / / / /  / ___ |/ / /  / / / / / /  __(__  ) \r\n/_/ |_|\\____/\\__/\\__/\\___/_/   \\__,_/\\__,_/_/ /_/ /_/  /_/  |_/_/_/  /_/_/_/ /_/\\___/____/"
    };
    static public void PrintBanner(bool print_all_options = false)
    {
        if (print_all_options)
        {
            for (int i = 0; i < banner_options.Count(); i++)
            {
                Console.WriteLine($"Banner {i + 1}:\n{banner_options[i]}\n\n");
            }
            Console.WriteLine("\nCurrent Banner: ");
        }
        SetConsoleColor.WriteEmbeddedColorLine($"[blue]{banner_options[banner_number - 1]}[/blue]");
    }


    public static void ChangeBanner(int id)
    {
        if (id > banner_options.Count)
        {
            banner_number = banner_options.Count;
        }
        else if (id <= 0)
        {
            banner_number = 1;
        }
        else
        {
            banner_number = id;
        }
    }

    public static void Add_Data_Flight()
    {
        Admin admin = new Admin();
        string flight_number = AccountFunctionality.GetInput("Enter flight number")!;
        string aircraft = AccountFunctionality.GetInput("Enter Aircraft");
        string origin = AccountFunctionality.GetInput("Enter origin");
        string destination = AccountFunctionality.GetInput("Enter destination");
        string date = AccountFunctionality.GetInput("Enter Date");
        double flighttime = Convert.ToDouble(AccountFunctionality.GetInput("Enter flighttime (in hours) [ format: 0.1 ]"));
        string departtime = AccountFunctionality.GetInput("Enter depart time (local)");
        string arrivaltime = AccountFunctionality.GetInput("Enter arrival time (local)");
        string gate = AccountFunctionality.GetInput("Enter gate")!;
        int price = Convert.ToInt32(AccountFunctionality.GetInput("Enter standard price of the flight"));
        admin.Add_flight(flight_number, aircraft, origin, destination, date, flighttime, departtime, arrivaltime, price, gate);
    }
}