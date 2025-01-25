using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankingEntities
{
    public static class UserValidator
    {
        /// <summary>
        /// Checks if the provided email is valid.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>True if the email is valid; otherwise, false.</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            // Use Regex to validate email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        /// Checks if the phone number is a valid Israeli phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>True if the phone number is valid; otherwise, false.</returns>
        public static bool IsValidIsraeliPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;

            // Israeli phone numbers typically start with 05X and have 10 digits
            string phonePattern = @"^05\d{8}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }

        /// <summary>
        /// Checks if the provided Israeli ID is valid.
        /// </summary>
        /// <param name="id">The ID to validate.</param>
        /// <returns>True if the ID is valid; otherwise, false.</returns>
        public static bool IsValidIsraeliID(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 9 || !int.TryParse(id, out _)) return false;
            return true;
        }
    }
}
