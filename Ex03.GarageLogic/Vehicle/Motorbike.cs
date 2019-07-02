using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorbike : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            B2
        }

        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorbike(int i_NumOfWheels, float i_MaxAirPressure, Engine i_Engine, string i_LicenseNumber) : base(i_Engine, i_LicenseNumber)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                switch (value)
                {
                    case eLicenseType.A:
                        m_LicenseType = eLicenseType.A;
                        break;
                    case eLicenseType.A1:
                        m_LicenseType = eLicenseType.A1;
                        break;
                    case eLicenseType.B1:
                        m_LicenseType = eLicenseType.B1;
                        break;
                    case eLicenseType.B2:
                        m_LicenseType = eLicenseType.B2;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }         
        }

        public override List<List<string>> UniqueAttributes()
        {
            List<List<string>> motorbikeUniqueAttributes = new List<List<string>>();

            // first attribute, license type
            List<string> licenseType = new List<string>();

            // licenseType[0] is the message
            licenseType.Add(@"Please choose license type:
1. A
2. A1
3. B1
4. B2"); 

            // licenseType[1] is the method name that will do it
            licenseType.Add("SetLicenseType");

            // second attribute, engine volume
            List<string> engineVolume = new List<string>();

            // engineVolume[0] is the message
            engineVolume.Add("Please enter the engine volume:");

            // engineVolume[1] is the method name that will do it  
            engineVolume.Add("SetEngineVolume");

            // finally, we add the two attributes to the main list of lists
            motorbikeUniqueAttributes.Add(licenseType);
            motorbikeUniqueAttributes.Add(engineVolume);
            return motorbikeUniqueAttributes;
        }

        public void SetLicenseType(string i_LicenseType)
        {
            try
            {
                int parsedLicenseType = int.Parse(i_LicenseType);
                LicenseType = (eLicenseType)parsedLicenseType;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void SetEngineVolume(string i_EngineVolume)
        {
            try
            {
                int parsedEngineVolume = int.Parse(i_EngineVolume);
                if (parsedEngineVolume > 0)
                {
                    m_EngineVolume = parsedEngineVolume;
                }
                else
                {
                    throw new Exception("Engine volume must be a positive number, please try again.");
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public override string ToString()
        {
            StringBuilder motorbikeInfo = new StringBuilder();
            motorbikeInfo.Append(base.ToString());
            motorbikeInfo.Append(string.Format("The motorbike license type is: {0}.{1}", m_LicenseType, Environment.NewLine));
            motorbikeInfo.Append(string.Format("The motorbike engine volume is: {0}.", m_EngineVolume));
            return motorbikeInfo.ToString();
        }
    }
}
