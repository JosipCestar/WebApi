using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;

using Npgsql;
using WebAppii.Models;
using WebAppii.Service.Common;
using WebAppii.Repository.Common;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace WebAppii.Repository

{
    public class HoodieRepository : HoodieRepositoryCommon
    {
        private NpgsqlConnection connection;
        private string tableName = "\"Hoodie\"";

        public HoodieRepository()
        {
        }
        
        public string DeleteHoodie(Guid id)
        {
            try
            {

                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"DELETE FROM {tableName} WHERE id = @id";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return ("Deleted");
                    }
                    else
                    {
                        return ("Hoodie not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }

        public List<Hoodie> GetAllHoodies() {
            try
            {
                List<Hoodie> hoodies = new List<Hoodie>();
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"SELECT * FROM {tableName}";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Hoodie hoodie = ReadHoodie(reader);
                            hoodies.Add(hoodie);
                        }
                        return hoodies;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            


        }
        Hoodie ReadHoodie(NpgsqlDataReader reader)
        {
            Guid? id = reader["id"] as Guid?;
            string name = reader["name"].ToString();
            string size = reader["size"].ToString();
            string style = reader["style"].ToString();


            Hoodie hoodie = new Hoodie(id.Value, name, size, style);
            return hoodie;
        }
        public Hoodie GetHoodieById(Guid id)
        {
            try
            {
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"SELECT * FROM {tableName} WHERE \"Id\" = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hoodie hoodie = ReadHoodie(reader);
                            return hoodie;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string PostHoodie(Hoodie hoodie)
        {
            try
            {
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"INSERT INTO {tableName} (\"Id\",\"Name\", \"Size\", \"Style\") VALUES (@id,@name, @size, @style)";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    Guid id = Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", hoodie.Name);
                    cmd.Parameters.AddWithValue("@size", hoodie.Size);
                    cmd.Parameters.AddWithValue("@style", hoodie.Style);

                    cmd.ExecuteNonQuery();
                    return ("Created");
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }
        }

        public string UpdateHoodie(Hoodie hoodie)
        {
            try
            {



                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                var commandText = $"UPDATE {tableName} SET \"Name\" = @name, \"Size\" = @size, \"Style\" = @style WHERE \"id\" = @id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", hoodie.Id);
                    cmd.Parameters.AddWithValue("@name", hoodie.Name);
                    cmd.Parameters.AddWithValue("@size", hoodie.Size);
                    cmd.Parameters.AddWithValue("@style", hoodie.Style);

                    cmd.ExecuteNonQuery();
                    return ("Updated");
                }
            }
            catch (Exception ex)
            {
                return ("Error");
            }

        }
    }
}
