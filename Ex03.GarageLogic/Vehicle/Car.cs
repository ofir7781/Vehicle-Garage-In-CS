using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColors
        {
            Gray = 1,
            Blue,
            White,
            Black        
        }

        public enum eCarNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private eCarColors m_CarColor;
        private eCarNumberOfDoors m_CarNumOfDoors;

        public Car(int i_NumOfWheels, float i_MaxAirPressure, Engine i_Engine, string i_LicenseNumber) : base(i_Engine, i_LicenseNumber)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
        }

        public eCarColors CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                switch (value)
                {
                    case eCarColors.Black:
                        m_CarColor = eCarColors.Black;
                        break;
                    case eCarColors.Blue:
                        m_CarColor = eCarColors.Blue;
                        break;
                    case eCarColors.Gray:
                        m_CarColor = eCarColors.Gray;
                        break;
                    case eCarColors.White:
                        m_CarColor = eCarColors.White;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public eCarNumberOfDoors CarNumOfDoors
        {
            get
            {
                return m_CarNumOfDoors;
            }

            set
            {
                switch (value)
                {
                    case eCarNumberOfDoors.Two:
                        m_CarNumOfDoors = eCarNumberOfDoors.Two;
                        break;
                    case eCarNumberOfDoors.Three:
                        m_CarNumOfDoors = eCarNumberOfDoors.Three;
                        break;
                    case eCarNumberOfDoors.Four:
                        m_CarNumOfDoors = eCarNumberOfDoors.Four;
                        break;
                    case eCarNumberOfDoors.Five:
                        m_CarNumOfDoors = eCarNumberOfDoors.Five;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override List<List<string>> UniqueAttributes()
        {
            List<List<string>> carUniqueAttributes = new List<List<string>>();

            // first attribute, car color
            List<string> carColor = new List<string>();

            // carColor[0] is the message
            carColor.Add(@"Please choose color:
1. Gray
2. Blue
3. White
4. Black"); 

            // carColor[1] is the method name that will do it
            carColor.Add("SetCarColor");

            // second attribute, car number of doors
            List<string> carDoors = new List<string>();

            // carDoors[0] is the message
            carDoors.Add(@"Please choose the number of doors:
1. Two
2. Three
3. Four
4. Five");

            // carDoors[1] is the method name that will do it
            carDoors.Add("SetCarNumOfDoors");

            // finally, we add the two attributes to the main list of lists
            carUniqueAttributes.Add(carColor);
            carUniqueAttributes.Add(carDoors);
            return carUniqueAttributes;
        }

        public void SetCarColor(string i_Color)
        {
            try
            {
                int parsedColor = int.Parse(i_Color);
                CarColor = (eCarColors)parsedColor;
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

        public void SetCarNumOfDoors(string i_NumOfDoors)
        {
            try
            {
                int parsedNumOfDoors = int.Parse(i_NumOfDoors);
                CarNumOfDoors = (eCarNumberOfDoors)parsedNumOfDoors;
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

        public override string ToString()
        {
            StringBuilder carInfo = new StringBuilder();
            carInfo.Append(base.ToString());
            carInfo.Append(string.Format("The car color is: {0}.{1}", m_CarColor, Environment.NewLine));
            carInfo.Append(string.Format("The car number of doors is: {0}.", m_CarNumOfDoors));
            return carInfo.ToString();
        }
    }
}
