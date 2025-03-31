using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public interface ICreditAssigner
    {
        public void Assign(Customer customer);
    }

    public class VipCreditAssigner : ICreditAssigner
    {
        public void Assign(Customer customer)
        {
            customer.HasCreditLimit = false;
        }
    }

    public class ImportantCreditAssigner : ICreditAssigner
    {
        public void Assign(Customer customer)
        {
            customer.HasCreditLimit = true;
            using var userService = new CustomerServiceClient();
            var creditLimit = userService.GetLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
            customer.CreditLimit = creditLimit * 3;
        }
    }
    public class DefaultCreditAssigner : ICreditAssigner
    {
        public void Assign(Customer customer)
        {
            customer.HasCreditLimit = true;
            using var userService = new CustomerServiceClient();
            customer.CreditLimit = userService.GetLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
        }
    }

}
