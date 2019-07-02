using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class OwnerInfo
    {
        public enum eVehicleSatuses
        {
            Repair = 1,
            Repaired,
            Paid
        }

        private eVehicleSatuses m_VehicleStatus;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;

        public OwnerInfo(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_VehicleStatus = eVehicleSatuses.Repair;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public eVehicleSatuses VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                switch (value)
                {
                    case eVehicleSatuses.Repair:
                        m_VehicleStatus = eVehicleSatuses.Repair;
                        break;
                    case eVehicleSatuses.Repaired:
                        m_VehicleStatus = eVehicleSatuses.Repaired;
                        break;
                    case eVehicleSatuses.Paid:
                        m_VehicleStatus = eVehicleSatuses.Paid;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder ownerInfo = new StringBuilder();
            ownerInfo.Append(string.Format("The vehicle current status is: {0}.{1}", m_VehicleStatus, Environment.NewLine));
            ownerInfo.Append(string.Format("The owner name is: {0}.{1}", m_OwnerName, Environment.NewLine));
            ownerInfo.Append(string.Format("The owner phone number is: {0}.{1}", m_OwnerPhoneNumber, Environment.NewLine));
            return ownerInfo.ToString();
        }
    }    
}