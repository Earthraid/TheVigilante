using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheVigilante.Classes
{
    public class Weapon
    {
        public int Weapon_ID
        {
            get; set;
        }
        public string Weapon_Type
        {
            get; set;
        }
        public int Weapon_Damage
        { get; set; }
        public int Weapon_Cost
        {
            get; set;
        }

        public override string ToString()
        {
            return Weapon_ID.ToString().PadRight(5) + Weapon_Type.PadRight(10) + Weapon_Damage.ToString().PadRight(5) + Weapon_Cost.ToString("C");
        }
    }
}
