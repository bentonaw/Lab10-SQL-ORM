using Lab10.Data;
using Lab10.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lab10
{
    internal class Program
    {

        static void Main(string[] args)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                string option;
                while (true)
                {
                    Console.WriteLine("1. View all customers");
                    Console.WriteLine("2. Add a customer");
                    Console.WriteLine("3. Quit");
                    Console.Write(": ");
                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            ViewCustomerList(context);
                            break;
                        case "2":
                            AddCustomer(context);
                            break;
                        case "3":
                            break;

                        default:
                            Console.WriteLine("please choose an option between 1-3");
                            option = "loop";
                            break;

                    }
                }
            }

        }

        //method for viewing all customers and selecting one for more info and order info
        static void ViewCustomerList(NorthwindContext context)
        {
            Console.WriteLine("1. List A-Z");
            Console.WriteLine("2. List Z-A");
            int listOrder = int.Parse(Console.ReadLine());

            if (listOrder == 2)
            {
                var customerList = context.Customers
                    .Select(c => new { c.CompanyName, c.Country, c.Region, c.Phone, OrderCount = c.Orders.Count() })
                    .OrderByDescending(c => c.CompanyName)
                    .ToList();
                for (var c = 1; c < customerList.Count; c++)
                {
                    var customer = customerList[c];
                    Console.WriteLine($"{c}. Name: {customer.CompanyName}, Country: {customer.Country}, Region: {customer.Region}, Phone: {customer.Phone}, Orders: {customer.OrderCount}");
                };
            }
            else
            {
                var customerList = context.Customers
                    .Select(c => new { c.CompanyName, c.Country, c.Region, c.Phone, OrderCount = c.Orders.Count() })
                    .OrderBy(c => c.CompanyName)
                    .ToList();

                for (var c = 1; c < customerList.Count; c++)
                {
                    var customer = customerList[c];
                    Console.WriteLine($"{c}. Name: {customer.CompanyName}, Country: {customer.Country}, Region: {customer.Region}, Phone: {customer.Phone}, Orders: {customer.OrderCount}");
                };
            }
            // här börjar delen för att se all info för specifik kund
            Console.WriteLine();
            string customerSelect;
            Console.Write("Enter customer name to view more information: ");
            customerSelect = (Console.ReadLine());
            Console.Clear();
            var customerSelection = context.Customers
                .Where(cs => cs.CompanyName == customerSelect)
                .Select(cs => new { cs.CompanyName, cs.ContactName, cs.ContactTitle, cs.Address, cs.City, cs.Country, cs.Region, cs.PostalCode, cs.Phone, cs.Fax })
                .ToList();
            foreach (var cs in customerSelection)
            {
                Console.WriteLine(
                    $" Name: {cs.CompanyName}," +
                    $" Contact: {cs.ContactName}, {cs.ContactTitle}" +
                    $" Country: {cs.Country}," +
                    $" Region: {cs.Region}," +
                    $" City: {cs.City}," +
                    $" Address: {cs.Address}, {cs.PostalCode}" +
                    $" Phone: {cs.Phone}," +
                    $" Fax: {cs.Fax},");

            }
            var customerOrders = context.Customers
                .Where(cs => cs.CompanyName == customerSelect)
                .Include(cs => cs.Orders)
                .Single()
                .Orders
                .ToList();
            foreach (var cs in customerOrders)
            {
                Console.WriteLine($"OrderID: {cs.OrderId} Order Date: {cs.OrderDate}");
            }

            Console.WriteLine();
        }

        //method for adding a customer
        static void AddCustomer(NorthwindContext context)
        {

            Console.WriteLine("Please enter company name of the new customer:");
            string companyName = Console.ReadLine();

            while (string.IsNullOrEmpty(companyName))
            {
                Console.WriteLine("Company name cannot be empty please try again.");
                companyName = Console.ReadLine();
            }

            string customerID = companyName.Substring(0, 4);
            var CustomerIdList = context.Customers
                .Select(c => c.CustomerId)
                .ToList();
            while (CustomerIdList.Contains(customerID))
            {
                {
                    char randomChar = (char)('a' + new Random().Next(0, 26));
                    customerID.Remove(customerID.Length - 1);
                    customerID += randomChar;
                }
            }

            Console.Write("Enter the name of the contactperson: ");
            string contactName = Console.ReadLine()?.Trim(); //tar bort mellanrum både före, efter texten och ifall det är endast mellanrum
            contactName = string.IsNullOrEmpty(contactName) ? null : contactName; // ifall det är tomt blir det null

            Console.Write("Enter the title of the contact person: ");
            string contactTitle = Console.ReadLine()?.Trim();
            contactTitle = string.IsNullOrEmpty(contactTitle) ? null : contactTitle;

            Console.Write("Enter country: ");
            string country = Console.ReadLine()?.Trim();
            country = string.IsNullOrEmpty(country) ? null : country;

            Console.Write("Enter region: ");
            string region = Console.ReadLine()?.Trim();
            region = string.IsNullOrEmpty(region) ? null : region;

            Console.Write("Enter city: ");
            string city = Console.ReadLine()?.Trim();
            city = string.IsNullOrEmpty(city) ? null : city;

            Console.Write("Enter postal code: ");
            string postalCode = Console.ReadLine()?.Trim();
            postalCode = string.IsNullOrEmpty(postalCode) ? null : postalCode;

            Console.Write("Enter address: ");
            string address = Console.ReadLine()?.Trim();
            address = string.IsNullOrEmpty(address) ? null : address;

            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine()?.Trim();
            phoneNumber = string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber;

            Console.Write("Enter fax number: ");
            string faxNumber = Console.ReadLine()?.Trim();
            faxNumber = string.IsNullOrEmpty(faxNumber) ? null : faxNumber;

            //skapar ny kund med all ifylld data
            Customer customer = new Customer()
            {

                CompanyName = companyName,
                CustomerId = customerID,
                ContactName = contactName,
                ContactTitle = contactTitle,
                Country = country,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Address = address,
                Phone = phoneNumber,
                Fax = faxNumber,

            };
            context.Customers.Add(customer);
            context.SaveChanges();
            Console.WriteLine($"Customer was successfully added");
        }
    }
}



                    