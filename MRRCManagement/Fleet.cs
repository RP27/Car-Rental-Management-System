using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRCManagement
{
    /// <summary>
    /// The Fleet class displays information about each Vehicle and its registration.
    /// 
    /// It contains methods required to add vehicles to the Fleet, find vehicles by rego,
    /// and allows vehicles to be removed, also.
    /// 
    /// 
    /// </summary>

    public class Fleet
    {
        // Global variables
        public List<Vehicle> vehicles { get; set; }
        public List<Vehicle> vehicles2 { get; set; }
        public List<string> rentalRego { get; set; }
        public List<int> rentalCustomerID { get; set; }

        // Locations of the CSV files to be read 
        public string rentalsFile;
        public string fleetFile;

        /// <summary>
        /// Intialises a new instance of the Fleet class
        /// </summary>
        /// 
        /// <param name="fleetFile"> Locations of the CSV file to be read  </param>
        /// <param name="rentalsFile"> Locations of the CSV file to be read </param>

        public Fleet(string fleetFile, string rentalsFile)
        {
            if (File.Exists(fleetFile) && File.Exists(rentalsFile))
            {
                this.fleetFile = fleetFile;
                this.rentalsFile = rentalsFile;
            }
            else
            {
                // Uses default file locations if file is not found
                this.fleetFile = @"..\..\..\Data\fleet.csv";
                this.rentalsFile = @"..\..\..\Data\rentals.csv";
            }

            // Initialising lists
            vehicles = new List<Vehicle>();
            vehicles2 = new List<Vehicle>();
            rentalRego = new List<string>();
            rentalCustomerID = new List<int>();

            // Reading data from CSV files
            LoadVehiclesFromFile();
            LoadRentalsFromFile();
        } //end of Fleet(string fleetFile, string rentalsFile)

        /// <summary>
        /// Adds a vehicle to the list of vehicles in Fleet
        /// 
        /// </summary>
        /// 
        /// <param name="vehicle"> Instance of a vehicle </param>
        /// 
        /// <returns> Returns true if the vehicle was added, false otherwise </returns>

        public bool AddVehicle(Vehicle vehicle)
        {
            // Checks if the vehicle registration we want to add matches any 
            // of our current vehicle registration numbers
            bool containsItem = vehicles.Any(item => item.vehicleRego == vehicle.vehicleRego);

            if (!containsItem)
            {
                // Adds the vehicle if no matching existing registration number
                vehicles.Add(vehicle);
                return true;
            }
            else
            {
                Console.WriteLine("Error: A vehicle with this registration already exists. Please use a different registration.");
                return false;
            }
        } //end of AddVehicle(Vehicle vehicle)

        /// <summary>
        /// Adds the registration of a vehicle and the customer's ID to their respective lists 
        /// 
        /// </summary>
        /// 
        /// <param name="rego"> Registration of a vehicle </param>
        /// <param name="customerID"> ID number of a customer </param>

        public void AddRental(string rego, int customerID)
        {
            rentalRego.Add(rego);
            rentalCustomerID.Add(customerID);
        } //end of AddRental(string rego, int customerID)


        /// <summary>
        /// Finds the vehicle by registration number in the Fleet list and removes it,
        /// if it is not currently being rented.
        /// </summary>
        /// 
        /// <param name="registration"> Registration number of the vehicle </param>
        /// 
        /// <returns> returns the customer ID. If no one is renting, return -1 </returns>

        public bool RemoveVehicle(string registration)
        {
            // If vehicle is currently being rented, do not remove it
            for (int i = 0; i < rentalRego.Count(); i++)
            {
                if (rentalRego[i] == registration || rentalRego[i] == registration.ToLower())
                {
                    return false;
                }
            }

            // If vehicle is not rented, remove it from the list of vehicles
            for (int i = 0; i < vehicles.Count(); i++)
            {
                if (vehicles[i].vehicleRego == registration || vehicles[i].vehicleRego == registration.ToLower())
                {
                    vehicles.Remove(vehicles[i]);
                    return true;
                }
            }
            return false;
        } //end of RemoveVehicle(string registration)    

        /// <summary>
        /// Finds the vehicle by registration number in the Rental list and removes it
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool RemoveRental(string registration)
        {
            for (int i = 0; i < rentalRego.Count(); i++)
            {
                if (rentalRego[i] == registration)
                {
                    rentalRego.Remove(rentalRego[i]);
                    rentalCustomerID.Remove(rentalCustomerID[i]);
                }
            }

            return false;
        } //end of RemoveRental(string registration)

        /// <summary>
        /// Checks if the vehicle is being rented out by its registration
        /// </summary>
        /// 
        /// <param name="registration"> Registration number of the vehicle </param>
        /// 
        /// <returns> returns true if the vehicle is being rented, false otherwise </returns>

        public bool IsRented(string registration)
        {
            for (int i = 0; i < rentalRego.Count; i++)
            {
                if (rentalRego[i] == registration || rentalRego[i] == registration.ToLower())
                {
                    return true;
                }
            }
            return false;
        } //end of IsRented(string registration)

        /// <summary>
        /// This method returns whether the customer with the provided customer ID is currently
        /// renting a vehicle
        /// </summary>
        /// 
        /// <param name="customerID"> Registration number of the vehicle </param>
        /// 
        /// <returns> returns true if the vehicle is being rented, false otherwise </returns>

        public bool IsRenting(int customerID)
        {
            for (int i = 0; i < rentalRego.Count; i++)
            {
                if (rentalCustomerID[i] == customerID)
                {
                    return true;
                }
            }
            return false;
        } //end of IsRented(string registration)

        /// <summary>
        /// This method returns the vehicle by its registration number
        /// 
        /// </summary>
        /// 
        /// <param name="registration> Registration number of the vehicle </param>
        /// 
        /// <returns> returns the instance of the vehicle matching the registration number </returns>
        public Vehicle GetVehicle(string registration)
        {
            // Checks if the registration number entered matches the registration number in the Fleet list
            Vehicle vehicle = vehicles.Find(x => x.vehicleRego == registration);

            if (vehicle != null)
            {
                return vehicle;
            }
            else
            {
                Console.WriteLine("\nError: There is no vehicle with this registration.");
                return null;
            }
        } //end of GetVehicle(string registration)

        /// <summary>
        ///  This method saves the current list of vehicles to file
        /// </summary>
        public void SaveVehiclesToFile()
        {
            using (StreamWriter writer = new StreamWriter(fleetFile))
            {
                // Writes column/header names in the CSV file
                string columnNames = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "Registration", "Grade", "Make", "Model", "Year", "NumSeats", "Transmission", "Fuel", " GPS", "SunRoof", "DailyRate", "Colour");
                writer.WriteLine(columnNames);

                foreach (Vehicle vehicle in vehicles)
                {
                    // Iterates through all the vehicles in the Fleet list and writes them to the CSV file
                    writer.WriteLine(vehicle.ToCSVString());
                }
            }
        } //end of SaveVehiclesToFile()

        /// <summary>
        ///  This method saves the current list of rentals to file
        /// </summary>
        public void SaveRentalsToFile()
        {
            using (StreamWriter writer = new StreamWriter(rentalsFile))
            {
                string columnNames = string.Format("{0},{1}", "Registration", "CustomerID");
                writer.WriteLine(columnNames);


                for (int i = 0; i < rentalRego.Count(); i++)
                {
                    var line = String.Format("{0},{1}", rentalRego[i], rentalCustomerID[i]);
                    writer.WriteLine(line);
                }
            }

        } //end of SaveRentalsToFile()

        /// <summary>
        ///  This method loads the list of vehicles from the CSV file
        /// </summary>
        public void LoadVehiclesFromFile()
        {
            //If file exists, then below if condition is true
            if (File.Exists(fleetFile))
            {
                //Read all lines and store it into string array called lines
                string[] lines = File.ReadAllLines(fleetFile);
                // Skip first row with column names
                lines = lines.Skip(1).ToArray();

                //Loop through the lines, Take line by line from lines
                foreach (string line in lines)
                {
                    //Split each line with "," and store it into string array called values
                    string[] values = line.Split(',');



                    string registration = values[0];
                    Vehicle.VehicleGrade grade = (Vehicle.VehicleGrade)Enum.Parse(typeof(Vehicle.VehicleGrade), values[1]);
                    string make = values[2];
                    string model = values[3];
                    int year = int.Parse(values[4]);
                    int seats = int.Parse(values[5]);
                    string Seats = seats + "-Seater";
                    Vehicle.TransmissionType transmission = (Vehicle.TransmissionType)Enum.Parse(typeof(Vehicle.TransmissionType), values[6]);
                    Vehicle.FuelType fuel = (Vehicle.FuelType)Enum.Parse(typeof(Vehicle.FuelType), values[7]);
                    bool GPS = bool.Parse(values[8]);
                    string newGPS = values[8];
                    string NewGPS;
                    if (newGPS == "TRUE")
                    {
                        NewGPS = "GPS";
                    }
                    else
                    {
                        NewGPS = "No GPS";
                    }
                    bool sunroof = bool.Parse(values[9]);
                    string sunRoof = values[9];
                    string newSunRoof;
                    if (sunRoof == "TRUE")
                    {
                        newSunRoof = "Sunroof";
                    }
                    else
                    {
                        newSunRoof = "No Sunroof";
                    }
                    double dailyRate = double.Parse(values[10]);
                    string colour = values[11];

                    Vehicle vehicle = new Vehicle(registration, grade, make, model, year, seats, transmission, fuel, GPS, sunroof, dailyRate, colour);
                    Vehicle vehicle2 = new Vehicle(registration, grade, make, model, year, Seats, transmission, fuel, NewGPS, newSunRoof, dailyRate, colour);
                    vehicles.Add(vehicle);
                    vehicles2.Add(vehicle2);
                }
            }
            else
            {
                if (!File.Exists(@"..\..\..\Data\fleet.csv"))
                {
                    Console.WriteLine("Error: File not found");
                }
                return;
            }
        } //end of LoadVehiclesFromFile()

        /// <summary>
        ///  This method loads the list of vehicles from the CSV file
        /// </summary>
        public void LoadRentalsFromFile()
        {
            //If file exists, then below if condition is true
            if (File.Exists(rentalsFile))
            {
                //Read all lines and store it into string array called lines
                string[] lines = File.ReadAllLines(rentalsFile);
                // Skip first row with column names
                lines = lines.Skip(1).ToArray();

                //Loop through the lines, Take line by line from lines
                foreach (string line in lines)
                {
                    //Split each line with "," and store it into string array called values
                    string[] values = line.Split(',');

                    string registration = values[0];
                    int customerID = int.Parse(values[1]);

                    rentalRego.Add(registration);
                    rentalCustomerID.Add(customerID);
                }
            }
            else
            {
                if (!File.Exists(@"..\..\..\Data\rentals.csv"))
                {
                    Console.WriteLine("Error: File not found");
                }
                return;
            }
        } //end of LoadRentalsFromFile()

        /// <summary>
        ///  This method creates a table and formats the Rental data into a table
        /// </summary>
        public void RentalTable()
        {
            //If Customers list contain atleast one customer data, then below if condition is true
            if (rentalRego.Count() > 0)
            {
                Fleet fleet = new Fleet(fleetFile, rentalsFile);

                //Print header
                string Line = "--------------------------------------------";
                Console.WriteLine(Line);
                string header = "| Registration  |".PadRight(6) + " CustomerID   |".PadRight(6) + " DailyRate |";
                Console.WriteLine(header);
                Console.WriteLine(Line);

                //Loop through Customers list
                for (int i = 0; i < rentalRego.Count; i++)
                {
                    //Print each customer
                    string registration = rentalRego[i];
                    int cID = rentalCustomerID[i];
                    double dr = fleet.GetVehicle(registration).dailyRate;

                    // String format code inspired by https://www.csharp-examples.net/align-string-with-spaces/
                    Console.WriteLine(String.Format("| {1,-13} | {2,-12} | {3,-9} |", "|", registration, cID, dr));


                    Console.WriteLine(Line);
                }
            }
            else
            {
                Console.WriteLine("No customers found.");
            }
        } //end of rentalTable()

        /// <summary>
        ///  This method creates a table and formats the Fleet data into a table
        /// </summary> 
        public void FleetTable()
        {
            //If Customers list contain atleast one customer data, then below if condition is true
            if (vehicles.Count() > 0)
            {
                //Print header
                string Line = "-------------------------------------------------------------------------------------------------------------------------------------------------";
                Console.WriteLine(Line);
                string header = "| Registration |".PadRight(5) + " Grade      |".PadRight(5) + " Make       |".PadRight(5) + " Model         |".PadRight(5) + " Year  |".PadRight(5) + " NumSeats |".PadRight(5) + " Transmission |".PadRight(5) + " Fuel      |".PadRight(5) + " GPS     |".PadRight(5) + " SunRoof |".PadRight(5) + " DailyRate |".PadRight(5) + " Colour |";
                Console.WriteLine(header);
                Console.WriteLine(Line);


                //Loop through Customers list
                for (int i = 0; i < vehicles.Count; i++)
                {
                    Vehicle eachVehicle = vehicles[i];

                    //Print each customer
                    string vehicleRego = eachVehicle.vehicleRego;
                    string make = eachVehicle.make;
                    string model = eachVehicle.model;
                    int year = eachVehicle.year;
                    Vehicle.VehicleGrade vehicleGrade = eachVehicle.vehicleGrade;
                    int numSeats = eachVehicle.numSeats;
                    Vehicle.TransmissionType transmission = eachVehicle.transmission;
                    Vehicle.FuelType fuel = eachVehicle.fuel;
                    bool GPS = eachVehicle.GPS;
                    bool sunRoof = eachVehicle.sunRoof;
                    double dailyRate = eachVehicle.dailyRate;
                    string colour = eachVehicle.colour;

                    // String format code inspired by https://www.csharp-examples.net/align-string-with-spaces/
                    Console.WriteLine(String.Format("| {1,-12} | {2,-10} | {3,-10} | {4,-13} | {5,-5} | {6,-8} | {7,-12} | {8,-9} | {9,-7} | {10,-7} | {11,-9} | {12,-6} |", "|", vehicleRego, vehicleGrade, make, model, year, numSeats, transmission, fuel, GPS, sunRoof, dailyRate, colour));

                    Console.WriteLine(Line);
                }
            }
            else
            {
                Console.WriteLine("No vehicles found.");
            }
        } //end of FleetTable()

        /// <summary>
        ///  This method produces a table for the search results of the list of vehicles
        /// </summary>
        /// <param name="searchList"> Final output of vehicles from the user's query </param>
        public void SearchTable(List<Vehicle> searchList)
        {
            //If Customers list contain atleast one customer data, then below if condition is true
            if (searchList.Count() > 0)
            {
                //Print header
                string Line = "----------------------------------------------------------------------------------------------------------------------------------------------------";
                Console.WriteLine(Line);
                string header = "| Registration |".PadRight(5) + " Grade      |".PadRight(5) + " Make       |".PadRight(5) + " Model         |".PadRight(5) + " Year  |".PadRight(5) + " NumSeats |".PadRight(5) + " Transmission |".PadRight(5) + " Fuel      |".PadRight(5) + " GPS     |".PadRight(5) + " SunRoof    |".PadRight(5) + " DailyRate |".PadRight(5) + " Colour |";
                Console.WriteLine(header);
                Console.WriteLine(Line);


                //Loop through Customers list
                for (int i = 0; i < searchList.Count; i++)
                {
                    Vehicle eachVehicle = searchList[i];

                    //Print each customer
                    string vehicleRego = eachVehicle.vehicleRego;
                    string make = eachVehicle.make;
                    string model = eachVehicle.model;
                    int year = eachVehicle.year;
                    Vehicle.VehicleGrade vehicleGrade = eachVehicle.vehicleGrade;
                    string sSeats = eachVehicle.numberSeats.Replace("-Seater", "");
                    int numSeats = int.Parse(sSeats);
                    Vehicle.TransmissionType transmission = eachVehicle.transmission;
                    Vehicle.FuelType fuel = eachVehicle.fuel;
                    string GPS = eachVehicle.newGPS;
                    string sunRoof = eachVehicle.newSunRoof;
                    double dailyRate = eachVehicle.dailyRate;
                    string colour = eachVehicle.colour;

                    // String format code inspired by https://www.csharp-examples.net/align-string-with-spaces/
                    Console.WriteLine(String.Format("| {1,-12} | {2,-10} | {3,-10} | {4,-13} | {5,-5} | {6,-8} | {7,-12} | {8,-9} | {9,-7} | {10,-10} | {11,-9} | {12,-6} |", "|", vehicleRego, vehicleGrade, make, model, year, numSeats, transmission, fuel, GPS, sunRoof, dailyRate, colour));

                    Console.WriteLine(Line);
                }
            }
            else
            {
                Console.WriteLine("No vehicles found.");
            }
        } //end of FleetTable()

        /// <summary>
        /// Searches the list of vehicles in the Fleet based on the user's query
        /// </summary>
        /// <param name="query"> User's search input </param>
        public void RentalSearch(string query)
        {
            string query2 = query;
            Fleet fleet = new Fleet(fleetFile, rentalsFile);
            List<string> cars2 = new List<string>();
            query = query.ToLower();
            List<string> list = query.Split().ToList();
            List<string> copyList = query.Split().ToList();
            List<string> copyList2 = list;            
            List<string> queryOperator = new List<string>();
            List<string> andList = new List<string>();
            List<string> copyAndList = andList;
            list.RemoveAll(string.IsNullOrWhiteSpace);
            IEnumerable<Vehicle> results;
            List<Vehicle> matchingVehicles = new List<Vehicle>();
            List<Vehicle> distinctMatchingVehicles = new List<Vehicle>();

            List<Vehicle> operatorVehicles = new List<Vehicle>();

            // For attributes with spaces, this combines the attributes into one element in the list
            for (int i = 0; i < list.Count() - 1; i++)
            {
                if ((list[i] != "or" && list[i] != "and"))
                {
                    if ((list[i + 1] != "or" && list[i + 1] != "and" && list[i + 1] != null))
                    {
                        list[i] = list[i] + list[i + 1];
                        list.Remove(list[i + 1]);
                    }
                }
            }
            // For attributes with spaces, this combines the attributes into one element in the list
            for (int i = 0; i < list.Count() - 1; i++)
            {
                if ((list[i] != "or" && list[i] != "and"))
                {
                    if ((list[i + 1] != "or" && list[i + 1] != "and" && list[i + 1] != null))
                    {
                        list[i] = list[i] + list[i + 1];
                        list.Remove(list[i + 1]);
                    }
                }
            }

            // Blank query
            if (query2.Length == 0)
            {
                // Table output
                FleetTable();
            }

            // Single attribute search
            else if (!list.Contains("or") && !list.Contains("and"))
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    results = vehicles2.Where(s => (s.vehicleRego.ToString().ToLower() == list[i] || s.vehicleGrade.ToString().ToLower() == list[i] ||
                                            s.make.ToString().ToLower() == list[i] || s.model.ToString().ToLower().Replace(" ", "") == list[i] || s.year.ToString().ToLower() == list[i] ||
                                            s.numberSeats.ToString().ToLower() == list[i] || s.transmission.ToString().ToLower() == list[i] ||
                                            s.fuel.ToString().ToLower() == list[i] || s.newGPS.ToString().ToLower() == list[i] ||
                                            s.newSunRoof.ToString().ToLower() == list[i] || s.dailyRate.ToString().ToLower() == list[i] ||
                                            s.colour.ToString().ToLower() == list[i]) && !IsRented(s.vehicleRego));

                    // Where conditions are met, the vehicles are added to the list
                    matchingVehicles.AddRange(results);
                    // Removes duplicate vehicles from previous list
                    distinctMatchingVehicles = matchingVehicles.Distinct().ToList();
                }
                // Table output
                SearchTable(distinctMatchingVehicles);
            }


            // Disjunction query (OR)
            else if (list.Contains("or") && !list.Contains("and"))
            {
                list.Remove("or");
                queryOperator.Add("or");

                for (int i = 0; i < list.Count(); i++)
                {
                    results = vehicles2.Where(s => (s.vehicleRego.ToString().ToLower() == list[i] || s.vehicleGrade.ToString().ToLower() == list[i] ||
                                            s.make.ToString().ToLower() == list[i] || s.model.ToString().ToLower().Replace(" ", "") == list[i] || s.year.ToString().ToLower() == list[i] ||
                                            s.numberSeats.ToString().ToLower() == list[i] || s.transmission.ToString().ToLower() == list[i] ||
                                            s.fuel.ToString().ToLower() == list[i] || s.newGPS.ToString().ToLower() == list[i] ||
                                            s.newSunRoof.ToString().ToLower() == list[i] || s.dailyRate.ToString().ToLower() == list[i] ||
                                            s.colour.ToString().ToLower() == list[i]) && !IsRented(s.vehicleRego));

                    // Where conditions are met, the vehicles are added to the list
                    matchingVehicles.AddRange(results);
                    // Removes duplicate vehicles from previous list
                    distinctMatchingVehicles = matchingVehicles.Distinct().ToList();
                }
                // Table output
                SearchTable(distinctMatchingVehicles);
            }

            // Conjunction query (AND)
            else if (list.Contains("and") && !list.Contains("or"))
            {
                while (list.Contains("and"))
                {
                    list.Remove("and");
                    queryOperator.Add("and");
                }

                foreach (Vehicle v in vehicles2)
                {
                    bool found = true;
                    for (int i = 0; found && i < list.Count; i++)
                    {
                        found = VehicleContains(v, list[i]);
                    }
                    if (found && !IsRented(v.vehicleRego))
                    {
                        matchingVehicles.Add(v);
                    }
                }
                SearchTable(matchingVehicles);
            }

            // Combination query (AND & OR)
            else if (list.Contains("and") && list.Contains("or"))
            {
                if (list.Count() < 6)
                {
                    // Finds index of OR and separates based on operators
                    int index = list.FindLastIndex(x => x == "or");

                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (i > index)
                        {
                            andList.Add(list[i]);
                            list.Remove(list[i]);
                        }
                    }

                    while (list.Contains("and") || list.Contains("or"))
                    {
                        list.Remove("and");
                        list.Remove("or");
                    }
                }
                else
                {
                    // Finds index of OR and separates based on operators
                    int index = list.FindLastIndex(x => x == "or");

                    andList.Add(list[index + 1]);
                    andList.Add(list[index + 2]);
                    andList.Add(list[index + 3]);
                    list.Remove(list[index + 3]);
                    list.Remove(list[index + 2]);
                    list.Remove(list[index + 1]);

                    while (list.Contains("and") || list.Contains("or"))
                    {
                        list.Remove("and");
                        list.Remove("or");
                    }
                    while (andList.Contains("and"))
                    {
                        andList.Remove("and");
                    }
                }

                int index2 = copyList.FindLastIndex(x => x == "or");
                int index3 = copyList.FindLastIndex(x => x == "and");

                // If query contains AND before OR, swap lists around
                if (index2 > index3)
                {
                    andList = copyList2;
                    list = copyAndList;
                }

                // AND operations
                foreach (Vehicle v in vehicles2)
                {
                    bool found = true;
                    for (int i = 0; found && i < andList.Count; i++)
                    {
                        found = VehicleContains(v, andList[i]);
                    }
                    if (found && !IsRented(v.vehicleRego))
                    {
                        distinctMatchingVehicles.Add(v);
                    }
                }

                // OR operations
                for (int i = 0; i < list.Count(); i++)
                {
                    results = vehicles2.Where(s => (s.vehicleRego.ToString().ToLower() == list[i] || s.vehicleGrade.ToString().ToLower() == list[i] ||
                                            s.make.ToString().ToLower() == list[i] || s.model.ToString().ToLower().Replace(" ", "") == list[i] || s.year.ToString().ToLower() == list[i] ||
                                            s.numberSeats.ToString().ToLower() == list[i] || s.transmission.ToString().ToLower() == list[i] ||
                                            s.fuel.ToString().ToLower() == list[i] || s.newGPS.ToString().ToLower() == list[i] ||
                                            s.newSunRoof.ToString().ToLower() == list[i] || s.dailyRate.ToString().ToLower() == list[i] ||
                                            s.colour.ToString().ToLower() == list[i]) && !IsRented(s.vehicleRego));

                    // Where conditions are met, the vehicles are added to the list
                    matchingVehicles.AddRange(results);
                    matchingVehicles = matchingVehicles.Distinct().ToList();

                    foreach (Vehicle vv in matchingVehicles)
                    {
                        distinctMatchingVehicles.Add(vv);
                    }
                }
                // Table output
                SearchTable(distinctMatchingVehicles);
            }
        } //end of RentalSearch(string query)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"> An instance of a vehicle </param>
        /// <param name="s"> User's query </param>
        /// <returns> Returns true if a vehicle matches the search conditions, false if not </returns>
        private static bool VehicleContains(Vehicle vehicle, string s)
        {
            if (s == vehicle.vehicleRego.ToString().ToLower() || s == vehicle.vehicleGrade.ToString().ToLower() || s == vehicle.make.ToString().ToLower() || s == vehicle.model.ToString().ToLower().Replace(" ", "") || s == vehicle.year.ToString().ToLower() || s == vehicle.numberSeats.ToString().ToLower() || s == vehicle.transmission.ToString().ToLower() || s == vehicle.fuel.ToString().ToLower() || s == vehicle.newGPS.ToString().ToLower() || s == vehicle.newSunRoof.ToString().ToLower() || s == vehicle.dailyRate.ToString().ToLower() || s == vehicle.colour.ToString().ToLower())
                return true;
            return false;
        } // end of VehicleContains(Vehicle vehicle, string s)
    } //end of Fleet class
}
