using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheVigilante.Classes
{
    public static class CriminalClass
    {
        private static string criminalName;
        private static int criminalMoney;
        private static string crimeCommitted;
        private static int criminalHitPoints;
        private static int criminalLevel;
        private static int criminalMaxHitPoints;
        private static int criminalExperience;


        private static string[] firstNames = new string[] { "Jimmy", "Mark", "Lars", "Carl", "Sam", "Jenny", "Lefty", "Righty", "Kevin" }; //Add more names
        private static string[] lastNames = new string[] { "The Shark", "Cuban", "Vorp", "Two Times", "Macintosh", "The Fist", "The Whole Fist", "Smith" }; //Add more names
        private static string[] crimeList = new string[] { "Murder", "Robbery", "Loitering", "Mugging", "Child Abuse", "Being a Garbage Human Being in General" };//Add more crimes

        public static string CriminalName
        {
            get { return criminalName; }
        }
        public static int CriminalMaxHealth
        {
            get { return criminalMaxHitPoints; }
        }
        public static int CriminalLevel
        {
            get { return criminalLevel; }
        }
        public static string CrimeCommited
        {
            get { return crimeCommitted; }
        }
        public static int CriminalMoney
        {
            get { return criminalMoney; }

        }
        public static int CriminalHitPoints
        {
            get { return criminalHitPoints; }
            set { criminalHitPoints = value; }
        }
        public static int CriminalExperience
        {
            get { return criminalExperience; }
        }

        //Constructor
        static CriminalClass()
        {
            criminalMoney = (criminalLevel * 2) + 10;
            criminalExperience = CriminalLevel * 10 + 100;
        }

        //Creates new criminal on GoFight
        public static void CreateCriminal()
        {
            //Create full name of criminal
            Random rnd = new Random();
            criminalName = firstNames[rnd.Next(0, firstNames.Length)] + " " + lastNames[rnd.Next(0, lastNames.Length)];
            //Create crime
            crimeCommitted = crimeList[rnd.Next(0, crimeList.Length)];
            //Create criminal level
            criminalLevel = rnd.Next(-1, 1) + PlayerClass.PlayerLevel;
            //Criminal money
            criminalMoney = (criminalLevel * 2) + 20;
            //Creates health stat for criminal
            criminalHitPoints = (criminalLevel * 10) + 90;
            //Creates amount of experience given to player upon defeat
            criminalExperience = (criminalLevel * 18) + 50;
        }

        //May have done this in Combat Class
        public static int TakeDamage(int damage)
        {
            return criminalHitPoints -= damage;
        }

        //Resets criminal hit points to max
        public static int CriminalMaxHitPoints()
        {
            return criminalHitPoints = (criminalLevel * 10) + 90;
        }

        //Sets experience amount to give to player
        public static int CriminalGivesExperience()
        {
            return criminalExperience = CriminalLevel * 10 + 100;
        }

        //Need to create a list of three criminals to select, later
    }
}

