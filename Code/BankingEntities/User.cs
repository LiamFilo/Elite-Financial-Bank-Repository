using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using BankingEntities;
using System.Text.RegularExpressions;
using BankingExceptions;
namespace BankingEntities {
	public class User {

		#region Data Members
		private BankAccounts<BankAccount> _accounts;
		private Address _address;
		private string _email;
		private string _fullName;
		private string _phoneNumber;
		public readonly string Id;


        #endregion
        

        #region Properties

        public string Email { get { return _email; } }
        public string FullName { get { return _fullName; } }
        public string PhoneNumber { get { return _phoneNumber; } }

		#endregion

		#region Constructors
		public User(Address address, string email, string id, string fullName, string phoneNumber)
		{
            if (!UserValidator.IsValidEmail(email)) throw new GeneralException("The email you enter is not valid is not valid");
            if (!UserValidator.IsValidIsraeliPhoneNumber(phoneNumber)) throw new GeneralException("The phone number you enter is not valid is not valid");
            if (!UserValidator.IsValidIsraeliID(id)) throw new GeneralException("The id you enter is not valid is not valid");

            this._address = address;
            this._fullName = fullName;
            this._phoneNumber = phoneNumber;
            this.Id = id;
		}

		#endregion

		#region Methods

		public void UpdateUserDetails(Address address = default(Address), string email = "", string fullName = "", string? phoneNumber = "")
		{
			email = email ?? "";
			phoneNumber = phoneNumber ?? "";

			if (address != default(Address)) this._address = address;
			if (email != "" && UserValidator.IsValidEmail(email)) this._email = email;
			if (fullName != "" ) this._fullName = fullName;
			if (phoneNumber != "" && UserValidator.IsValidIsraeliPhoneNumber(phoneNumber)) this._phoneNumber = phoneNumber;

		}

        public override string ToString()
        {
			return $"User: {this.FullName} - {this.Id}: Address: {this._address}, Phone Number: {this._phoneNumber}, Email: {this._email}";
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj is User user && this.GetHashCode().Equals(user.GetHashCode());
        }

		#endregion

	
}

}