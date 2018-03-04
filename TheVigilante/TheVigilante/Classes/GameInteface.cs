using System;
using System.IO;
using System.Threading;

namespace TheVigilante.Classes
{
    public class GameInterface
    {
        private string databaseConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TheVigilante;Integrated Security=True";
        private string originStory = @"C:\Users\jchristian\Desktop\TheVigilante\etc\OriginStory.txt";
        private bool firstMenu = true;
        private bool atHome = true;
        private bool atStore = false;
        private bool newOrLoad = true;
        private string selection;
        private string connectionString;


        public void RunInterface()
        {
            while (newOrLoad)
            {
                Console.WriteLine();
                Console.WriteLine(" 1. New Game\n\n 2. Load Game\n\n");
                selection = Console.ReadLine();
                Console.WriteLine();


                if (selection == "1")
                {
                    Console.Clear();
                    Console.WriteLine(" What is your name? she asked.\n");
                    PlayerClass.SetName(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($" \"{PlayerClass.PlayerName},\" you responded. As you pressed your hands firmly against her stomach.\n\n");

                    OriginWriter();
                    while (firstMenu)
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Enter \"1\" to go home. \n\n");
                        selection = Console.ReadLine();
                        if (selection == "1")
                        {
                            while (atHome)
                            {
                                GoHome();

                                firstMenu = false;

                            }
                        }
                        else
                        {
                            Console.WriteLine(" You're not very good at directions, eh?  Maybe the vigilante business is too dangerous for you?  I have an idea; take up knitting.\n\n");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                    }
                }
                else if (selection == "2")
                {
                    //Print list of available games to load based

                    //DAL loadGame = new DAL();
                    //LoadGame()
                    newOrLoad = false;

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Fat fingers or dumb brain?  Try again.");
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    Console.Clear();
                    newOrLoad = true;
                }

            }
        }
        public void OriginWriter()
        {
            try
            {
                using (StreamReader sr = new StreamReader(originStory))
                {

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Console.WriteLine(line);
                        //Thread.Sleep(1000);

                    }
                }
            }
            catch { throw; }
        }
        public void GoHome()
        {
            PlayerClass.PlayerHitPoints = PlayerClass.MaxHitPoints;
            Console.Clear();
            while (atHome)
            {
                Console.WriteLine();
                Console.WriteLine(" You're home now.  You hate this place.\n\n You're feeling rested enough and ready to move.\n\n");
                Console.WriteLine(" 1. \"Store\"\n\n 2. Go Fight\n\n 3. Save Game\n\n 4. Quit Game\n\n");
                selection = Console.ReadLine();
                if (selection == "1")
                {
                    Console.Clear();
                    newOrLoad = false;
                    atStore = true;
                    atHome = false;
                    firstMenu = false;
                    Console.WriteLine();
                    PrintStoreOptions();
                    Console.WriteLine();
                }
                else if (selection == "2")
                {
                    Console.Clear();
                    Console.WriteLine();
                    GoFight();
                    Console.WriteLine();
                }
                else if (selection == "3")
                {
                    Console.Clear();
                    Console.WriteLine();
                    UpdateSaveGame();
                    Console.WriteLine();
                }
                else if (selection == "4")
                {
                    Environment.Exit(0);
                }
            }
        }
        //To do
        public void PrintStoreOptions()
        {
            while (atStore)
            {
                Console.WriteLine();
                Console.WriteLine(" Which would you like to see?\n\n 1. Weapons \n\n 2. Armor\n\n");
                selection = Console.ReadLine();
                Console.WriteLine();
                if (selection == "1")
                {
                    Console.Clear();
                    //PrintWeaponList();
                    Console.WriteLine();
                }
                else if (selection == "2")
                {
                    Console.Clear();
                    //PrintArmorList();
                    Console.WriteLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(" Stop wasting my time and pick something.");
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }
        }
        public void PrintWeapons()
        {

        }
        public void PrintArmor()
        {

        }

        public void GoFight()
        {
            bool chooseFight = true;
            while (chooseFight)
            {
                CriminalClass.CreateCriminal();
                Console.WriteLine();
                Console.WriteLine(" You decide to walk the streets wher you knkow you'll find trouble.\n\n");
                Console.WriteLine($" You find {CriminalClass.CriminalName}  Level: {CriminalClass.CriminalLevel}  Crime: {CriminalClass.CrimeCommited}\n\n");
                Console.WriteLine(" 1. Fight\n\n 2. Run\n\n");
                selection = Console.ReadLine();
                Console.WriteLine();
                if (selection == "1")
                {
                    CombatClass newCombat = new CombatClass();
                    newCombat.FightOrder();
                    chooseFight = false;
                }
                else if (selection == "2")
                {
                    //Run()
                    chooseFight = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Geesh, you're not very good at this.\n\n");
                    chooseFight = true;
                }
            }
        }

        public void UpdateSaveGame()
        {
            Console.WriteLine();
            Console.WriteLine(" Would you like to save your game?\n\n (Y)es or (N)o?\n\n");
            selection = Console.ReadLine();
            Console.WriteLine();
            if (selection == "y" || selection == "yes")
            {
                Console.WriteLine();
                if (PlayerClass.SaveID > 0)
                {
                    DAL save = new DAL(connectionString);
                    save.ReSaveFileList(PlayerClass.SaveID);
                }
                else
                {
                    DAL save = new DAL(connectionString);
                    save.InsertSaveFile();
                }
            }
        }
    }
}
