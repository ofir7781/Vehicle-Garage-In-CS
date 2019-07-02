using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_Manufacturer;
        private readonly float r_MaximumAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_Manufacturer, float i_MaximumAirPressure, float i_CurrentAirPressure)
        {
            r_MaximumAirPressure = i_MaximumAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_Manufacturer = i_Manufacturer;

            if (i_CurrentAirPressure > r_MaximumAirPressure || i_CurrentAirPressure < 0)
            {
                throw new ValueOutOfRangeException("Current air pressure", 0, r_MaximumAirPressure);
            }
        }

        public void VehicleTireInflationMax()
        {
            m_CurrentAirPressure = r_MaximumAirPressure;
        }

        public override string ToString()
        {
            StringBuilder wheelInfo = new StringBuilder();
            wheelInfo.Append(string.Format("The wheel manufacturer is {0}.{1}", r_Manufacturer, Environment.NewLine));
            wheelInfo.Append(string.Format("The maximum air pressure of the wheel is {0:f}.{1}", r_MaximumAirPressure, Environment.NewLine));
            wheelInfo.Append(string.Format("The current air pressure of the wheel is {0:f}.{1}", m_CurrentAirPressure, Environment.NewLine));
            return wheelInfo.ToString();
        }
    }
}
