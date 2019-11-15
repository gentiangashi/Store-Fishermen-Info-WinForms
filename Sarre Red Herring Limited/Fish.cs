using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarre_Red_Herring_Limited
{
    class Fish
    {
        private string Species;
        private decimal Factor;
        private int Quota;

        public Fish()
        {

        }

        public Fish(string Name, decimal factor, int qouta)
        {
            Species = Name;
            Factor = factor;
            Quota = qouta;
        }

        public string GetSpecies()
        {
            return Species;
        }

        public decimal GetFactor()
        {
            return Factor;
        }

        public int GetQuota()
        {
            return Quota;
        }
    }    
}
