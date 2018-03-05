using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TheVigilante.Classes
{
    public static class PlayerClass
    {
        private static string databaseConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TheVigilante;Integrated Security=True";
        private static string playerName;
        private static int playerLevel;
        private static int playerHitPoints;
        private static int maxHitPoints;
        private static int currentExp;
        private static int nextLevelExp;
        private static string ownedWeaponType = "";
        private static int ownedWeaponDamage = 0;
        private static string ownedArmorType = "";
        private static int ownedArmorValue = 0;
        private static int playerMoney = 0;
        private static int saveID = 0;

        //Properties
        public static int SaveID
        { get { return saveID; } }
        public static string PlayerName
        {
            get { return playerName; }
        }
        public static int PlayerLevel
        {
            get { return playerLevel; }
        }
        public static int PlayerHitPoints
        {
            get { return playerHitPoints; }
            set { playerHitPoints = value; }
        }
        public static int MaxHitPoints
        {
            get { return maxHitPoints; }
        }
        public static int CurrentExp
        {
            get { return currentExp; }
        }
        public static int NextLevelExp
        {
            get { return nextLevelExp; }
        }
        public static string OwnedWeaponType
        {
            get { return ownedWeaponType; }
        }
        public static int OwnedWeaponDamage
        {
            get { return ownedWeaponDamage; }
            set { ownedWeaponDamage = value; }
        }
        public static string OwnedArmorType
        {
            get { return ownedArmorType; }
            set { ownedArmorType = value; }
        }
        public static int OwnedArmorValue
        {
            get { return ownedArmorValue; }
            set { ownedArmorValue = value; }
        }
        public static int PlayerMoney
        {
            get { return playerMoney; }
            set { playerMoney = value; }
        }

        //Constructor
        static PlayerClass()
        {
            playerLevel = 1;
            maxHitPoints = (playerLevel * 10) + 90;
        }

        //Methods

        //Sets player name throughout game
        public static string SetName(string input)
        {
            return playerName = input;
        }

        //Calculate next level experience
        public static int NextLevelExperience()
        {
            return nextLevelExp = PlayerLevel * 20 + 100;
        }

        //Adds experience to total amount after battle
        public static int AddExp(int input)
        {
            return currentExp += input;
        }

        //Increments level based on experience
        public static int LevelUp()
        {
            if (currentExp >= nextLevelExp)
            {
                Console.WriteLine(" You leveled up.  You suck a little less at some things now.");
                Thread.Sleep(2000);
                return playerLevel += 1;
            }
            return playerLevel;
        }

        //Sets max hit points based on level
        public static int SetMaxHitPoints()
        {
            return maxHitPoints = (playerLevel * 10) + 90;
        }

        //Saves information on owned weapons from the store.
        public static void SetWeapon(string weaponType, int weaponDamage)
        {

            ownedWeaponType = weaponType;
            ownedWeaponDamage = weaponDamage;

        }

        //Saves information on owned armor from the strore.
        public static void SetArmor(string armorType, int armorValue)
        {
            ownedArmorType = armorType;
            ownedArmorValue = armorValue;
        }

        //Sets saveID to IDENTITY in game_file
        public static void SaveGame()
        {
            Console.Clear();
            Console.WriteLine("\n Are you sure you wish to save?\n\n(Y)es or (N)o?\n\n");
            Console.Write(" "); string selection = Console.ReadLine().ToLower();
            Console.WriteLine();

            if (selection == "y" || selection == "yes")
            {
                if (SaveID == 0)
                {
                    Console.Clear();
                    DAL save = new DAL(databaseConnectionString);
                    save.InsertSaveFile();
                    
                }
                else
                {
                    Console.Clear();
                    DAL save = new DAL(databaseConnectionString);
                    save.UpdateSaveFileList(SaveID);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Ok, game not saved. \n\n Why'd you even come here?\n\n");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        //Ensures save-id exists in game
        public static void CheckSave(int newsaveID)
        {
            saveID = newsaveID;

            if (saveID > 0)
            {
                Console.Clear();
                Console.WriteLine("\n Save successful.\n\n");
                Thread.Sleep(2000);
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Save failed.\n\n");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        //Adds money to player bank after battle victory
        public static decimal AddPlayerMoney(int foundMoney)
        {
            return playerMoney += foundMoney;
        }

        //Spends money in store or in case of fight loss or run
        public static decimal SpendPlayerMoney(int spentMoney)
        {
            return playerMoney -= spentMoney;
        }

        //Ends fight based on criterion from Combat Class
        public static void EndFight(bool winFight, int money)
        {
            if (winFight)
            {
                CriminalClass.CriminalMaxHitPoints();
                AddExp(CriminalClass.CriminalExperience);
                NextLevelExperience();
                LevelUp();
                AddPlayerMoney(money);
                GameMenus thisMenu = new GameMenus();
                thisMenu.GoHome();
            }
            else
            {
                CriminalClass.CriminalMaxHitPoints();
                SpendPlayerMoney(money);
                GameMenus thisMenu = new GameMenus();
                thisMenu.GoHome();
            }


        }

        //Loads game information
        public static void LoadGame(int save_id, string player_name, int player_level, int player_money, string owned_weapon_type, int owned_weapon_damage, string owned_armor_type, int owned_armor_value)
        {
            saveID = save_id;
            playerName = player_name;
            playerLevel = player_level;
            playerMoney = player_money;
            ownedWeaponType = owned_weapon_type;
            ownedWeaponDamage = owned_weapon_damage;
            ownedArmorType = owned_armor_type;
            ownedArmorValue = owned_armor_value;


        }
    }
}
