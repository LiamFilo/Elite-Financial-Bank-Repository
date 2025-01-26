using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingEntities;
using BankingEnumeration;
using System.Text.Json;
using BankingExceptions;

namespace BankingCommunication
{
	public class RegisterCommand : ICommand, IValidatableCommand
	{
		public readonly string FullName;
		public readonly string Id;
		public readonly string PhoneNumber;
		public readonly string Email;
		public readonly DateTime BirthDate;
        public readonly Address Address;
        public readonly EmployeeStatus Status;


        public RegisterCommand(string fullName, string id, string phoneNumber, string email, DateTime birthDate, Address address, EmployeeStatus status)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            BirthDate = birthDate;
            Address = address;
            Status = status;

            ExecutionDate = DateTime.Now; // Initialize ExecutionDate to the current date and time
            CommandType = ClientCommandType.Register; // Initialize CommandType to Register
        }

        public DateTime ExecutionDate { get; private set; }

        public ClientCommandType CommandType { get; private set; }

        public string ExportCommandsToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // Format the JSON for readability
            });
        }

        public void IsValidCommand()
        {
            List<UserInputField> invalidInputList = new List<UserInputField>();    
            if(UserValidator.IsValidIsraeliPhoneNumber(PhoneNumber))
                invalidInputList.Add(UserInputField.Phone_Number);
            if (UserValidator.IsValidEmail(Email))
                invalidInputList.Add(UserInputField.Email);
            if(UserValidator.IsValidIsraeliID(Id))
                invalidInputList.Add(UserInputField.Id);


            if (invalidInputList.Count != 0)
                throw new InvalidUserInput(invalidInputList);

        }
    }
}