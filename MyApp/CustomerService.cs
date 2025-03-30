using System;
using System.Drawing.Text;

namespace MyApp
{
    public class CustomerService
    {
        private CustomerValidator validator;
        public bool AddCustomer(string furname, string surname, string email, DateTime dateOfBirth, int clientId)
        {

            var clientRepository = new ClientRepository();

            //What if client with selected Id does not exist? In long term I'd suggest implementing TryGetById
            //For now, GetById will throw exception with proper error message.
            var client = clientRepository.GetById(clientId);

            var customer = new Customer
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = furname,
                Surname = surname
            };

            validator = new CustomerValidator();

            if (!validator.Validate(customer, client))
            {
                Console.WriteLine("Nieprawidłowe dane klienta.");
                return false;
            }

            CustomerDataAccess.AddCustomer(customer);

            return true;
        }
    }
}
