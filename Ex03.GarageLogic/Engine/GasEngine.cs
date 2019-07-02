using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        public enum eGasType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        private readonly eGasType r_GasTypeOfVehicle;

        public GasEngine(eGasType i_GasType, float i_MaxEnergy) : base(i_MaxEnergy)
        {
            r_GasTypeOfVehicle = i_GasType;
        }

        public void VehicleFueling(eGasType i_GasType, float i_AmountOfGasToAddInLiters)
        {
            if (i_GasType == r_GasTypeOfVehicle)
            {
                if (RemainingEnergy == MaxEnergy)
                {
                    throw new Exception("Tank is already full.");
                }
                else
                {
                    try
                    {
                        RemainingEnergy += i_AmountOfGasToAddInLiters;
                    }
                    catch (ValueOutOfRangeException)
                    {
                        throw new ValueOutOfRangeException("Valid liters amount", 1.0f, r_MaxEnergy - m_RemainingEnergy);
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            StringBuilder gasEngineInfo = new StringBuilder();
            gasEngineInfo.Append(string.Format("The gas type of the vehicle is: {0}.{1}", r_GasTypeOfVehicle, Environment.NewLine));
            gasEngineInfo.Append(base.ToString());
            return gasEngineInfo.ToString();
        }
    }
}
