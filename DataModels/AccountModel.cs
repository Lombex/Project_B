using System.Text.Json.Serialization;

namespace DataModels
{
    public class AccountModel : IAccess
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("hasdisability")]
        public bool HasDisability { get; set; } // When a person has a disability

        [JsonPropertyName("haschildren")]
        public bool HasChildren { get; set; } // Check if user has children

        [JsonPropertyName("childamount")]
        public int ChildAmount { get; set; } // Amount of children getting booked

        public AccountModel(int id, string emailAddress, string password, string fullName, bool hasdisability, bool haschildren, int childamount)
        {
            ID = id;
            EmailAddress = emailAddress;
            Password = password;
            FullName = fullName;
            HasDisability = hasdisability;
            HasChildren = haschildren;
            ChildAmount = childamount;
        }
    }
}
