﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace TheVigilante.Classes
{
    class GameMenus
    {
        private string originPath = @"C:\Users\jchristian\Desktop\TheVigilante\TheVigilante\etc\OriginStory.txt";
        private string readMePath = @"C:\Users\jchristian\Desktop\TheVigilante\TheVigilante\etc\README.txt";
        const string DatabaseConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TheVigilante;Integrated Security=True";
       
        private List<Weapon> weapons;
        private List<Armor> armor;
        private List<SaveFile> saves;
        private bool atHome = true;
        private bool atStore = true;
        private bool newOrLoad = true;
        private bool firstMenu = true;
        

        //Start of game
        public void RunInterface()
        {
            while (newOrLoad)
            {
                Console.WriteLine("\n 1. New Game\n\n 2. Load Game\n\n 3. Read Me\n\n");
                Console.Write(" "); string selection = Console.ReadLine();

                //New game
                if (selection == "1")
                {
                    Console.Clear();
                    Console.WriteLine("\n What is your name? she asked.\n");
                    Console.Write(" "); PlayerClass.SetName(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($" \"{PlayerClass.PlayerName},\" you responded. As you pressed your hands firmly against her stomach.\n\n");

                    OriginWriter();

                    Console.ReadLine();
                    //Gives the first "Go Home" option
                    while (firstMenu)
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Enter \"1\" to go home. \n\n");
                        Console.Write(" "); selection = Console.ReadLine();

                        //Sends home for the first time
                        if (selection == "1")
                        {
                            atHome = true;
                            firstMenu = false;
                            newOrLoad = false;
                            GoHome();

                        }

                        //mocks fail
                        else
                        {
                            Console.WriteLine(" You're not very good at directions, eh?  Maybe the vigilante business is too dangerous for you?  I have an idea; take up knitting.\n\n");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                    }
                }

                //Load Game
                else if (selection == "2")
                {
                    //Print list of available games to load
                    PrintLoadList(selection);
                    LoadGame();
                    Thread.Sleep(2000);
                    GoHome();
                }

                //Prints README.txt
                else if (selection == "3")
                {
                    Console.Clear();
                    Console.WriteLine();
                    PrintReadMe();
                    Console.WriteLine("\n Press Enter key to continue. \n");
                    Console.ReadLine();
                }

                //Mock fail
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n Fat fingers or dumb brain?  Try again.");
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    Console.Clear();
                    newOrLoad = true;
                }

            }
        }

        //Writes origin story if not loaded
        public void OriginWriter()
        {
            using (StreamReader sr = new StreamReader(originPath))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                    //Thread.Sleep(1000);
                }
            }
        }

        //Prints the read me file
        public void PrintReadMe()
        {
            using (StreamReader sr = new StreamReader(readMePath))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                 
                }
            }
        }

        //First GoHome menu entry from origin.
        public void FirstTimeHomeMenu(string input)
        {
            while (atHome)
            {
                Console.Clear();
                //Goes home
                if (input == "1")
                {
                    Console.WriteLine();
                    GoHome();
                    Console.WriteLine();
                }
                //Mock fail
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" The game hasn't even started and you're messing up simple instructions.  Perhaps fighting crime isn't for you?  Perhaps you should try something a little more risk averse, like knitting.\n");
                    Console.WriteLine();
                }
            }
        }

        //Player "goes home" fills health can go to store, fight criminals, save or quit.
        public void GoHome()
        {
            Console.Clear();
            while (atHome)
            {
                Console.WriteLine($" Name: {PlayerClass.PlayerName}  Level: {PlayerClass.PlayerLevel}  Bank Account: {PlayerClass.PlayerMoney.ToString("C")}");
                PlayerClass.PlayerHitPoints = PlayerClass.MaxHitPoints;
                Console.WriteLine("\n You're home.  You hate it here.\n");
                Console.WriteLine(" You're feelng rested and ready to move. \n \n What would you like to do?\n");
                Console.WriteLine($" Money: {PlayerClass.PlayerMoney.ToString("C")}\n\n");
                Console.WriteLine(" 1. \"Store\"\n\n 2. Go fight\n\n 3. Save Game\n\n 4. Quit Game\n\n");
                Console.Write(" "); string homeSelection = Console.ReadLine();
                Console.WriteLine();

                //Got to store
                if (homeSelection == "1")
                {
                    atStore = true;
                    while (atStore)
                    {
                        GoToStore();
                        
                    }
                }

                //Fight a criminal
                else if (homeSelection == "2")
                {
                    GoToFight();
                   
                }

                //Save your game
                else if (homeSelection == "3")
                {
                    PlayerClass.SaveGame();
                }

                //Quit game
                else if (homeSelection == "4")
                {
                    Environment.Exit(0);
                }

                //Mock your fail
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n Make a choice available on the screen.  You're probably not going to make it long at this rate.");
                    Console.WriteLine();
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }
        }

        //Prints weapon list
        public void PrintWeapons(string selection)
        {
            DAL newWeaponList = new DAL(DatabaseConnectionString);
            weapons = newWeaponList.WeaponList(Int32.Parse(selection));
            int count = weapons.Count();
            Console.Clear();
            Console.WriteLine("\n No.     Type      Damage      Cost");
            Console.WriteLine("____________________________________");

            foreach (Weapon w in weapons)
            {
                Console.WriteLine($"  {((weapons.IndexOf(w)) + 1).ToString().PadRight(5)} {w.Weapon_Type.PadRight(10)}   {w.Weapon_Damage.ToString()}       {w.Weapon_Cost.ToString("C")}");
            }
            Console.WriteLine();
            Console.WriteLine("Money: " + PlayerClass.PlayerMoney.ToString("C"));
            Console.WriteLine();

        }

        //Prints armor list
        public void PrintArmor(string selection)
        {
            DAL newArmorList = new DAL(DatabaseConnectionString);
            armor = newArmorList.ArmorList(Int32.Parse(selection));
            int count = armor.Count();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\n No.     Type             Damage      Cost");
            Console.WriteLine("___________________________________________");

            foreach (Armor a in armor)
            {
                Console.WriteLine($" {((armor.IndexOf(a)) + 1).ToString().PadRight(5)}{a.Armor_Type.PadRight(20)}   {a.Armor_Value.ToString().PadRight(5)} {a.Armor_Cost.ToString("C")}");
            }

            Console.WriteLine("\n Money: " + PlayerClass.PlayerMoney.ToString("C") + "\n");
        }

        //Go to store
        public void GoToStore()
        {
            Console.WriteLine($" Name: {PlayerClass.PlayerName}  Level: {PlayerClass.PlayerLevel}  Bank Account: {PlayerClass.PlayerMoney.ToString("C")}");
            Console.Clear();
            Console.WriteLine("\n Which would you like to purchase? \n\n 1. Weapons \n\n 2. Armor\n\n \"B\" to go back home.\n");
            Console.Write(" "); string selection = Console.ReadLine().ToLower();
            Console.WriteLine();

            //Prints weapons to purchase
            if (selection == "1")
            {
                PrintWeapons(selection);
                atStore = false;
                GoToWeaponStore();
            }

            //Prints armor to purchase
            else if (selection == "2")
            {
                PrintArmor(selection);
                atStore = false;
                GoToArmorStore();
            }

            //Goes back to previuos menu
            else if (selection == "b")
            {
                Console.Clear();
                Console.WriteLine("\n Headed back home.  You hate that dump.\n");

                atHome = true;
                atStore = false;
            }

            //mocks fail
            else
            {
                Console.Clear();
                if (PlayerClass.PlayerMoney != 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n The directions weren't clear enough?  You drop some money because you're an idiot.\n");
                    PlayerClass.SpendPlayerMoney(1);
                    Console.WriteLine("\n You somehow got lost and ended up back home... Wow...\n");
                    Thread.Sleep(3000);
                    Console.Clear();
                    atHome = true;
                    atStore = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n Not super good at directions, eh?  This vigilante thing is going to work out great...\n");
                    Console.WriteLine(" You somehow got lost and ended up back home... Wow...\n");
                    Thread.Sleep(3000);
                    Console.Clear();
                    atHome = true;
                    atStore = false;
                }
            }
        }

        //Go to weapon store
        public void GoToWeaponStore()
        {
            Console.WriteLine(" Choose something to purchase or select \"B\" to go back.");
            Console.Write(" "); string selection = Console.ReadLine().ToLower();

            //Goes back to store menu
            if (selection == "b")
            {
                atStore = true;
            }

            //Try parses a selection input
            else if (Int32.TryParse(selection, out int outNum))
            {
                //Confirms selection is a valid input number
                if (outNum > 0 && outNum <= weapons.Count)
                {
                    selection = weapons[(Int32.Parse(selection) - 1)].Weapon_ID.ToString();
                    Console.Clear();
                    Console.WriteLine($"\n You sure this is what you want?\n");
                    Console.WriteLine("  No.   Type    Damage   Cost");
                    Console.WriteLine(" _____________________________");
                    Console.WriteLine("  " + weapons[Int32.Parse(selection) - 1] + "\n");
                    Console.WriteLine();
                    Console.WriteLine(" (Y)es or (N)o?\n");
                    Console.Write(" "); string input = Console.ReadLine().ToLower();
                    
                    //Confirms weapon purchase and subtracts cost from player money
                    ConfirmWeaponPurchase(selection, input);
                }

                //mocks fail
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n Can't count, eh?");
                    Thread.Sleep(2000);
                    Console.WriteLine();
                    atStore = true;

                }
            }

            //if try parse fails returns to store menu
            else
            {
                Console.Clear();
                Console.WriteLine("\n Please push a button that makes sense. Idiot...");
                Console.WriteLine();
                Thread.Sleep(2000);
                atStore = true;
            }
        }

        //Go to armor store
        public void GoToArmorStore()
        {
            Console.WriteLine("\n Choose something to purchase or select \"B\" to go back.");
            Console.Write(" ");string selection = Console.ReadLine().ToLower();

            //Goes back to store menu
            if (selection == "b")
            {
                atStore = true;
            }

            //Attempts to parse selection input
            else if (Int32.TryParse(selection, out int outNum))
            {
                //Validates input selection
                if (outNum > 0 && outNum <= armor.Count)
                {
                    selection = armor[(Int32.Parse(selection) - 1)].Armor_ID.ToString();
                    Console.Clear();
                    Console.WriteLine($"\n You sure this is what you want?\n");
                    Console.WriteLine(" No.     Type                Damage    Cost");
                    Console.WriteLine("____________________________________________");
                    Console.WriteLine(" " + armor[Int32.Parse(selection) - 1] + "\n");
                    Console.WriteLine();
                    Console.WriteLine(" (Y)es or (N)o?\n");
                    string input = Console.ReadLine().ToLower();
                    
                    //Confirms armor purchase and subtracts money
                    ConfirmArmorPurchase(selection, input);
                    
                }

                //mocks fail
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n Can't count, eh?\n");
                    Thread.Sleep(2000);
                    atStore = true;

                }
            }

            //mocks fail
            else
            {
                Console.Clear();
                Console.WriteLine("\n Please push a button that makes sense. Idiot...\n");
                Thread.Sleep(2000);
                atStore = true;
            }
        }

        //Go to fight a criminal
        public void GoToFight()
        {
            Console.Clear();
            CriminalClass.CreateCriminal();
            Console.WriteLine($"\n You decide to stroll the neighborhoods where you know criminals hide.\n\n You find {CriminalClass.CriminalName}\n");
            Console.WriteLine($" Name: {CriminalClass.CriminalName} Level: {CriminalClass.CriminalLevel} Crime Commited: {CriminalClass.CrimeCommited} \n");
            Console.WriteLine(" 1. Go fight \n\n 2. Run away\n\n \"B\" to run home.");
            Console.Write(" "); string doFight = Console.ReadLine().ToLower();
            Console.WriteLine();

            //Goes to fight criminal
            if (doFight == "1")
            {
                Console.Clear();
                CombatClass newCombat = new CombatClass();
                newCombat.FightOrder();
            }

            //Run away, lose money and face
            else if (doFight == "2")
            {
                Console.WriteLine();
                CombatClass newCombat = new CombatClass();
                newCombat.Run("yes");
                Thread.Sleep(2000);
            }

            //Go back home
            else if (doFight == "b")
            {
                Console.Clear();
                Console.WriteLine("\n You wimp out and head home\n\n");
                Thread.Sleep(1000);
                Console.Clear();
                atHome = true;
            }

            //Mock fail
            else
            {
                Console.Clear();
                Console.WriteLine("\n I hope you lose the next fight...");
                Thread.Sleep(3000);
                Console.Clear();
            }
        }

        //Prints available games to load
        public int PrintLoadList(string selection)
        {
            Console.Clear();
            DAL newLoadList = new DAL(DatabaseConnectionString);
            saves = newLoadList.LoadGameFile(Int32.Parse(selection));
            int count = saves.Count();
            Console.WriteLine("\n No.    Name        Level");
            Console.WriteLine("___________________________");
            foreach (SaveFile s in saves)
            {
                Console.WriteLine($" {((saves.IndexOf(s)) + 1).ToString().PadRight(5)}{s.PlayerName.PadRight(15)}{s.PlayerLevel.ToString()}\n");
            }
            return count;
        }

        //Loads chosen game file
        public void LoadGame()
        {
            Console.WriteLine("\n Select a game to load.\n\n \"B\" to go back");
            Console.Write(" "); string selection = Console.ReadLine();

            //Goes back to store menu
            if (selection == "b")
            {
                newOrLoad = true;
                atHome = false;
            }

            //Try parses a selection input
            else if (Int32.TryParse(selection, out int outNum))
            {
                //Confirms selection is a valid input number
                if (outNum > 0 && outNum <= PrintLoadList(selection))
                {
                    Console.Clear();
                    Console.WriteLine($"\n You sure this is what you want?\n");
                    Console.WriteLine("  No.   Name        Level");
                    Console.WriteLine(" _________________________");    
                    Console.WriteLine("  " + saves[(Int32.Parse(selection)) - 1] + "\n");
                    Console.WriteLine();
                    Console.WriteLine(" (Y)es or (N)o?\n Type DELETE to delete this file.");
                    //Console.WriteLine("\n Type DELETE to delete this file.");
                    Console.Write(" "); string input = Console.ReadLine().ToLower();

                    //Confirms load selection
                    if (input == "y" || input == "yes")
                    {
                        PlayerClass.LoadGame(saves[(Int32.Parse(selection))-1].SaveId, saves[(Int32.Parse(selection))-1].PlayerName, saves[(Int32.Parse(selection))-1].PlayerLevel, saves[(Int32.Parse(selection))-1].PlayerMoney, saves[(Int32.Parse(selection))-1].OwnedWeapon, saves[(Int32.Parse(selection))-1].OwnedWeaponDMG, saves[(Int32.Parse(selection))-1].OwnedArmor, saves[(Int32.Parse(selection))-1].OwnedArmorVAL);
                        Console.WriteLine("\n Game loaded successfully.  Try to be better than last time.\n\n");
                        Thread.Sleep(2000);
                        newOrLoad = false;
                        atHome = true;
                    }

                    //returns to new or load menu
                    else if (input == "n" || input == "no")
                    {
                        Console.Clear();
                        Console.WriteLine("\n Stop wasting my time.");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                        newOrLoad = true;
                        atHome = false;
                    }

                    //Deletes this save game file
                    //else if(input == "delete")
                    //{

                    //}
                    //mocks fail
                    else
                    {
                        Console.WriteLine("\n Why don't you get it together?");
                        Thread.Sleep(2000);
                        newOrLoad = true;
                        atHome = false;
                    }
                }

                //mocks fail
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n Can't count, eh?");
                    Thread.Sleep(2000);
                    Console.WriteLine();
                    newOrLoad = true;
                    atHome = false;

                }
            }

            //Do not load game
            else if( selection == "no" || selection == "n")
            {
                Console.Clear();
                Console.WriteLine("\n Forget to save your game last time?\n\n");
                Console.Clear();
                newOrLoad = true;
            }

            //Mocks fail
            else
            {
                Console.Clear();
                Console.WriteLine("\n Why don't you get it together?\n\n");
                newOrLoad = true;
                atHome = false;
            }
        }

        //Weapon confirm purchase
        public void ConfirmWeaponPurchase(string selection, string input)
        {
            //Confirms weapon purchase and subtracts cost from player money
            if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney >= weapons[Int32.Parse(selection) - 1].Weapon_Cost)
            {
                PlayerClass.SetWeapon(weapons[Int32.Parse(selection) - 1].Weapon_Type, weapons[Int32.Parse(selection) - 1].Weapon_Damage);
                Console.WriteLine("\n Thanks. Anything else you want?");
                Thread.Sleep(2000);
                atStore = true;

            }

            //Does not allow purchase due to shortage of funds
            else if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney < weapons[Int32.Parse(selection) - 1].Weapon_Cost)
            {
                Console.Clear();
                Console.WriteLine("\n Why don't you get some money and come back?  Stop wasting my time.");
                Thread.Sleep(2000);
                atStore = true;
            }

            //returns to the store menu
            else if (input == "n" || input == "no")
            {
                Console.Clear();
                Console.WriteLine("\n Stop wasting my time.");
                Thread.Sleep(2000);
                Console.WriteLine();
                atStore = true;
            }

            //mocks fail
            else
            {
                Console.Clear();
                Console.WriteLine("\n Why don't you get it together?");
                Thread.Sleep(2000);
                atStore = true;
            }
        }

        //Armor confirm purchase
        public void ConfirmArmorPurchase(string selection, string input)
        {
            if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney >= armor[Int32.Parse(selection) - 1].Armor_Cost)
            {
                PlayerClass.SetArmor(armor[Int32.Parse(selection) - 1].Armor_Type, armor[Int32.Parse(selection) - 1].Armor_Value);
                Console.WriteLine("\n Thanks. Anything else you want?");
                Thread.Sleep(2000);
                atStore = true;

            }
            else if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney < armor[Int32.Parse(selection) - 1].Armor_Cost)
            {
                Console.Clear();
                Console.WriteLine("\n Why don't you get some money and come back?  Stop wasting my time.");
                Thread.Sleep(2000);
                atStore = true;
            }
            else if (input == "n" || input == "no")
            {
                Console.Clear();
                Console.WriteLine("\n Stop wasting my time.\n");
                Thread.Sleep(2000);
                atStore = true;
                atHome = false;

            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Why don't you get it together?");
                Thread.Sleep(2000);
                Console.Clear();
                atStore = true;
            }
        }
    }
}

