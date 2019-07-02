using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, Vehicle> r_Vehicles;

        public GarageManager()
        {
            r_Vehicles = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> Vehicles
        {
            get
            {
                return r_Vehicles;
            }
        }

        public bool IsVehicleExists(string i_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public void AddVehicleToGarage(string i_LicenseNumber, Vehicle i_Vehicle)
        {
            r_Vehicles.Add(i_LicenseNumber, i_Vehicle);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, OwnerInfo.eVehicleSatuses i_NewVehicleStatus)
        {
            r_Vehicles[i_LicenseNumber].OwnerInfo.VehicleStatus = i_NewVehicleStatus;
        }

        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            r_Vehicles[i_LicenseNumber].WheelsInflationMax();
        }

        public Vehicle DisplayVehicleData(string i_LicenseNumber)
        {
            Vehicle res = r_Vehicles[i_LicenseNumber];
            return res;
        }

        public string DisplayVehicleLicenseNumbers(bool i_DispalyAllLicenseNumbers, OwnerInfo.eVehicleSatuses i_VehicleSatuses)
        {
            StringBuilder ShowLicenseNumbers = new StringBuilder();

            if ((int)i_VehicleSatuses < 1 || (int)i_VehicleSatuses > 4)
            {
                throw new ArgumentException();
            }

            foreach (KeyValuePair<string, Vehicle> vehicle in r_Vehicles)
            {
                if (i_DispalyAllLicenseNumbers == true)
                {
                    ShowLicenseNumbers.Append(string.Format("{0}{1}", vehicle.Key, Environment.NewLine));
                }
                else if (vehicle.Value.OwnerInfo.VehicleStatus == i_VehicleSatuses)
                {
                    ShowLicenseNumbers.Append(string.Format("{0}{1}", vehicle.Key, Environment.NewLine));
                }
            }

            if (ShowLicenseNumbers.Length == 0)
            {
                if (i_DispalyAllLicenseNumbers == true)
                {
                    ShowLicenseNumbers.Append("There are no vehicles in the garage.");
                }
                else
                {
                    ShowLicenseNumbers.Append("There are no vehicles in the garage that fits your selections.");
                }
            }

            return ShowLicenseNumbers.ToString();
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_MinutesToCharge)
        {
            ElectricEngine electricEngine = r_Vehicles[i_LicenseNumber].Engine as ElectricEngine;

            if (electricEngine != null)
            {
                electricEngine.BatteryCharging(i_MinutesToCharge);
            }
            else
            {
                throw new ArgumentException();
            }

            r_Vehicles[i_LicenseNumber].UpdateVehicleEngineEnergyPrecentage();
        }

        public void FuelGasVehicle(string i_LicenseNumber, float i_FillFuelQuantity, GasEngine.eGasType i_GasType)
        {
            GasEngine gasEngine = r_Vehicles[i_LicenseNumber].Engine as GasEngine;

            if (gasEngine != null)
            {
                gasEngine.VehicleFueling(i_GasType, i_FillFuelQuantity);
            }
            else
            {
                throw new ArgumentException("Incorrect engine");
            }

            r_Vehicles[i_LicenseNumber].UpdateVehicleEngineEnergyPrecentage();
        }
    }
}
