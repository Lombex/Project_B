using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirplaneApplicationTest
{
    [TestClass]
    public class T1
    {
        // T1 (REQ F4.1) -> Try to create account with a valid email and password, but also with invalid information. 
        [TestMethod]

        // use Console.In for input
        public void Check_if_input_null_gives_right_exception()
        {
            AccountsLogic account_logic = new AccountsLogic();
            try 
            {
                account_logic.CheckLogin(null, null);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Either email or password is null"));
            }
        }

        [TestMethod]
        public void Check_logout()
        {
            // start van een idee mr vgm werkt dit niet
            bool succeeded;
            process.StartInfo.RedirectStandardInput = true;
            MainSpace.Main();
            process.StandardInput.WriteLine("3");
            // Assert.

            /* example for timeouts:

            var task = Task.Run(() => SomeMethod(input));
            if (task.Wait(TimeSpan.FromSeconds(10)))
                return task.Result;
            else
                throw new Exception("Timed out");
            */
        }

        public void Check_for_creating_new_user_account()
        {
            UserLogin.MakeAccount(UserLogin.AccountType.User, true);
        }
    }
}