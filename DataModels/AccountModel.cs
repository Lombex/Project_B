using System.Text.Json.Serialization;
public class AccountModel
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("EmailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("Password")]
    public string Password { get; set; }

    [JsonPropertyName("FullName")]
    public string FullName { get; set; }

    [JsonPropertyName("IsAdmin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("IsEmployee")]
    public bool IsEmployee { get; set; }

    [JsonPropertyName("BookedFlights")]
    public List<List<string>> BookedFlights { get; set; }

    [JsonPropertyName("hasdisability")]
    public bool HasDisability { get; set; } // When a person has a disability

    // [JsonPropertyName("haschildren")]
    // public bool HasChildren { get; set; } // Check if user has children

    // [JsonPropertyName("childamount")]
    // public int ChildAmount { get; set; } // Amount of children getting booked



    public AccountModel(int id, string emailAddress, string password, string fullName, bool isemployee = false, bool isadmin = false, bool hasdisability = false)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        IsEmployee = isemployee;
        IsAdmin = isadmin;
        BookedFlights = new List<List<string>>();
        HasDisability = hasdisability;
        // HasChildren = haschildren;
        // ChildAmount = childamount;
    }
}