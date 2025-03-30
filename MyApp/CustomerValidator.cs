using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class CustomerValidator
    {
        public bool Validate(Customer customer, Client client)
        {
            if (string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname))
            {
                return false;
            }

            if (!customer.EmailAddress.Contains("@") && !customer.EmailAddress.Contains("."))
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

            if (client.Name == "VIP")
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (client.Name == "Important")
            {
                // Do credit check and double credit limit
                customer.HasCreditLimit = true;
                using (var userService = new CustomerServiceClient())
                {
                    var creditLimit = userService.GetLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    creditLimit = creditLimit * 3;
                    customer.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Do credit check
                customer.HasCreditLimit = true;
                using (var userService = new CustomerServiceClient())
                {
                    var creditLimit = userService.GetLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    customer.CreditLimit = creditLimit;
                }
            }

            if (customer.HasCreditLimit && customer.CreditLimit < 700)
            {
                return false;
            }

            return true;
        }
    }
}
