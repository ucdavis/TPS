using System;

namespace NamSearch2.Helpers
{
    public class PhoneFormatter : IFormatProvider, ICustomFormatter
    {
        #region IFormatProvider Members

        public object GetFormat(Type formatType)
        {
            // Check if the class implements ICustomFormatter
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        #endregion IFormatProvider Members

        #region ICustomFormatter Members

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // if the passed in argument is null, return empty string
            if (arg == null)
            {
                return string.Empty;
            }
            // Get the value of argument in string
            string phoneNumber = arg.ToString();
            // Check if phone number has country code
            if (phoneNumber.StartsWith("+") && phoneNumber.IndexOf(' ') > 1)
            {
                // If it contains country code, separate it from phone number
                string countryCode = phoneNumber.Substring(0, phoneNumber.IndexOf(' ') + 1);
                phoneNumber = phoneNumber.Remove(0, countryCode.Length);
                // Get the formatted value of phone number and prefix it with the country code
                phoneNumber = string.Format("{0}{1}", countryCode, this.GetFormattedPhoneNumber(phoneNumber, format));
            }
            // Check if the phone number is a valid 10 digit number
            if (phoneNumber.Length == 10)
            {
                // Get the formatted value of phone number
                phoneNumber = this.GetFormattedPhoneNumber(phoneNumber, format);
            } return phoneNumber;
        }

        #endregion ICustomFormatter Members

        #region Helper method

        private string GetFormattedPhoneNumber(string phoneNumber, string format)
        {
            long number = 0;
            //Check if the phone number is a valid numeric value
            if (long.TryParse(phoneNumber, out number))
            {
                // If phone number is numeric, format it as per the passed in value
                phoneNumber = number.ToString(format);
            } return phoneNumber;
        }

        #endregion Helper method
    }
}