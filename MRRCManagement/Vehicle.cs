using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRCManagement
{
    /// <summary>
    /// The Vehicle class provides us with information about the attributes of vehicles
    /// 
    /// </summary>    
    public class Vehicle
    {
        public enum VehicleGrade
        {
            Economy,
            Family,
            Commercial,
            Luxury
        }

        public enum TransmissionType
        {
            Manual,
            Automatic
        }

        public enum FuelType
        {
            Petrol,
            Diesel
        }

        // Automatically intialised vehicles
        public string vehicleRego { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public VehicleGrade vehicleGrade { get; set; }
        public int numSeats {get; set;}
        public TransmissionType transmission { get; set; }
        public FuelType fuel { get; set; }
        public bool GPS { get; set; }
        public bool sunRoof { get; set; }
        public double dailyRate { get; set; }
        public string colour { get; set; }

        // Variables for the other constructors
        public string newGPS { get; set; }
        public string newSunRoof { get; set; }
        public string numberSeats { get; set; }

        /// <summary>
        /// Intialises the Vehicle class
        /// </summary>
        public Vehicle(string registration, VehicleGrade grade, string make, string model,
                        int year, int numSeats, TransmissionType transmission, FuelType fuel,
                        bool GPS, bool sunRoof, double dailyRate, string colour)
        {
            this.vehicleRego = registration;
            this.vehicleGrade = grade;
            this.make = make;
            this.model = model;
            this.year = year;
            this.numSeats = numSeats;
            this.transmission = transmission;
            this.fuel = fuel;
            this.GPS = GPS;
            this.sunRoof = sunRoof;
            this.dailyRate = dailyRate;
            this.colour = colour;
        } //end of Vehicle()

        public Vehicle(string registration, VehicleGrade grade, string make, string model,
                        int year, string numSeats, TransmissionType transmission, FuelType fuel,
                        string GPS, string sunRoof, double dailyRate, string colour)
        {
            this.vehicleRego = registration;
            this.vehicleGrade = grade;
            this.make = make;
            this.model = model;
            this.year = year;
            this.numberSeats = numSeats;
            this.transmission = transmission;
            this.fuel = fuel;
            this.newGPS = GPS;
            this.newSunRoof = sunRoof;
            this.dailyRate = dailyRate;
            this.colour = colour;
        } //end of Vehicle()

        public Vehicle(string registration, VehicleGrade grade, string make, string model,
                       int year)
        {
            this.vehicleRego = registration;
            this.vehicleGrade = grade;
            this.make = make;
            this.model = model;
            this.year = year;

            if(grade == VehicleGrade.Economy)
            {
                numSeats = 4;
                transmission = TransmissionType.Automatic;
                fuel = FuelType.Petrol;
                GPS = false;
                sunRoof = false;
                dailyRate = 50;
                colour = "Black";
            } 
            else if (grade == VehicleGrade.Family)
            {
                numSeats = 4;
                transmission = TransmissionType.Manual;
                fuel = FuelType.Petrol;
                GPS = false;
                sunRoof = false;
                dailyRate = 80;
                colour = "Black";
            }
            else if (grade == VehicleGrade.Luxury)
            {
                numSeats = 4;
                transmission = TransmissionType.Manual;
                fuel = FuelType.Petrol;
                GPS = true;
                sunRoof = true;
                dailyRate = 120;
                colour = "Black";
            }
            else if (grade == VehicleGrade.Commercial)
            {
                numSeats = 4;
                transmission = TransmissionType.Manual;
                fuel = FuelType.Diesel;
                GPS = false;
                sunRoof = false;
                dailyRate = 130;
                colour = "Black";
            }
            else
            {
                numSeats = 4;
                transmission = TransmissionType.Manual;
                fuel = FuelType.Petrol;
                GPS = false;
                sunRoof = false;
                dailyRate = 50;
                colour = "Black";
            }
        } //end of Vehicle()

        /// <summary>
        /// Converts data type into a string for CSV writing
        /// </summary>
        /// <returns> Returns in a formatted string format </returns>
        public string ToCSVString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", vehicleRego, vehicleGrade, make, model, year, numSeats, transmission, fuel, GPS, sunRoof, dailyRate, colour);
        } //end of ToCSVString()

        /// <summary>
        /// Converts data type into a string
        /// </summary>
        /// 
        /// <returns> Returns in a formatted string format </returns>
        public override string ToString()
        {
            return base.ToString();
        } //end of ToString()   

        /// <summary>
        ///  Gets the attributes of the vehicle in string format
        /// </summary>
        /// <returns> Returns the attributes of the vehicle in string format </returns>
        public List<string> GetAttributeList()
        {
            List<string> listOfAttributes = new List<string>();

            string newNumSeats = numSeats.ToString() + " seater";

            listOfAttributes.Add(vehicleRego);
            listOfAttributes.Add(vehicleGrade.ToString());
            listOfAttributes.Add(make);
            listOfAttributes.Add(model);
            listOfAttributes.Add(year.ToString());
            listOfAttributes.Add(newNumSeats);
            listOfAttributes.Add(transmission.ToString());
            listOfAttributes.Add(fuel.ToString());
            listOfAttributes.Add(dailyRate.ToString());
            listOfAttributes.Add(colour);

            if (sunRoof == true)
            {
                listOfAttributes.Add("sunroof");
            }
            if (GPS == true)
            {
                listOfAttributes.Add("GPS");
            }

            return listOfAttributes;
        }
    } // end of Vehicle class
}
