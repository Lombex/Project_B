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

    public AccountModel(int id, string emailAddress, string password, string fullName, bool isemployee = false, bool isadmin = false)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        IsEmployee = isemployee;
        IsAdmin = isadmin;
        BookedFlights = new List<List<string>>();
    }
}