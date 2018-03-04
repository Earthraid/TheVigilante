using System;
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
        private string originPath = @"C:\Users\jchristian\Desktop\TheVigilante\etc\OriginStory.txt";
        const string DatabaseConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TheVigilante;Integrated Security=True";
        private int count;
        private string selection;
        private List<Weapon> weapons;
        private List<Armor> armor;
        private bool atHome = true;
        private bool atStore = true;
        private bool newOrLoad = true;
        private bool firstMenu = true;
        private string input;

        public void RunInterface()
        {
            while (newOrLoad)
            {
                Console.WriteLine();
                Console.WriteLine(" 1. New Game\n\n 2. Load Game\n\n");
                selection = Console.ReadLine();
                Console.WriteLine();

                //New game
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
                //Load Game
                else if (selection == "2")
                {
                    //Print list of available games to load based

                    //DAL loadGame = new DAL();
                    //LoadGame()
                    newOrLoad = false;

                }
                //Mock fail
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


        //Writes origin story if not loaded
        public void OriginWriter()
        {
            using (StreamReader sr = new StreamReader(originPath))
            {
                while (!sr.EndOfStream)
                {
                    Console.WriteLine(sr.ReadLine());
                    // Thread.Sleep(1000);
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
                PlayerClass.PlayerHitPoints = PlayerClass.MaxHitPoints;
                Console.WriteLine();
                Console.WriteLine(" You're home.  You hate it here.\n");
                Console.WriteLine(" You're feelng rested and ready to move. \n \n What would you like to do?\n");
                Console.WriteLine($" Money: {PlayerClass.PlayerMoney.ToString("C")}\n");
                Console.WriteLine(" 1. \"Store\"\n 2. Go fight\n 3. Save Game\n 4. Quit Game\n");
                string homeSelection = Console.ReadLine();
                Console.WriteLine();
                //Got to store
                if (homeSelection == "1")
                {
                    atStore = true;
                    while (atStore)
                    {
                        Console.Clear();
                        Console.WriteLine(" Which would you like to purchase? \n\n 1. Weapons \n\n 2. Armor\n\n \"B\" to go back home.");
                        selection = Console.ReadLine().ToLower();
                        Console.WriteLine();
                        //Prints weapons to purchase
                        if (selection == "1")
                        {
                            PrintWeapons();
                            atStore = false;
                            Console.WriteLine(" Choose something to purchase or select \"B\" to go back.");
                            selection = Console.ReadLine().ToLower();
                            if (selection == "b")
                            {
                                atStore = true;
                            }
                            if (Int32.TryParse(selection, out int outNum))
                            {
                                if (outNum > 0 && outNum <= weapons.Count)
                                {
                                    selection = weapons[(Int32.Parse(selection) - 1)].Weapon_ID.ToString();
                                    Console.WriteLine();
                                    Console.Clear();
                                    Console.WriteLine($" You sure this is what you want?\n");
                                    Console.WriteLine("  No.   Type    Damage   Cost");
                                    Console.WriteLine(" _____________________________");
                                    Console.WriteLine("  " + weapons[Int32.Parse(selection) - 1] + "\n");
                                    Console.WriteLine();
                                    Console.WriteLine(" (Y)es or (N)o?");
                                    string input = Console.ReadLine().ToLower();

                                    //Need to figure out money issue
                                    if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney >= weapons[Int32.Parse(selection) - 1].Weapon_Cost)
                                    {
                                        PlayerClass.SetWeapon(weapons[Int32.Parse(selection) - 1].Weapon_Type, weapons[Int32.Parse(selection) - 1].Weapon_Damage);
                                        Console.WriteLine();
                                        Console.WriteLine(" Thanks. Anything else you want?");
                                        Thread.Sleep(2000);
                                        atStore = true;

                                    }
                                    else if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney < weapons[Int32.Parse(selection) - 1].Weapon_Cost)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(" Why don't you get some money and come back?  Stop wasting my time.");
                                        Thread.Sleep(2000);
                                        atStore = true;
                                    }
                                    else if (input == "n" || input == "no")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(" Stop wasting my time.");
                                        Thread.Sleep(2000);
                                        Console.WriteLine();
                                        atStore = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Why don't you get it together?");
                                        atStore = true;
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(" Can't count, eh?");
                                    Thread.Sleep(2000);
                                    Console.WriteLine();
                                    atStore = true;

                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(" Please push a button that makes sense. Idiot...");
                                Console.WriteLine();
                                Thread.Sleep(2000);
                                atStore = true;
                            }
                        }
                        //Prints armor to purchase
                        else if (selection == "2")
                        {
                            PrintArmor();
                            atStore = false;
                            Console.WriteLine(" Choose something to purchase or select \"B\" to go back.");
                            selection = Console.ReadLine().ToLower();
                            if (selection == "b")
                            {
                                atStore = true;
                            }
                            if (Int32.TryParse(selection, out int outNum))
                            {
                                if (outNum > 0 && outNum <= armor.Count)
                                {
                                    selection = armor[(Int32.Parse(selection) - 1)].Armor_ID.ToString();
                                    Console.WriteLine();
                                    Console.Clear();
                                    Console.WriteLine($" You sure this is what you want?\n");
                                    Console.WriteLine(" No.     Type                Damage    Cost");
                                    Console.WriteLine("____________________________________________");
                                    Console.WriteLine(" " + armor[Int32.Parse(selection) - 1] + "\n");
                                    Console.WriteLine();
                                    Console.WriteLine(" (Y)es or (N)o?");
                                    string input = Console.ReadLine().ToLower();

                                    //Need to figure out money issue
                                    if ((input == "y" || input == "yes") && PlayerClass.PlayerMoney >= armor[Int32.Parse(selection) - 1].Armor_Cost)
                                    {
                                        PlayerClass.SetArmor(armor[Int32.Parse(selection) - 1].Armor_Type, armor[Int32.Parse(selection) - 1].Armor_Value);
                                        Console.WriteLine();
                                        Console.WriteLine(" Thanks. Anything else you want?");
                                        Thread.Sleep(2000);
                                        atStore = true;

                                    }
                                    else if (input == "n" || input == "no")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(" Stop wasting my time.");
                                        Thread.Sleep(2000);
                                        Console.WriteLine();
                                        atStore = true;
                                        atHome = false;

                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(" Can't count, eh?");
                                    Thread.Sleep(2000);
                                    Console.WriteLine();
                                    atStore = true;

                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(" Please push a button that makes sense. Idiot...");
                                Console.WriteLine();
                                Thread.Sleep(2000);
                                atStore = true;
                            }
                        }
                        //Goes back to previuos menu
                        else if (selection == "b")
                        {
                            Console.Clear();
                            Console.WriteLine(" Headed back home.  You hate that dump.\n");

                            atHome = true;
                            atStore = false;
                        }
                        //mocks fail
                        else
                        {
                            Console.Clear();
                            if (PlayerClass.PlayerMoney != 0)
                            {
                                Console.WriteLine(" The directions weren't clear enough?  You drop some money because you're an idiot.\n");
                                PlayerClass.SpendPlayerMoney(1);
                                Console.WriteLine(" You somehow got lost and ended up back home... Wow...\n");
                                atHome = true;
                                atStore = false;
                            }
                            else
                            {
                                Console.WriteLine(" Not super good at directions, eh?  This vigilante thing is going to work out great...\n");
                                Console.WriteLine(" You somehow got lost and ended up back home... Wow...\n");
                                atHome = true;
                                atStore = false;
                            }
                        }
                    }
                }
                //Fight a criminal
                else if (homeSelection == "2")
                {
                    CriminalClass.CreateCriminal();
                    Console.WriteLine($"\n You decide to stroll the neighborhoods where you know criminals hide.\n\n You find {CriminalClass.CriminalName}\n");
                    Console.WriteLine($" Name: {CriminalClass.CriminalName} Level: {CriminalClass.CriminalLevel} Crime Commited: {CriminalClass.CrimeCommited} \n");
                    Console.WriteLine(" 1. Go fight \n 2. Run away\n");
                    Console.Write(" "); string doFight = Console.ReadLine();
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
                        newCombat.Run("y");
                        Thread.Sleep(2000);
                    }
                    else 

                }
                //Save your game
                else if (homeSelection == "3")
                {
                    //SaveGame();
                }
                //Quit game
                else if (homeSelection == "4")
                {
                    Environment.Exit(0);
                }
                //Mock your fail
                else
                {
                    Console.WriteLine(" Make a choice available on the screen.  You're probably not going to make it long at this rate.");
                }
            }


        }

        //Prints weapon list
        public void PrintWeapons()
        {
            Console.WriteLine();
            DAL newWeaponList = new DAL(DatabaseConnectionString);
            weapons = newWeaponList.WeaponList(Int32.Parse(selection));
            count = weapons.Count();
            Console.WriteLine();
            Console.Clear();
            Console.WriteLine(" No.     Type      Damage      Cost");
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
        public void PrintArmor()
        {
            Console.WriteLine();
            DAL newArmorList = new DAL(DatabaseConnectionString);
            armor = newArmorList.ArmorList(Int32.Parse(selection));
            count = armor.Count();
            Console.WriteLine();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" No.     Type             Damage      Cost");
            Console.WriteLine("___________________________________________");

            foreach (Armor a in armor)
            {
                Console.WriteLine($" {((armor.IndexOf(a)) + 1).ToString().PadRight(5)}{a.Armor_Type.PadRight(20)}   {a.Armor_Value.ToString().PadRight(5)} {a.Armor_Cost.ToString("C")}");
            }
            Console.WriteLine();
            Console.WriteLine("Money: " + PlayerClass.PlayerMoney.ToString("C"));
            Console.WriteLine();
        }
    }
}

