using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialty
    {
        private string _specialty;
        private int _id;

        public Specialty(string specialty)
        {
            _specialty = specialty;
        }

        public string GetSpecialty()
        {
            return _specialty;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialty VALUES (@specialty);";

            MySqlParameter specialty = new MySqlParameter("@specialty", _specialty);
            cmd.Parameters.Add(specialty);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialty_stylist (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);";
            MySqlParameter stylist_id = new MySqlParameter("@StylistId", newStylist.GetId());
            MySqlParameter specialty_id = new MySqlParameter("@SpecialtyId", _id);
            cmd.Parameters.Add(stylist_id);
            cmd.Parameters.Add(specialty_id);
            cmd.ExecuteNonQuery();
            conn.Dispose();
        }

        public void UpdateSpecialty(string newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialty SET specialty = @newSpecialty WHERE id = @id;";

            MySqlParameter specialty = new MySqlParameter("@newSpecialty", newSpecialty);
            MySqlParameter id = new MySqlParameter("@id", _id);
            cmd.Parameters.Add(specialty);
            cmd.Parameters.Add(id);
            cmd.ExecuteNonQuery();
            conn.Dispose();
        }

        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialty WHERE id=@id;";
            cmd.Parameters.Add(new MySqlParameter("@id", id));

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            string tempSpecialty = "";
            int tempId = 0;

            while (rdr.Read())
            {
                tempId = rdr.GetInt32(0);
                tempSpecialty = rdr.GetString(1);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            Specialty thisSpecialty = new Specialty(tempSpecialty);
            thisSpecialty.SetId(tempId);
            return thisSpecialty;
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> mySpecialties = new List<Specialty>();

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialty;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string specialty = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(specialty);
                mySpecialties.Add(newSpecialty);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return mySpecialties;
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylist_id FROM specialty_stylist WHERE specialty_id = @specialty_id;";

            MySqlParameter specialty = new MySqlParameter("@specialty_id", _id);
            cmd.Parameters.Add(specialty);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<int> stylistIds = new List<int>();

            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(1);
                stylistIds.Add(stylistId);
            }
            rdr.Dispose();

            List<Stylist> stylists = new List<Stylist>{};
            foreach (int stylistId in stylistIds)
            {
                var stylistQuery = conn.CreateCommand() as MySqlCommand;
                stylistQuery.CommandText = @"SELECT * FROM stylists WHERE id = @StylistId;";

                MySqlParameter stylistIdPara = new MySqlParameter("@StylistId", stylistId);
                stylistQuery.Parameters.Add(stylistIdPara);

                var rdr2 = stylistQuery.ExecuteReader() as MySqlDataReader;
                while (rdr2.Read())
                {
                    int thisStylistId = rdr2.GetInt32(0);
                    string stylistName = rdr2.GetString(1);

                    Stylist foundStylist = new Stylist(stylistName);
                    stylists.Add(foundStylist);
                }
                rdr2.Dispose();
            }
            conn.Dispose();
            return stylists;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialty;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialty WHERE id=@id; @DELETE FROM specialty_stylist WHERE id=@id;";
            MySqlParameter tempId = new MySqlParameter("@id", _id);
            cmd.Parameters.Add(tempId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                return (newSpecialty.GetSpecialty() == _specialty);
            }
        }

        public override int GetHashCode()
        {
            return this.GetSpecialty().GetHashCode();
        }

    }
}
