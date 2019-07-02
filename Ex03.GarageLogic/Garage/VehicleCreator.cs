using System;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eSupportedVehicles
        {
            GasMotorbike = 1,
            ElectricMotorbike,
            GasCar,
            ElectricCar,
            Truck,
        }

        private const int k_GasMotorbikeNumberOfWheels = 2;
        private const float k_GasMotorbikeMaxAirPreasure = 30.0f;
        private const GasEngine.eGasType k_GasMotoebikeGasType = GasEngine.eGasType.Octan96;
        private const float k_GasMotorbikeGasTankInLiters = 6.0f;

        private const int k_ElectricMotorbikeNumberOfWheels = 2;
        private const float k_ElectricMotorbikeMaxAirPreasure = 30.0f;
        private const float k_ElectricMotorbikeMaxBatteryInHours = 1.8f;

        private const int k_GasCarNumberOfWheels = 4;
        private const float k_GasCarMaxAirPreasure = 32.0f;
        private const GasEngine.eGasType k_GasCarGasType = GasEngine.eGasType.Octan98;
        private const float k_GasCarGasTankInLiters = 45.0f;

        private const int k_ElectricCarNumberOfWheels = 4;
        private const float k_ElectricCarMaxAirPreasure = 32.0f;
        private const float k_ElectricCarMaxBatteryInHours = 3.2f;

        private const int k_TruckNumberOfWheels = 12;
        private const float k_TruckMaxAirPreasure = 28.0f;
        private const GasEngine.eGasType k_TruckGasType = GasEngine.eGasType.Soler;
        private const float k_TruckGasTankInLiters = 115.0f;

        public static Vehicle MakeVehicle(int i_VehicleTypeChoice, string i_LicenseNumber)
        {
            Vehicle newVehicle = null;
            Engine engine = null;
            switch ((eSupportedVehicles)i_VehicleTypeChoice)
            {
                case eSupportedVehicles.GasMotorbike:
                    engine = new GasEngine(k_GasMotoebikeGasType, k_GasMotorbikeGasTankInLiters);
                    newVehicle = new Motorbike(k_GasMotorbikeNumberOfWheels, k_GasMotorbikeMaxAirPreasure, engine, i_LicenseNumber);
                    break;
                case eSupportedVehicles.ElectricMotorbike:
                    engine = new ElectricEngine(k_ElectricMotorbikeMaxBatteryInHours);
                    newVehicle = new Motorbike(k_ElectricMotorbikeNumberOfWheels, k_ElectricMotorbikeMaxAirPreasure, engine, i_LicenseNumber);
                    break;
                case eSupportedVehicles.GasCar:
                    engine = new GasEngine(k_GasCarGasType, k_GasCarGasTankInLiters);
                    newVehicle = new Car(k_GasCarNumberOfWheels, k_GasCarMaxAirPreasure, engine, i_LicenseNumber);
                    break;
                case eSupportedVehicles.ElectricCar:
                    engine = new ElectricEngine(k_ElectricCarMaxBatteryInHours);
                    newVehicle = new Car(k_ElectricCarNumberOfWheels, k_ElectricCarMaxAirPreasure, engine, i_LicenseNumber);
                    break;
                case eSupportedVehicles.Truck:
                    engine = new GasEngine(k_TruckGasType, k_TruckGasTankInLiters);
                    newVehicle = new Truck(k_TruckNumberOfWheels, k_TruckMaxAirPreasure, engine, i_LicenseNumber);
                    break;
                default:
                    throw new ArgumentException();
            }

            return newVehicle;
        }
    }
}
