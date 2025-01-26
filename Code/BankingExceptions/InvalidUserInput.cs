using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BankingEnumeration;

namespace BankingExceptions
{
    public class InvalidUserInput : Exception
    {
        public readonly List<UserInputField> InvalidUserInputList = new List<UserInputField>();
        private string _text;

        public InvalidUserInput(List<UserInputField> invalidUserInputList) : base($"There are invalid fields.")
        {

            InvalidUserInputList.AddRange(invalidUserInputList);
            _text = "There next fields are invalid: ";
            AddInvalidUserInoutToMessage();
        }


        public InvalidUserInput(UserInputField invalidUserInput) : base($"There are invalid fields.")
        {
            InvalidUserInputList = new List<UserInputField>();
            InvalidUserInputList.Add( invalidUserInput );

            _text = "There next field is invalid: ";
            AddInvalidUserInoutToMessage();
        }

        private void AddInvalidUserInoutToMessage()
        {
            foreach (UserInputField InvalidFieldName in this.InvalidUserInputList)
                _text += InvalidFieldName.ToString() + ", ";

            _text = _text.Substring(0, _text.Length - 2); // Remove the last characterss ", "  from message string.
        }

        public string Text
        {
            get
            {      
                return _text;
            }

            
        }
    }
}
