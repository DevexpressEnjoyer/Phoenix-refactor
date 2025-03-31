using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public interface ICustomerValidator
    {
        bool Validate(Customer customer, Client client);
    }
    public class CustomerValidator : ICustomerValidator
    {
        private readonly ICreditAssignerSelector creditAssignerSelector;

        public CustomerValidator(ICreditAssignerSelector _creditAssignerSelector)
        {
            creditAssignerSelector = _creditAssignerSelector;
        }
        public bool Validate(Customer customer, Client client)
        {
            if (string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname))
            {
                return false;
            }

            if (!new EmailAddressAttribute().IsValid(customer.EmailAddress))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - customer.DateOfBirth.Year;
            if (now.Month < customer.DateOfBirth.Month || (now.Month == customer.DateOfBirth.Month && now.Day < customer.DateOfBirth.Day))
                age--;

            if (age < 18)
            {
                return false;
            }

            var assigmentType = creditAssignerSelector.GetAssignType(client);
            assigmentType.Assign(customer);

            if (customer.HasCreditLimit && customer.CreditLimit < 700)
            {
                return false;
            }

            return true;
        }
    }
}
