using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _email;

    public Client(string name, string email, int id = 0)
    {
      _name = name;
      _email = email;
      _id = id;
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
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetName() == newClient.GetName();
        bool emailEquality = this.GetEmail() == newClient.GetEmail();

        return (idEquality && nameEquality && emailEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
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

    public static List<Client> GetAll()
    {
        List<Client> allClients = new List<Client>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          string email = rdr.GetString(2);

          Client newClient = new Client(clientName, email, clientId);
          allClients.Add(newClient);
        }
        conn.Close();
        if (conn != null)
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
      cmd.CommandText = @"INSERT INTO clients (name, email) VALUES (@name, @email);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@email";
      email.Value = this._email;
      cmd.Parameters.Add(email);


      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      string email = "";

      while (rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        email = rdr.GetString(2);

      }
      Client newClient = new Client(clientName, email, clientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newClient;
    }
    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;
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
      cmd.CommandText = @"INSERT INTO specialty (client_id) VALUES (@clientId);";

      MySqlParameter client_id = new MySqlParameter();
      client_id.ParameterName = "@clientId";
      client_id.Value = _id;
      cmd.Parameters.Add(client_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      List<Stylist> newStylistList = new List<Stylist>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM clients
                JOIN schedule ON (clients.id = schedule.client_id)
                JOIN stylists ON (schedule.stylist_id = stylists.id)
                WHERE clients.id = @ClientId;";

      MySqlParameter client_id = new MySqlParameter();
      client_id.ParameterName = "@ClientId";
      client_id.Value = _id;
      cmd.Parameters.Add(client_id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        string name = rdr.GetString(1);
        Stylist newStylist = new Stylist(name);
        newStylistList.Add(newStylist);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylistList;
    }

    public void DeleteOne()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
