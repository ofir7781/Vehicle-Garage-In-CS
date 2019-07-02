using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTrunkCooled;
        private float m_CargoVolume;

        public Truck(int i_NumOfWheels, float i_MaxAirPressure, Engine i_Engine, string i_LicenseNumber) : base(i_Engine, i_LicenseNumber)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
        }

        public override List<List<string>> UniqueAttributes()
        {
            List<List<string>> TruckUniqueAttributes = new List<List<string>>();

            // first attribute, is trunk cooled
            List<string> isTrunkCooled = new List<string>();

            // isTrunkCooled[0] is the message
            isTrunkCooled.Add(@"Is the trunk cooled?
1. Yes
2. No");

            // isTrunkCooled[1] is the method name that will do it
            isTrunkCooled.Add("IsTrunkCooled");

            // second attribute, cargo volume
            List<string> cargoVolume = new List<string>();

            // cargoVolume[0] is the message
            cargoVolume.Add("Please enter the cargo volume:");

            // engineVolume[1] is the method name that will do it  
            cargoVolume.Add("SetCargoVolume");

            // finally, we add the two attributes to the main list of lists
            TruckUniqueAttributes.Add(isTrunkCooled);
            TruckUniqueAttributes.Add(cargoVolume);
            return TruckUniqueAttributes;
        }

        public void IsTrunkCooled(string i_TrunkCooled)
        {
            try
            {
                int parsedTrunkCooled = int.Parse(i_TrunkCooled);
                switch (parsedTrunkCooled)
                {
                    case 1:
                        m_IsTrunkCooled = true;
                        break;
                    case 2:
                        m_IsTrunkCooled = false;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public void SetCargoVolume(string i_CargoVolume)
        {
            try
            {
                int parsedCargoVolume = int.Parse(i_CargoVolume);
                if (parsedCargoVolume > 0)
                {
                    m_CargoVolume = parsedCargoVolume;
                }
                else
                {
                    throw new Exception("Cargo volume must be a positive number, please try again.");
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public override string ToString()
        {
            StringBuilder truckInfo = new StringBuilder();
            truckInfo.Append(base.ToString());
            if(m_IsTrunkCooled == true)
            {
                truckInfo.Append(string.Format("The trunk of the truck is cooled.{0}", Environment.NewLine));
            }
            else
            {
                truckInfo.Append(string.Format("The trunk of the truck is not cooled.{0}", Environment.NewLine));
            }

            truckInfo.Append(string.Format("The truck cargo volume is: {0:f}.", m_CargoVolume));
            return truckInfo.ToString();
        }
    }
}
