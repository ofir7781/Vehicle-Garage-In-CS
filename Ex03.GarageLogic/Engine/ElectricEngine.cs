using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {
        }

        public void BatteryCharging(float i_HoursOfElectricEnergyToAdd)
        {
            if (RemainingEnergy == MaxEnergy)
            {
                throw new Exception("Battery is already full.");
            }
            else
            {
                try
                {
                    RemainingEnergy += i_HoursOfElectricEnergyToAdd / 60;
                }
                catch (ValueOutOfRangeException)
                {
                    throw new ValueOutOfRangeException("Valid minutes amount", 1.0f, (r_MaxEnergy - m_RemainingEnergy) * 60);
                }
            }
        }
    }
}
