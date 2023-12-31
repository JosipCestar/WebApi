﻿using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebAppii.Models;
using WebAppii.Repository.Common;
using WebApiPractice.Common;
using System.Text;
using System.ComponentModel;
using System.Web.Http.ModelBinding;
using Repository;

public class HoodieRepository : IHoodieRepository
{
    private NpgsqlConnection ConnectionString;
    
    private string tableName = "\"Hoodie\"";

    public HoodieRepository()
    {
        ConnectionString = new BaseConnection().connectionString;
    }

    public async Task<string> DeleteHoodie(Guid id)
    {
        try
        {
            using (var connection = new NpgsqlConnection(ConnectionString.ConnectionString))
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

    public async Task<List<Hoodie>> GetAllHoodies(Paging paging, Sorting sorting, Filtering filtering)
    {
        try
        {
            StringBuilder querryBuilder = new StringBuilder();
            List<Hoodie> hoodies = new List<Hoodie>();

            using (var connection = new NpgsqlConnection(ConnectionString.ConnectionString))
            {

                await connection.OpenAsync();
                querryBuilder.Append($"SELECT * FROM {tableName} ");
                NpgsqlCommand cmd = new NpgsqlCommand(querryBuilder.ToString(),connection);
                if (filtering != null)
                {
                    querryBuilder.Append("WHERE ");
                    if (filtering.QuerryName != null)
                    {
                        querryBuilder.Append($"\"Name\" LIKE '%{filtering.QuerryName}%' ");
                    }
                    if (filtering.QuerrySize != null)
                    {
                        if (filtering.QuerryName != null)
                        {
                            querryBuilder.Append("AND ");
                        }
                        querryBuilder.Append($"\"Size\" LIKE '%{filtering.QuerrySize}%' ");
                    }
                    if (filtering.QuerryStyle != null)
                    {
                        if (filtering.QuerryName != null || filtering.QuerrySize != null)
                        {
                            querryBuilder.Append("AND ");
                        }
                        querryBuilder.Append($"\"Style\" LIKE '%{filtering.QuerryStyle}%' ");
                    }

                    int offset= (paging.PageNumber - 1) * paging.PageSize;

                    if(paging.PageNumber!=0 && paging.PageSize!=0)
                    {   
                        if (sorting.SortBy != null)
                        {
                            querryBuilder.Append($" ORDER BY \"{sorting.SortBy}\" {sorting.SortOrder} ");
                        }
                        else
                        {
                            if (sorting.SortOrder != null)
                            {
                                querryBuilder.Append($" ORDER BY \"Id\" {sorting.SortOrder} ");
                            }
                            else
                            {
                                querryBuilder.Append(" ORDER BY \"Id\" ASC ");
                            }   
                           
                        }
                        querryBuilder.Append(" ORDER BY \"Id\" ASC ");
                        querryBuilder.Append($" OFFSET {offset} ROWS FETCH NEXT {paging.PageSize} ROWS ONLY ");
                    }

                    using (cmd)
                    {
                        cmd.CommandText = querryBuilder.ToString();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Hoodie hoodie = ReadHoodie(reader);
                                hoodies.Add(hoodie);
                            }
                        }
                        connection.Close();
                    }

                }
                return hoodies;

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
            using (var connection = new NpgsqlConnection(ConnectionString.ConnectionString))
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
            using (var connection = new NpgsqlConnection(ConnectionString.ConnectionString))
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
            using (var connection = new NpgsqlConnection(ConnectionString.ConnectionString))
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
