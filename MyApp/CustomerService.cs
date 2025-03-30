using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;

namespace MyApp
{
    public class CustomerService
    {
        private readonly CustomerValidator validator;
        private readonly ClientRepository clientRepository;


        //dependency injection
        public CustomerService(CustomerValidator _validator, ClientRepository _clientRepository)
        {
            validator = _validator;
            clientRepository = _clientRepository;
        }

        public bool AddCustomer(string furname, string surname, string email, DateTime dateOfBirth, int clientId)
        {

            var clientRepository = new ClientRepository();

            //What if client with selected Id does not exist? In long term I'd suggest implementing TryGetById
            //For now, GetById will throw exception with proper error message (proper way is also to create a new exception class - ClientNotFoundException)
            var client = clientRepository.GetById(clientId);

            var customer = new Customer
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = furname,
                Surname = surname
            };

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
