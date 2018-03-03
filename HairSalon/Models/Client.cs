using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _email;
        private int _stylistId;

        public Client(string name, string email, int stylistId, int id = 0)
        {
            _name = name;
            _email = email;
            _stylistId = stylistId;
            _id = id;
        }
        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string name)
        {
          _name = name;
        }
        public string GetEmail()
        {
            return _email;
        }
        public void SetEmail(string email)
        {
          _email = email;
        }
        public int GetStylistId()
        {
            return _stylistId;
        }
        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.GetId()) == newClient.GetId();
                bool nameEquality = (this.GetName()) == newClient.GetName();
                bool emailEquality = (this.GetEmail()) == newClient.GetEmail();

                return (idEquality && nameEquality && emailEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }
        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>();
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM clients ORDER BY name;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                string clientEmail = rdr.GetString(2);
                // int clientStylist = rdr.GetInt32(3);

                Client newClient = new Client(clientName, clientEmail, clientId);
                allClients.Add(newClient);
            }
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
            return allClients;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, email, stylistId) VALUES (@name, @email, @stylist_id);";

            MySqlParameter clientName = new MySqlParameter();
            clientName.ParameterName = "@name";
            clientName.Value = this._name;
            cmd.Parameters.Add(clientName);

            MySqlParameter clientEmail = new MySqlParameter();
            clientEmail.ParameterName = "@email";
            clientEmail.Value = this._email;
            cmd.Parameters.Add(clientEmail);

            MySqlParameter clientStylist = new MySqlParameter();
            clientStylist.ParameterName = "@stylist_id";
            clientStylist.Value = this._stylistId;
            cmd.Parameters.Add(clientStylist);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
        }
        public static Client Find(int searchId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@searchId";
            clientId.Value = searchId;
            cmd.Parameters.Add(clientId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int foundId = 0;
            string foundName = "";
            string foundEmail = "";
            int foundStylistId = 0;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundEmail = rdr.GetString(2);
                foundStylistId = rdr.GetInt32(3);
            }
            Client foundClient = new Client(foundName, foundEmail, foundStylistId, foundId);
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
            return foundClient;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@thisId";
            clientId.Value = this._id;
            cmd.Parameters.Add(clientId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
        }
    }
}
