using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheVigilante.Classes
{
    public class Armor
    {
        public int Armor_ID
        {
            get; set;
        }
        public string Armor_Type
        {
            get; set;
        }
        public int Armor_Value
        {
            get; set;
        }
        public int Armor_Cost
        {
            get; set;
        }

        public override string ToString()
        {
            return Armor_ID.ToString().PadRight(5) + Armor_Type.PadRight(25) + Armor_Value.ToString().PadRight(5) + Armor_Cost.ToString("C");
        }
    }
}
