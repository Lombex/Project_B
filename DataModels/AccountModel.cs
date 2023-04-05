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

    // [JsonPropertyName("hasdisability")]
    // public bool HasDisability { get; set; } // When a person has a disability

    // [JsonPropertyName("haschildren")]
    // public bool HasChildren { get; set; } // Check if user has children

    // [JsonPropertyName("childamount")]
    // public int ChildAmount { get; set; } // Amount of children getting booked

    public AccountModel(int id, string emailAddress, string password, string fullName, bool isadmin = false)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        IsAdmin = isadmin;  
        //IsAdmin = adminpermissions;
        // HasDisability = hasdisability;
        // HasChildren = haschildren;
        // ChildAmount = childamount;
    }
}

