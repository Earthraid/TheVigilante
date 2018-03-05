using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TheVigilante.Classes
{
    public class CombatClass
    {
        private bool run = false;
        private string input;
        private bool stillFighting = true;
        private int playerAttack;
        private int criminalAttack;

        static Random rnd = new Random();

        //Determines order of attack
        public void FightOrder()
        {
            stillFighting = true;

            //Decides fight order
            int fightOrder = rnd.Next(0, 1);

            //Player hits first
            if (fightOrder == 0)
            {
                Console.WriteLine(" You got the jump on this scum-bag.\n");
                while (stillFighting)
                {
                    Console.Clear();
                    Console.WriteLine($" Player Health: {PlayerClass.PlayerHitPoints}    Criminal Health: {CriminalClass.CriminalHitPoints} \n");

                    PlayerAttack();
                    Console.WriteLine($" You hit {CriminalClass.CriminalName} for {playerAttack}.\n");
                    CheckHitPonts(CriminalClass.CriminalHitPoints);
                    Thread.Sleep(500);

                    CriminalAttack();
                    Console.WriteLine($" Oof. You got hit for {criminalAttack}.\n");
                    CheckHitPonts(PlayerClass.PlayerHitPoints);
                    Thread.Sleep(500);

                    Console.Write(" Do you want to wimp out and run away? (Y)es or (N)o?: ");
                    input = Console.ReadLine().ToLower();
                    Run(input);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            //Criminal hits first
            else
            {
                Console.WriteLine(" This jack ass saw you coming a mile away.\n");
                while (stillFighting)
                {
                    Console.Clear();
                    Console.WriteLine($" Player Health: {PlayerClass.PlayerHitPoints}    Criminal Health: {CriminalClass.CriminalHitPoints} \n");
                    CriminalAttack();
                    Console.WriteLine($" Oof. You got hit for {criminalAttack}.\n");
                    CheckHitPonts(PlayerClass.PlayerHitPoints);
                    Thread.Sleep(1000);

                    PlayerAttack();
                    Console.WriteLine($" You hit {CriminalClass.CriminalName} for {playerAttack}.\n");
                    CheckHitPonts(CriminalClass.CriminalHitPoints);
                    Thread.Sleep(1000);

                    Console.Write(" Do you want to wimp out and run away? (Y)es or (N)o?: ");
                    input = Console.ReadLine().ToLower();
                    Console.WriteLine();
                    Run(input);
                    CombatEndFight();
                    Console.WriteLine();
                }
            }
        }

        //Calculates and applies player's attack value
        public int PlayerAttack()
        {
            playerAttack = rnd.Next(PlayerClass.PlayerLevel + 5, PlayerClass.PlayerLevel + 10) + PlayerClass.OwnedWeaponDamage;
            CriminalClass.CriminalHitPoints -= playerAttack;
            CheckHitPonts(CriminalClass.CriminalHitPoints);
            return playerAttack;
        }

        //Calculates and applies criminal's attack value
        public int CriminalAttack()
        {
            criminalAttack = rnd.Next(CriminalClass.CriminalLevel + 5, CriminalClass.CriminalLevel + 10) - PlayerClass.OwnedArmorValue;
            PlayerClass.PlayerHitPoints -= criminalAttack;
            CheckHitPonts(PlayerClass.PlayerHitPoints);
            return criminalAttack;
        }

        //Gives consequences of the fight ending, however that may be
        public void CombatEndFight()
        {
            if (PlayerClass.PlayerHitPoints <= 0)
            {
                Console.WriteLine();
                Console.WriteLine(" You got knocked out! Hey, at least they didn't kill you.\n");
                Console.WriteLine($" You've lost {(PlayerClass.PlayerMoney / 10).ToString("C")}\n");
                Thread.Sleep(3000);
                Console.Clear();
                PlayerClass.EndFight(false, PlayerClass.PlayerMoney / 10);
                CriminalClass.CriminalMaxHitPoints();
                stillFighting = false;
            }
            else if (CriminalClass.CriminalHitPoints <= 0)
            {
                Console.WriteLine();
                Console.WriteLine(" You've won. Justice.\n");
                Console.WriteLine($" You've gained {(CriminalClass.CriminalMoney).ToString("C")}\n");
                Console.WriteLine($" You've gained {CriminalClass.CriminalExperience} XP.\n");
                Thread.Sleep(3000);
                Console.Clear();
                PlayerClass.EndFight(true, CriminalClass.CriminalMoney);
                CriminalClass.CriminalMaxHitPoints();
                stillFighting = false;
            }
        }

        //Gives player the opprtunity to run from fight at a cost of 10% of $$
        public void Run(string input)
        {
            if (input == "y" || input == "yes")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(" Just another coward posing as a hero.\n");
                Console.WriteLine($" You've lost {(PlayerClass.PlayerMoney / 10).ToString("C")}\n");
                Console.WriteLine(" Cry in your pillow at home...\n");
                Thread.Sleep(3000);
                PlayerClass.EndFight(false, PlayerClass.PlayerMoney / 10);
                stillFighting = false;
            }
            else if (input == "n" || input == "no")
            {
                Console.WriteLine();
                Console.WriteLine(" You suck it up and keep fighting.\n");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(" Not too good at directions, huh?\n");
                Console.WriteLine(" Hopefully you survive this round.\n");
                Console.WriteLine();
            }
        }

        //Checks hitpoints to end fight if one's hitpoints falls to 0 or below.
        public void CheckHitPonts(int hitPoints)
        {
            if (hitPoints <= 0)
            {
                CombatEndFight();
            }
        }

    }
}
