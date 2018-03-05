using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TheVigilante.Classes;
using System.Threading;

namespace TheVigilante
{
    class DAL
    {
        private const string SQL_AvailableWeapons = "SELECT * FROM weapon";
        private const string SQL_AvailableArmor = "SELECT * FROM armor";
        private const string SQL_LoadGameFiles = "SELECT * FROM game_files";
        private const string SQL_SaveGameFile = "INSERT INTO game_files (player_name, player_level, player_money, owned_weapon_type, owned_weapon_damage, owned_armor_type, owned_armor_value) VALUES (@player_name, @player_level, @player_money, @owned_weapon_type, @owned_weapon_damage, @owned_armor_type, @owned_armor_value); SELECT CAST(SCOPE_IDENTITY() as int)";
        private const string SQL_ReSaveGameFIle = "UPDATE game_files SET player_name = @player_name, player_level = @player_level, player_money = @player_money, owned_weapon_type = @owned_weapon_type, owned_weapon_damage = @owned_weapon_damage, owned_armor_type = @owned_armor_type, owned_armor_value = @owned_armor_value WHERE save_id = @save_id";
        private string connectionString;

        //Constructor
        public DAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        //Method to create and store a List<Weapon>
        public List<Weapon> WeaponList(int weaponSelection)
        {
            List<Weapon> allWeapons = new List<Weapon>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open connection
                    conn.Open();
                    //Calls private instance variable from above to input SQL command
                    SqlCommand cmd = new SqlCommand(SQL_AvailableWeapons, conn);

                    //Reads SQL tables
                    SqlDataReader reader = cmd.ExecuteReader();
                    //While reader is still reading populates List with things of type Weapon
                    while (reader.Read())
                    {
                        Weapon w = new Weapon();
                        w.Weapon_ID = Convert.ToInt32(reader["weapon_id"]);
                        w.Weapon_Type = Convert.ToString(reader["weapon_type"]);
                        w.Weapon_Damage = Convert.ToInt32(reader["weapon_damage"]);
                        w.Weapon_Cost = Convert.ToInt32(reader["weapon_cost"]);


                        allWeapons.Add(w);
                    }
                }
            }
            catch { throw; }
            return allWeapons;
        }

        public List<Armor> ArmorList(int armornSelection)
        {
            List<Armor> allArmor = new List<Armor>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open connection
                    conn.Open();
                    //Calls private instance variable from above to input SQL command
                    SqlCommand cmd = new SqlCommand(SQL_AvailableArmor, conn);

                    //Reads SQL tables
                    SqlDataReader reader = cmd.ExecuteReader();
                    //While reader is still reading populates List with things of type Weapon
                    while (reader.Read())
                    {
                        Armor a = new Armor();
                        a.Armor_ID = Convert.ToInt32(reader["armor_id"]);
                        a.Armor_Type = Convert.ToString(reader["armor_type"]);
                        a.Armor_Value = Convert.ToInt32(reader["armor_value"]);
                        a.Armor_Cost = Convert.ToInt32(reader["armor_cost"]);


                        allArmor.Add(a);
                    }
                }
            }
            catch { throw; }
            return allArmor;
        }

        //To do -- Write a list to print load games
        public List<SaveFile> LoadGameFile(int gameFileSelection)
        {
            List<SaveFile> allSaves = new List<SaveFile>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open connection
                    conn.Open();

                    //Calls private instance variable from above to input SQL command
                    SqlCommand cmd = new SqlCommand(SQL_LoadGameFiles, conn);

                    //Reads SQL tables
                    SqlDataReader reader = cmd.ExecuteReader();

                    //While reader is still reading populates List with things of type Weapon
                    while (reader.Read())
                    {
                        SaveFile s = new SaveFile();

                        s.SaveId = Convert.ToInt32(reader["save_id"]);
                        s.PlayerName = Convert.ToString(reader["player_name"]);
                        s.PlayerLevel = Convert.ToInt32(reader["player_level"]);
                        s.PlayerMoney = Convert.ToInt32(reader["player_money"]);
                        s.OwnedWeapon = Convert.ToString(reader["owned_weapon_type"]);
                        s.OwnedWeaponDMG = Convert.ToInt32(reader["owned_weapon_damage"]);
                        s.OwnedArmor = Convert.ToString(reader["owned_armor_type"]);
                        s.OwnedArmorVAL = Convert.ToInt32(reader["owned_armor_value"]);

                        allSaves.Add(s);
                    }
                }
            }
            catch { throw; }
            return allSaves;
        }

        public void InsertSaveFile()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open connection
                    conn.Open();
                    //Calls private instance variable from above to input SQL command
                    SqlCommand cmd = new SqlCommand(SQL_SaveGameFile, conn);

                    cmd.Parameters.AddWithValue("@player_name", PlayerClass.PlayerName);
                    cmd.Parameters.AddWithValue("@player_level", PlayerClass.PlayerLevel);
                    cmd.Parameters.AddWithValue("@player_money", PlayerClass.PlayerMoney);
                    cmd.Parameters.AddWithValue("@owned_weapon_type", PlayerClass.OwnedWeaponType);
                    cmd.Parameters.AddWithValue("@owned_weapon_damage", PlayerClass.OwnedWeaponDamage);
                    cmd.Parameters.AddWithValue("@owned_armor_type", PlayerClass.OwnedArmorType);
                    cmd.Parameters.AddWithValue("@owned_armor_value", PlayerClass.OwnedArmorValue);

                    int saveID = (int)cmd.ExecuteScalar();
                    PlayerClass.CheckSave(saveID);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("\n Sorry, the game failed to save.  It was probably your fault.");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        public void UpdateSaveFileList(int gameFileSelection)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Open connection
                    conn.Open();
                    //Calls private instance variable from above to input SQL command
                    SqlCommand cmd = new SqlCommand(SQL_ReSaveGameFIle, conn);

                    cmd.Parameters.AddWithValue("@save_id", PlayerClass.SaveID);
                    cmd.Parameters.AddWithValue("@player_name", PlayerClass.PlayerName);
                    cmd.Parameters.AddWithValue("@player_level", PlayerClass.PlayerLevel);
                    cmd.Parameters.AddWithValue("@player_money", PlayerClass.PlayerMoney);
                    cmd.Parameters.AddWithValue("@owned_weapon_type", PlayerClass.OwnedWeaponType);
                    cmd.Parameters.AddWithValue("@owned_weapon_damage", PlayerClass.OwnedWeaponDamage);
                    cmd.Parameters.AddWithValue("@owned_armor_type", PlayerClass.OwnedArmorType);
                    cmd.Parameters.AddWithValue("@owned_armor_value", PlayerClass.OwnedArmorValue);

                    cmd.ExecuteNonQuery();
                    PlayerClass.CheckSave(PlayerClass.SaveID);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("\n Sorry, the game failed to save.  It was probably your fault.");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }


    }
}
