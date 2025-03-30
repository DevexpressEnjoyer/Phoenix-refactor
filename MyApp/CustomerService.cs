using System;

namespace MyApp
{
    public class CustomerService
    {
        public bool AddCustomer(string furname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(furname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            if (age < 18)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var customer = new Customer
                               {
                                   Client = client,
                                   DateOfBirth = dateOfBirth,
                                   EmailAddress = email,
                                   Firstname = furname,
                                   Surname = surname
                               };

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
                    creditLimit = creditLimit*3;
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

            CustomerDataAccess.AddCustomer(customer);

            return true;
        }
    }
}
