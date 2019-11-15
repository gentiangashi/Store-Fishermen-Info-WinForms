using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarre_Red_Herring_Limited
{
    class Fleet
    {
        //Initialize 
        private string boatName;
        private string boatLicense;
        private decimal maximumLoad;
        private decimal quotaFilled;

        public Fleet()
        {
            boatName = "";
            boatLicense = "";
            maximumLoad = 0m;
        }

        // Constructor that takes arguments:
        public Fleet(string name, string license, decimal maxLoad, decimal qouta)
        {
            boatName = name;
            boatLicense = license;
            maximumLoad = maxLoad;
            quotaFilled = qouta;
        }

        //Boat Name Method
        public string GetBoatName()
        {
            return boatName;
        }
       
        //Boat License Method
        public string GetBoatLicense()
        {
            return boatLicense;
        }
        
        //Boat Maximum Load Method
        public decimal GetMaximumLoad()
        {
            return maximumLoad;
        }
        
        //Boat Quota Method
        public decimal GetQuotaFilled()
        {
            return quotaFilled;
        }
    }
}

