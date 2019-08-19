using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DesktopAppSample4.Classes
{
    class AppValidator
    {
        private static AppValidator appValObj;
        public static AppValidator getValidatorInstance()
        {
            if (appValObj == null)
                appValObj = new AppValidator();

            return appValObj;
        }
       
        public bool isValidField(String text, int identifier)
        {
            bool isValid = false;

            switch (identifier)
            {
                case 1:
                    isValid = Regex.Match(text, @"^[0-9]+[a-zA-Z]+.{3,25}$").Success; //Username
                    break;

                    //At least one upper case english letter,
                    //At least one lower case english letter,
                    //At least one digit,
                    //At least one special character, Minimum 8 and maximum of 15 in length
                case 8:
                    isValid = Regex.Match(text, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,25}$").Success; //Password
                    break;
            }
            return isValid;
        }
    }
}
