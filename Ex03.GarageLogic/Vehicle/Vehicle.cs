using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly Engine r_Engine;
        protected readonly string r_LicenseNumber;
        protected List<Wheel> m_Wheels;
        protected float m_MaxAirPressure;
        protected float m_CurrentPercentageOfEngineEnergy;
        protected string m_ModelName;
        protected OwnerInfo m_OwnerInfo;

        public Vehicle(Engine i_Engine, string i_LicenseNumber)
        {
            r_Engine = i_Engine;
            r_LicenseNumber = i_LicenseNumber;
        }

        public Engine Engine
        {
            get
            {
                return r_Engine;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public OwnerInfo OwnerInfo
        {
            get
            {
                return m_OwnerInfo;
            }

            set
            {
                m_OwnerInfo = value;
            }
        }

        public float CurrentPercentageOfEngineEnergy
        {
            get
            {
                return m_CurrentPercentageOfEngineEnergy;
            }

            set
            {
                if (value >= 0 && value <= 100)
                {
                    Engine.SetEnergyByPercentage(value);
                    m_CurrentPercentageOfEngineEnergy += value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Current percentage of engine energy", 0.0f, 100.0f);
                }
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public void UpdateVehicleEngineEnergyPrecentage()
        {
            m_CurrentPercentageOfEngineEnergy = (Engine.RemainingEnergy / Engine.MaxEnergy) * 100;
        }

        public void WheelsInflationMax()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.VehicleTireInflationMax();
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();
            vehicleInfo.Append(string.Format("The license number of the vehicle is {0}.{1}", r_LicenseNumber, Environment.NewLine));
            vehicleInfo.Append(string.Format("The model of the vehicle is {0}.{1}", m_ModelName, Environment.NewLine));
            vehicleInfo.Append(m_OwnerInfo.ToString());
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                vehicleInfo.Append(string.Format("Wheel number {0}:{1}", i + 1, Environment.NewLine));
                vehicleInfo.Append(m_Wheels[i].ToString());
            }

            vehicleInfo.Append(r_Engine.ToString());
            vehicleInfo.Append(string.Format("The current percentage of engine energy is {0}.{1}", m_CurrentPercentageOfEngineEnergy, Environment.NewLine));
            return vehicleInfo.ToString();
        }

        public abstract List<List<string>> UniqueAttributes();
    }
}