using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Product_Management_Core.Helpers
{
    public static class ValidationHelper
    {
        public static bool FechaIsValid(DateTime? dateTime)
        {
            var tomorrow = DateTime.Today.AddDays(1);
            return dateTime < tomorrow;
        }

        public static bool SetupEffectiveDateIsValid(DateTime? dateTime)
        {
            var firstDayOfMonth = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);

            return dateTime.Value.Day == firstDayOfMonth.Day;
        }

        public static bool IsEmailValid(this string field)
        {
            return EmailValidation().Match(field).Success;
        }

        private static Regex EmailValidation()
        {
            const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            // Set explicit regex match timeout, sufficient enough for email parsing
            // Unless the global REGEX_DEFAULT_MATCH_TIMEOUT is already set
            TimeSpan matchTimeout = TimeSpan.FromSeconds(2);

            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(pattern, options, matchTimeout);
                }
            }
            catch
            {
                // Fallback on error
            }

            // Legacy fallback (without explicit match timeout)
            return new Regex(pattern, options);
        }

        public static bool IsNull(this object field)
        {
            return field is null;
        }

        public static bool GenderIsValid(string gender)
        {
            if (gender is null) return false;

            var normalizedGender = gender.ToUpper();
            if (normalizedGender == "F" || normalizedGender == "M") return true;
            return false;
        }

        public static bool IsNumeric(string data)
        {
            try
            {
                return data.All(a => char.IsDigit(a));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool IsOne(this int transactionId)
        {
            return transactionId == 1;
        }

        public static bool TPDIsValid(int typePD)
        {
            return typePD == 1 || typePD == 2;
        }
        public static bool Required(object data)
        {
            return data != null;
        }

        public static bool EsPersonaMayor(this DateTime birthDate)
        {
            var today = DateTime.UtcNow;
            int age = today.Year - birthDate.Year;
            if (today < birthDate.AddYears(age))
                age--;
            return age >= 18;
        }


        public static bool EsPersonaMayor( IEnumerable<DateTime> birthDates)
        {
            var ok = true;

            var today = DateTime.UtcNow;
            foreach (DateTime birthDate in birthDates)
            {
                int age = today.Year - birthDate.Year;
                if (today < birthDate.AddYears(age))
                    age--;
                ok = age >= 18;
            }

            return ok;
        }
    }
}
