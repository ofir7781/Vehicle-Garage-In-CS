using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinimumValue;
        private float m_MaximumValue;

        public ValueOutOfRangeException(string i_MessageForException, float i_MinimumValue, float i_MaximumValue) : base(i_MessageForException)
        {
            m_MinimumValue = i_MinimumValue;
            m_MaximumValue = i_MaximumValue;
        }

        public override string ToString()
        {
            return string.Format("{0} must be between {1:f} and {2:f}, please try again.", Message, m_MinimumValue, m_MaximumValue);
        }
    }
}
