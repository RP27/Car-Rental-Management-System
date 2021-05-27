using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRCManagement
{
    /// <summary>
    /// The Customer class provides us with information about customers renting vehicles
    /// 
    /// </summary>    
    public class Customer
    {
        // Variables are automatically initialised
        public int customerID { get; set; }
        public string customerTitle { get; set; }
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public Gender customerGender { get; set; }
        public DateTime customerDOB { get; set; }

        public enum Gender
        {
            Male,
            Female,
            Other
        }

        /// <summary>
        /// Intialises a new instance of the Customer class
        /// </summary>
        /// 
        /// <param name="ID"> Customer's ID number </param>
        /// <param name="title"> Customer's title </param>
        /// <param name="firstName"> Customer's first name </param>
        /// <param name="lastName"> Customer's last name </param>
        /// <param name="gender"> Customer's gender </param>
        /// <param name="DOB"> Customer's date of birth </param>
        public Customer(int ID, string title, string firstName, string lastName, Gender gender,
            DateTime DOB)
        {
            this.customerID = ID;
            this.customerTitle = title;
            this.customerFirstName = firstName;
            this.customerLastName = lastName;
            this.customerGender = gender;
            this.customerDOB = DOB;
        } //end of Customer()

        /// <summary>
        /// Converts data type into a string for CSV writing
        /// </summary>
        /// 
        /// <returns> Returns in CSV string format </returns>
        public string ToCSVString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", customerID, customerTitle, customerFirstName, customerLastName, customerGender, customerDOB.ToString("dd/MM/yyyy"));
        } //end of ToCSVString()

        /// <summary>
        /// Converts data type into a string
        /// </summary>
        /// 
        /// <returns> Returns in a formatted string format </returns>        
        public override string ToString()
        {
            return this.customerID + "," + this.customerTitle + "," + this.customerFirstName + "," + this.customerLastName + "," + this.customerGender + "," + this.customerDOB;
        } //end of ToString()
    } //end of Customer class
}
