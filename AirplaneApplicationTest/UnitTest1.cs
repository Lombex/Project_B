using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AirplaneApplicationTest;

namespace AirplaneApplicationTest
{
    [TestClass]
    public class T1
    {

        // T1 (REQ F4.1) -> Try to create account with a valid email and password, but also with invalid information.
        [TestMethod]
        public void Check_if_input_null_gives_right_exception()
        {
            try
            {
                AccountsLogic.CheckLogin(null, null);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Either email or password is null"));
            }
        }
        [TestMethod]
        [DataRow("Password1$")]
        public void PasswordCheck_ValidPassword_ReturnsTrue(string password)
        {

            bool result = UserLogin.PasswordCheck(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("passw")]
        [DataRow("passwordpasswordpasswordpasswordpasswordpasswordpasswordpasswordpassword")]
        [DataRow("Password1")]
        [DataRow("password1$")]
        [DataRow("PASSWORD1$")]
        [DataRow("Pass word1$")]
        [DataRow("Password$")]
        public void PasswordCheck_ReturnsFalse(string password)
        {
            bool result = UserLogin.PasswordCheck(password);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("example@gmail.com")]
        public void SanitizeEmailValidator_ValidEmail_ReturnsTrue(string email)
        {
            bool result = UserLogin.SanitizeEmailValidator(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("invalidemail")]
        [DataRow("example@invalid")]
        [DataRow("invalid@example")]
        [DataRow("invalid@example.")]
        public void SanitizeEmailValidator_ValidEmail_ReturnsFalse(string email)
        {
            bool result = UserLogin.SanitizeEmailValidator(email);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("ABC123", "Boeing 747", "City A", "City B", "15-07-2023", 2.5, "10:00", "12:30", 200, "A1")]
        public void AddFlight_ShouldAddFlightToDataListAndWriteAll(string flight_number, string aircraft, string origin, string destination, string date, double flighttime, string departtime, string arrivaltime, int price, string gate)
        {
            FlightInfoAccess flightinfoAccess = new FlightInfoAccess();
            List<FlightInfoModel> flight = flightinfoAccess.LoadAll();

            Admin admin = new Admin();
            admin.Add_flight(flight_number, aircraft, origin, destination, date, flighttime, departtime, arrivaltime, price, gate);

            Assert.IsTrue(flight.Any(f => f.FlightNumber == flight_number));
        }

    }
}






