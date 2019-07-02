using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected readonly float r_MaxEnergy;
        protected float m_RemainingEnergy;

        public Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public float RemainingEnergy
        {
            get
            {
                return m_RemainingEnergy;
            }

            set
            {
                if (value <= r_MaxEnergy && value > m_RemainingEnergy)
                {
                    m_RemainingEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("The energy", 1, r_MaxEnergy); 
                }
            }
        }

        public void SetEnergyByPercentage(float i_PercentageToAdd)
        {
            m_RemainingEnergy = (i_PercentageToAdd / 100) * r_MaxEnergy;
        }

        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();
            engineInfo.Append(string.Format("The maximum possible energy is: {0:f}.{1}", r_MaxEnergy, Environment.NewLine));
            engineInfo.Append(string.Format("The current energy is: {0:f}.{1}", m_RemainingEnergy, Environment.NewLine));
            return engineInfo.ToString();
        }
    }
}
