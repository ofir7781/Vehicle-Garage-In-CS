using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {

        }

        public void BatteryCharging(float i_HoursOfElectricEnergyToAdd)
        {
            try
            {
                RemainingEnergy += i_HoursOfElectricEnergyToAdd / 60;
            }
            catch(ValueOutOfRangeException)
            {
                throw new ValueOutOfRangeException("Minutes Of Electric Energy To Add", 0, (r_MaxEnergy - m_RemainingEnergy) * 60);
            }
        }
    }
}
