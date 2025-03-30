using System;

namespace MyApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ProveAddUser(args);
        }

        public static void ProveAddUser(string[] args)
        {
            /*
			 *	DO NOT CHANGE THIS FILE AT ALL
        	*/

            var userService = new CustomerService();
            var addResult = userService.AddCustomer("Max", "Mustermann", "max.mustermann@gmail.com", new DateTime(1993, 1, 1), 4);
            System.Console.WriteLine("Adding Max Mustermann was " + (addResult ? "successful" : "unsuccessful"));
        }
    }
}
