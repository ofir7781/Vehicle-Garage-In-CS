using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageConsoleUI
    {
        private readonly GarageManager r_GarageManager;
        private eSystemStatus m_SystemStatus;

        private enum eGarageOperations
        {
            AddNewVehicleToGarage = 1,
            DisplayLicenseNumbers,
            ChangeVehicleStatus,
            InflateWheels,
            FuelAVehicle,
            ChargeAVehicle,
            DisplayVehicleData,
            Exit,
        }

        private enum eSystemStatus
        {
            Off,
            On,
        }

        public GarageConsoleUI()
        {
            r_GarageManager = new GarageManager();
            Console.ForegroundColor = ConsoleColor.White;
            m_SystemStatus = eSystemStatus.On;
        }

        public void Run()
        {
            while (m_SystemStatus == eSystemStatus.On)
            {
                printMainMenu();
                makeGarageOperations();
            }
        }

        private void printMainMenu()
        {
            UIMessages.DisplayMessages(UIMessages.eGeneralMessages.Seperator);
            UIMessages.DisplayMessages(UIMessages.eGeneralMessages.MainMenu);
            UIMessages.DisplayMessages(UIMessages.eGeneralMessages.Seperator);
            for (int row = 1; row <= 12; row++)
            {
                Console.SetCursorPosition(49, row);
                Console.Write('|');
            }

            Console.SetCursorPosition(0, 14);
        }

        private void makeGarageOperations()
        {
            bool isValidChoice = false;
            while (isValidChoice == false)
            {
                int operation = readIntFromConsole();
                try
                {
                    preformOperations(operation);
                    isValidChoice = true;
                }
                catch (ArgumentException)
                {
                    UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidSelection);
                }
            }
        }

        private void preformOperations(int i_OperationChoice)
        {
            bool isOperationSuccessful = false;
            switch ((eGarageOperations)i_OperationChoice)
            {
                case eGarageOperations.AddNewVehicleToGarage:
                    isOperationSuccessful = addNewVehicleToGarage();
                    break;
                case eGarageOperations.DisplayLicenseNumbers:
                    displayLicenseNumbers();
                    break;
                case eGarageOperations.ChangeVehicleStatus:
                    isOperationSuccessful = changeVehicleStatus();
                    break;
                case eGarageOperations.InflateWheels:
                    isOperationSuccessful = inflateWheels();
                    break;
                case eGarageOperations.FuelAVehicle:
                    isOperationSuccessful = fuelAVehicle();
                    break;
                case eGarageOperations.ChargeAVehicle:
                    isOperationSuccessful = chargeAVehicle();
                    break;
                case eGarageOperations.DisplayVehicleData:
                    displayVehicleData();
                    break;
                case eGarageOperations.Exit:
                    m_SystemStatus = eSystemStatus.Off;
                    break;
                default:
                    throw new ArgumentException();
            }

            UIMessages.DisplayMessages(UIMessages.eGeneralMessages.Seperator);
            if (isOperationSuccessful)
            {
                UIMessages.DisplayMessages(UIMessages.eGeneralMessages.OperationSuccess);
            }

            UIMessages.DisplayMessages(UIMessages.eGeneralMessages.PressAnyKeyToContinue);
        }

        private void displayVehicleData()
        {
            bool isVehicleExists = false;
            string licenseNumber = getLicenseNumber();
            isVehicleExists = r_GarageManager.IsVehicleExists(licenseNumber);
            if (isVehicleExists == true)
            {
                Console.WriteLine(r_GarageManager.DisplayVehicleData(licenseNumber).ToString());
            }
            else
            {
                Console.WriteLine("Vehicle does not exist in the garage.");
            }
        }

        private bool chargeAVehicle()
        {
            bool isVehicleExists = false;
            bool isSuccessfullOperation = false;
            string licenseNumber = getLicenseNumber();
            isVehicleExists = r_GarageManager.IsVehicleExists(licenseNumber);
            if (isVehicleExists == false)
            {
                Console.WriteLine("Vehicle does not exist in the garage.");
            }
            else
            {
                Console.WriteLine("Please enter the amount of minutes to charge:");
                bool isIllegalOperation = false;
                while (isIllegalOperation == false && isSuccessfullOperation == false)
                {
                    float amountOfMinutesToCharge = readFloatFromConsole();
                    try
                    {
                        r_GarageManager.ChargeElectricVehicle(licenseNumber, amountOfMinutesToCharge);
                        isSuccessfullOperation = true;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("The vehicle is not an electric based vehicle.");
                        isIllegalOperation = true;
                    }
                    catch (ValueOutOfRangeException voore)
                    {
                        Console.WriteLine(voore.ToString());
                    }
                    catch (Exception e)
                    {
                        // handle battery is full exception
                        Console.WriteLine(e.Message);
                        isIllegalOperation = true;
                    }
                }
            }

            return isSuccessfullOperation;
        }

        private bool fuelAVehicle()
        {
            bool isVehicleExists = false;
            bool isSuccessfullOperation = false;
            string licenseNumber = getLicenseNumber();
            isVehicleExists = r_GarageManager.IsVehicleExists(licenseNumber);
            if (isVehicleExists == false)
            {
                Console.WriteLine("Vehicle does not exist in the garage.");
            }
            else
            {
                bool isIllegalOperation = false;
                string[] supportedFuelTypes = Enum.GetNames(typeof(GasEngine.eGasType));
                while (isIllegalOperation == false && isSuccessfullOperation == false)
                {
                    Console.WriteLine("Select gas type:");
                    for (int i = 1; i <= supportedFuelTypes.Length; i++)
                    {
                        Console.WriteLine("{0}. {1}", i, supportedFuelTypes[i - 1]);
                    }

                    int gasType = readIntFromConsole();
                    Console.WriteLine("Please enter the amount of liters to fuel:");
                    float amountOfLitersToCharge = readFloatFromConsole();
                    try
                    {
                        r_GarageManager.FuelGasVehicle(licenseNumber, amountOfLitersToCharge, (GasEngine.eGasType)gasType);
                        isSuccessfullOperation = true;
                    }
                    catch (ArgumentException ae)
                    {
                        if (ae.Message == "Incorrect engine")
                        {
                            Console.WriteLine("The vehicle is not a gas based vehicle.");
                            isIllegalOperation = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect gas type, please try again.");
                        }
                    }
                    catch (ValueOutOfRangeException voore)
                    {
                        Console.WriteLine(voore.ToString());
                    }
                    catch (Exception e)
                    {
                        // handle tank is full exception
                        Console.WriteLine(e.Message);
                        isIllegalOperation = true;
                    }
                }
            }

            return isSuccessfullOperation;
        }

        private bool inflateWheels()
        {
            bool isVehicleExists = false;
            string licenseNumber = getLicenseNumber();
            isVehicleExists = r_GarageManager.IsVehicleExists(licenseNumber);
            if (isVehicleExists == false)
            {
                Console.WriteLine("Vehicle does not exist in the garage.");
            }
            else
            {
                r_GarageManager.InflateWheelsToMax(licenseNumber);
            }

            return isVehicleExists;
        }

        private bool changeVehicleStatus()
        {
            bool isVehicleExists = false;
            string licenseNumber = getLicenseNumber();
            isVehicleExists = r_GarageManager.IsVehicleExists(licenseNumber);
            if (isVehicleExists == true)
            {
                string[] vehicleStatuses = Enum.GetNames(typeof(OwnerInfo.eVehicleSatuses));
                Console.WriteLine("Select new status:");
                for (int i = 1; i <= vehicleStatuses.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i, vehicleStatuses[i - 1]);
                }

                bool isValidSelection = false;
                while (isValidSelection == false)
                {
                    int newStatus = readIntFromConsole();
                    try
                    {
                        r_GarageManager.ChangeVehicleStatus(licenseNumber, (OwnerInfo.eVehicleSatuses)newStatus);
                        isValidSelection = true;
                    }
                    catch (ArgumentException)
                    {
                        UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidSelection);
                    }
                }
            }
            else
            {
                Console.WriteLine("Vehicle does not exist in the garage.");
            }

            return isVehicleExists;
        }

        private void displayLicenseNumbers()
        {
            bool isOperationDone = false;
            Console.WriteLine(@"Choose one of the following options:
1. Dispaly license with status Repair.
2. Dispaly license with status Repaired.
3. Dispaly license with status Paid.
4. Display all license numbers.");
            while (isOperationDone == false)
            { 
            int selectedOptionFromUser = readIntFromConsole();
                try
                {
                    switch ((OwnerInfo.eVehicleSatuses)selectedOptionFromUser)
                    {
                        case OwnerInfo.eVehicleSatuses.Repair:
                            Console.WriteLine(r_GarageManager.DisplayVehicleLicenseNumbers(false, (OwnerInfo.eVehicleSatuses)selectedOptionFromUser));
                            isOperationDone = true;
                            break;
                        case OwnerInfo.eVehicleSatuses.Repaired:
                            Console.WriteLine(r_GarageManager.DisplayVehicleLicenseNumbers(false, (OwnerInfo.eVehicleSatuses)selectedOptionFromUser));
                            isOperationDone = true;
                            break;
                        case OwnerInfo.eVehicleSatuses.Paid:
                            Console.WriteLine(r_GarageManager.DisplayVehicleLicenseNumbers(false, (OwnerInfo.eVehicleSatuses)selectedOptionFromUser));
                            isOperationDone = true;
                            break;
                        default:
                            Console.WriteLine(r_GarageManager.DisplayVehicleLicenseNumbers(true, (OwnerInfo.eVehicleSatuses)selectedOptionFromUser));
                            isOperationDone = true;
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidSelection);
                }
            }
        }

        private int readIntFromConsole()
        {
            int parsedNumber;
            string userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out parsedNumber))
            {
                UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidInput);
                userInput = Console.ReadLine();
            }

            return parsedNumber;
        }

        private string readNonEmptyStringFromConsole()
        {
            string userInput = Console.ReadLine();
            while (userInput.Length == 0)
            {
                UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidInput);
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private bool addNewVehicleToGarage()
        {
            bool isNewVehicleAdded = false;
            Vehicle vehicle = null;
            string licenseNumber = getLicenseNumber();
            if (r_GarageManager.IsVehicleExists(licenseNumber) == false)
            {
                OwnerInfo ownerInfo = getOwnerInfo();
                vehicle = chooseVehicleType(licenseNumber);
                vehicle.OwnerInfo = ownerInfo;
                getModelName(vehicle);
                getCurrentEngineEnergy(vehicle);
            //    getWheelsData(vehicle);
                getUniqueVehicleAttributes(vehicle);
                r_GarageManager.AddVehicleToGarage(licenseNumber, vehicle);
                isNewVehicleAdded = true;
            }
            else
            {
                Console.WriteLine("Vehicle is already in the garage, setting status to: Repair.");
                r_GarageManager.ChangeVehicleStatus(licenseNumber, OwnerInfo.eVehicleSatuses.Repair);
            }

            return isNewVehicleAdded;
        }

        private string getLicenseNumber()
        {
            Console.WriteLine("Please enter the license number of the vehicle:");
            string licenseNumber = readNonEmptyStringFromConsole();
            return licenseNumber;
        }

        private void getUniqueVehicleAttributes(Vehicle i_Vehicle)
        {
            // Th is method use reflection using MethodInfo. this is for note number 4 in the document exercise.
            // The reflection help to get the unique memebers of each different vehicle without hard coding for a specific type in the UI.
            // In this way, the system can add new vehicles with adding the new vehicle class and editing the "CreateVehicle.cs" code only.
            List<List<string>> uniqueAttributes = i_Vehicle.UniqueAttributes(); // create list of List<string> with 2 values, in number [0] there is the output messeage. in number [1] there is the method name
            foreach (List<string> attribute in uniqueAttributes)
            {
                bool validInput = false;
                Console.WriteLine(attribute[0]); // in attribute[0] there is the current unique method relevant message to output
                while (validInput == false)
                {
                    MethodInfo uniqueMethod = i_Vehicle.GetType().GetMethod(attribute[1]); // in attribute[1] there is the name of the current unique vehicle method to use
                    try
                    {
                        string currentAttributeInput = Console.ReadLine(); // get input data from user
                        invokeUniqueMethod(uniqueMethod, i_Vehicle, new object[] { currentAttributeInput }); // call to the current unique method 
                        validInput = true;
                    }
                    catch (FormatException)
                    {   // handle parsing exceptions
                        UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidInput);
                    }
                    catch (ArgumentException)
                    {   // handle bad choice exception
                        UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidSelection);
                    }
                    catch (ValueOutOfRangeException voore)
                    {   // handle out of range exception
                        Console.WriteLine(voore.ToString());
                    }
                    catch (Exception e)
                    {
                        // handle any other exceptions
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private void invokeUniqueMethod(MethodInfo i_UniqueMethod, Vehicle i_Vehicle, object[] i_MethodData)
        {
            try
            {
                i_UniqueMethod.Invoke(i_Vehicle, i_MethodData);
            }
            catch (Exception e)
            {   
                throw e.InnerException;
            }
        }

        private void getCurrentEngineEnergy(Vehicle i_Vehicle)
        {
            bool isValidEngineEnergy = false;
            Console.WriteLine("Please enter current percentage of energy in the engine: [0 - 100]");
            while (isValidEngineEnergy == false)
            {
                try
                {
                    i_Vehicle.CurrentPercentageOfEngineEnergy = readFloatFromConsole();
                    isValidEngineEnergy = true;
                }
                catch (ValueOutOfRangeException voore)
                {
                    Console.WriteLine(voore.ToString());
                }
            }
        }

        private void getWheelsData(Vehicle i_Vehicle)
        {
            for (int i = 1; i <= i_Vehicle.Wheels.Capacity; i++)
            {
                Console.WriteLine("Please enter details about wheel number {0}:", i);
                Console.WriteLine("Manufacturer name:");
                string wheelManufacturer = readNonEmptyStringFromConsole();
                Console.WriteLine("Current air pressure:");
                bool isValidWheel = false;
                while (isValidWheel == false)
                {
                    try
                    {
                        float currentAirPressure = readFloatFromConsole();
                        i_Vehicle.Wheels.Add(new Wheel(wheelManufacturer, i_Vehicle.MaxAirPressure, currentAirPressure));
                        isValidWheel = true;
                    }
                    catch (ValueOutOfRangeException voore)
                    {
                        Console.WriteLine(voore.ToString());
                    }
                }
            }
        }

        private OwnerInfo getOwnerInfo()
        {
            Console.WriteLine("Please enter the owner full name:");
            string ownerName = readNonEmptyStringFromConsole();
            Console.WriteLine("Please enter the owner phone number:");
            string ownerPhone = readNonEmptyStringFromConsole();
            return new OwnerInfo(ownerName, ownerPhone);
        }

        private float readFloatFromConsole()
        {
            float parsedNumber;
            string userInput = Console.ReadLine();
            while (!float.TryParse(userInput, out parsedNumber))
            {
                UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidInput);
                userInput = Console.ReadLine();
            }

            return parsedNumber;
        }

        private void getModelName(Vehicle i_Vehicle)
        {
            Console.WriteLine("Please enter the model name of the vehicle:");
            i_Vehicle.ModelName = readNonEmptyStringFromConsole();
        }

        private Vehicle chooseVehicleType(string i_LicenseNumber)
        {
            // The next printing section was done with Enum.GetName because of note number 4 in the document exercise.
            // In this way, the system can add new vehicles with adding the new vehicle class and editing the "CreateVehicle.cs" code only.
            Vehicle newVehicle = null;
            Console.WriteLine("Select vehicle type:");
            string[] supportedVehicleTypes = Enum.GetNames(typeof(VehicleCreator.eSupportedVehicles));
            for (int i = 1; i <= supportedVehicleTypes.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i, supportedVehicleTypes[i - 1]);
            }

            int vehicleTypeChoice;
            bool isValidSelection = false;
            while (isValidSelection == false)
            {
                vehicleTypeChoice = readIntFromConsole();
                try
                {
                    newVehicle = VehicleCreator.MakeVehicle(vehicleTypeChoice, i_LicenseNumber);
                    isValidSelection = true;
                }
                catch (ArgumentException)
                {
                    UIMessages.DisplayMessages(UIMessages.eGeneralMessages.InvalidSelection);
                }
            }

            return newVehicle;
        }
    }
}
