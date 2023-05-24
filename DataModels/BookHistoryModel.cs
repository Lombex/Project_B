using System.Text.Json.Serialization;

namespace DataModels
{
    public class BookHistoryModel
    {
        [JsonPropertyName("AccountID")]
        public int AccountID { get; set; }

        [JsonPropertyName("Account-Name")]
        public string? AccountName { get; set; }

        [JsonPropertyName("Account-Email")]
        public string? AccountEmail { get; set; }

        [JsonPropertyName("BookTime")]
        public DateTime BookTime { get; set; }

        [JsonPropertyName("Booked-Airplane")]
        public string? BookedAirplane { get; set; }

        [JsonPropertyName("Booked-Seat")]
        public string? BookedSeat { get; set; }

        [JsonPropertyName("Booked-Destination")]
        public string? BookedDestination { get; set; }

        [JsonPropertyName("Booked-Gate")]
        public string? BookedGate { get; set; }

        [JsonPropertyName("DepartTime")]
        public DateTime DepartTime { get; set; }

        [JsonPropertyName("ArrivalTime")]
        public DateTime ArrivalTime { get; set; }

        public BookHistoryModel(int accountId, string accountname, string accountemail, DateTime booktime, string bookedairplane, string bookedseat, string bookeddestination, 
            string bookedgate, DateTime departtime, DateTime arrivaltime) 
        {
            AccountID = accountId;
            AccountName = accountname;
            AccountEmail = accountemail;     
            BookTime = booktime;
            BookedAirplane = bookedairplane;
            BookedSeat = bookedseat;
            BookedDestination = bookeddestination;
            BookedGate = bookedgate;
            DepartTime = departtime;
            ArrivalTime = arrivaltime;
        }
    }
}