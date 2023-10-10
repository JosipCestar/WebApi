using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebAppii.Models;
using WebAppii.Repository.Common;

public class HoodieRepository : HoodieRepositoryCommon
{
    private NpgsqlConnection connection;
    private string tableName = "\"Hoodie\"";

    public HoodieRepository()
    {
    }

    public async Task<string> DeleteHoodie(Guid id)
    {
        try
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;"))
            {
                await connection.OpenAsync();
                string commandText = $"DELETE FROM {tableName} WHERE \"Id\" = @id";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                   
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        return "Deleted";
                    }
                    else
                    {
                        return "Hoodie not found";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    public async Task<List<Hoodie>> GetAllHoodies()
    {
        try
        {
            List<Hoodie> hoodies = new List<Hoodie>();
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;"))
            {
                await connection.OpenAsync();
                string commandText = $"SELECT * FROM {tableName}";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Hoodie hoodie = ReadHoodie(reader);
                            hoodies.Add(hoodie);
                        }
                        return hoodies;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Hoodie> GetHoodieById(Guid id)
    {
        try
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;"))
            {
                await connection.OpenAsync();
                string commandText = $"SELECT * FROM {tableName} WHERE \"Id\" = @id";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Hoodie hoodie = ReadHoodie(reader);
                            return hoodie;
                        }
                    }
                }
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string> PostHoodie(Hoodie hoodie)
    {
        try
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;"))
            {
                await connection.OpenAsync();
                string commandText = $"INSERT INTO {tableName} (\"Id\",\"Name\", \"Size\", \"Style\") VALUES (@id,@name, @size, @style)";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    Guid id = Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", hoodie.Name);
                    cmd.Parameters.AddWithValue("@size", hoodie.Size);
                    cmd.Parameters.AddWithValue("@style", hoodie.Style);

                    await cmd.ExecuteNonQueryAsync();
                    return "Created";
                }
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    public async Task<string> UpdateHoodie(Hoodie hoodie, Guid id)
    {
        try
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;"))
            {
                await connection.OpenAsync();
                var commandText = $"UPDATE {tableName} SET \"Name\" = @name, \"Size\" = @size, \"Style\" = @style WHERE \"Id\" = @id";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", hoodie.Id);
                    cmd.Parameters.AddWithValue("@name", hoodie.Name);
                    cmd.Parameters.AddWithValue("@size", hoodie.Size);
                    cmd.Parameters.AddWithValue("@style", hoodie.Style);

                    cmd.ExecuteNonQuery();
                    return "Updated";
                }
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    Hoodie ReadHoodie(NpgsqlDataReader reader)
    {
        Guid? id = reader["Id"] as Guid?;
        string name = reader["Name"].ToString();
        string size = reader["Size"].ToString();
        string style = reader["Style"].ToString();

        Hoodie hoodie = new Hoodie(id.Value, name, size, style);
        return hoodie;
    }
}
