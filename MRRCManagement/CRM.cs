using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRCManagement
{
    /// <summary>
    /// The CRM class provides us with information about customer resource management
    /// 
    /// Author Romeet Puhar May 2020
    /// 
    /// </summary>    
    public class CRM
    {
        // Location of CSV file
        private string crmFile;

        // Automatic intialised variable
        private List<Customer> customers { get; set; }

        /// <summary>
        /// Intialises a new instance of the CRM class
        /// </summary>
        /// 
        /// <param name="crmFile"> Location of the CSV file to be read </param>
        public CRM(string crmFile)
        {
            if(File.Exists(crmFile))
            {
                this.crmFile = crmFile;
            }
            else
            {
                this.crmFile = @"..\..\..\Data\customers.csv";
            }

            // Intialises new list of customers
            customers = new List<Customer>();

            // Reads CSV file
            LoadFromFile();
        } //end of CRM(string crmFile)

        /// <summary>
        /// Adds a customer to the list of customers
        /// 
        /// Code inspired by https://stackoverflow.com/questions/3435089/how-to-check-if-object-already-exists-in-a-list/3435099#3435099, modified by me (Romeet Puhar) for assignment purposes
        /// </summary>
        /// 
        /// <param name="customer"> Instance of a customer </param>
        /// 
        /// <returns> Returns true if the customer was added, false otherwise </returns>        
        public bool AddCustomer(Customer customer)
        {
            // Checks if the customer we want to add has the same ID as any other customers
            bool containsItem = customers.Any(item => item.customerID == customer.customerID);

            if (!containsItem)
            {
                // Adds customer if no other similar IDs
                customers.Add(customer);
                return true;
            }
            else
            {
                CRM crm = new CRM(crmFile);
                crm.Table();
                Console.WriteLine("\n\nError: A customer with this ID already exists. Please use a different ID.");
                return false;
            }
        } //end of AddCustomer(Customer customer)

        /// <summary>
        /// Removes a customer from the list of customers if not currently renting a vehicle
        /// 
        /// Code inspired by https://stackoverflow.com/questions/3435089/how-to-check-if-object-already-exists-in-a-list/3435099#3435099, modified by me (Romeet Puhar) for assignment purposes
        /// </summary>
        /// 
        /// <param name="ID"> Instance of a customer's ID </param>
        /// <param name="fleet"> Instance of the list of vehicles </param>
        /// 
        /// <returns> Returns true if the customer was removed, false otherwise </returns>
        public bool RemoveCustomer(int ID, Fleet fleet)
        {
            // If the customer's ID is not in the vehicle database for someone renting a car, remove the customer
            if (!fleet.IsRenting(ID))
            {
                customers.Remove(GetCustomer(ID));
                return true;
            }
            else
            {
                return false;
            }
        } //end of RemoveCustomer(int ID, Fleet fleet)

        /// <summary>
        /// Returns an instance of a customer by their ID
        /// 
        /// Code inspired by https://stackoverflow.com/a/9854944, modified by me (Romeet Puhar) for assignment purposes
        /// </summary>
        /// 
        /// <param name="ID"> Instance of a customer's ID </param>
        /// 
        /// <returns> Returns an instance of a customer by their ID </returns>
        public Customer GetCustomer(int ID)
        {
            
            Customer customer = customers.Find(x => x.customerID == ID);

            if (customer != null)
            {
                return customer;
            } 
            else
            {
                return null;
            }
        } //end of GetCustomer(int ID)

        /// <summary>
        ///  This method saves the current list of customers to file
        /// </summary>        
        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(crmFile))
            {
                string columnNames = string.Format("{0},{1},{2},{3},{4},{5}", "ID", "Title", "FirstName", "LastName", "Gender", "DOB");
                writer.WriteLine(columnNames);
                foreach (Customer customerToAdd in customers)
                {
                    writer.WriteLine(customerToAdd.ToCSVString());
                }
            }
        } //end of SaveToFile()  

        /// <summary>
        ///  This method loads the list of customers from the CSV file
        /// </summary>         
        public void LoadFromFile()
        {
            //If file exists, then below if condition is true
            if (File.Exists(crmFile))
            {
                //Read all lines and store it into string array called lines
                string[] lines = File.ReadAllLines(crmFile);
                // Skip first row with column names
                lines = lines.Skip(1).ToArray();

                //Loop through the lines, Take line by line from lines
                foreach (string line in lines)
                {
                    //Split each line with "," and store it into string array called values
                    string[] values = line.Split(',');

                    //First value in the line is id

                    int ID = int.Parse(values[0]);
                    string customerTitle = values[1];
                    string firstName = values[2];
                    string lastName = values[3];
                    Customer.Gender gender = (Customer.Gender)Enum.Parse(typeof(Customer.Gender), values[4]);
                    string dob_ = values[5];
                    DateTime DOB = DateTime.Parse(dob_);

                    //Create instance of class Customer and populate the details
                    Customer customer = new Customer(ID, customerTitle, firstName, lastName, gender, DOB);

                    //Add the customer object to the Customers list
                    customers.Add(customer);
                }
            }
            else
            {
                if(!File.Exists(@"..\..\..\Data\customers.csv"))
                {
                    Console.WriteLine("Error: File not found");
                }                
                return;
            }
        } //end of LoadFromFile()

        /// <summary>
        ///  This method creates a table and formats the Customer data into a table
        /// </summary>
        public void Table()
        {
            //If Customers list contain atleast one customer data, then below if condition is true
            if (customers.Count > 0)
            {
                //Print header
                string Line = "-----------------------------------------------------------------------------";
                Console.WriteLine(Line);
                string header = "| ID  |".PadRight(6) + " Title   |".PadRight(6) + " FirstName       |".PadRight(5) + " LastName       |".PadRight(5) + " Gender    |".PadRight(5) + " DOB        |";
                Console.WriteLine(header);
                Console.WriteLine(Line);


                //Loop through Customers list
                for (int i = 0; i < customers.Count; i++)
                {
                    //Get each customer
                    Customer eachCustomer = customers[i];

                    //Print each customer
                    string cID = eachCustomer.customerID.ToString();
                    string cTitle = eachCustomer.customerTitle;
                    string cFname = eachCustomer.customerFirstName;
                    string cLname = eachCustomer.customerLastName;
                    string cGender = eachCustomer.customerGender.ToString();
                    string cDOB = eachCustomer.customerDOB.ToString("dd/MM/yyyy");

                    // String format code inspired by https://www.csharp-examples.net/align-string-with-spaces/
                    Console.WriteLine(String.Format("| {1,-3} | {2,-7} | {3,-15} | {4,-14} | {5,-9} | {6,-10} |", "|", cID, cTitle, cFname, cLname, cGender, cDOB));
                    
                    Console.WriteLine(Line);
                }
            }
            else
            {
                Console.WriteLine("Error: No customers found");
            }
        } //end of Table()
    } //end of CRM class
}
