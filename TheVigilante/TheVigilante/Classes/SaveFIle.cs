using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheVigilante.Classes
{
    class SaveFile
    {
        public int SaveId
        {
            get; set;
        }

        public string PlayerName
        {
            get; set;
        }

        public int PlayerLevel
        {
            get; set;
        }

        public int PlayerMoney
        {
            get; set;
        }
        
        public string OwnedWeapon
        {
            get; set;
        }
       
        public int OwnedWeaponDMG
        {
            get; set;
        }
       
        public string OwnedArmor
        {
            get; set;
        }
        
        public int OwnedArmorVAL
        {
            get; set;
        }
    }
}
