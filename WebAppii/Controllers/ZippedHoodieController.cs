using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using WebAppii.Models; 

namespace WebAppii.Controllers
{
    public class ZippedHoodieController : ApiController
    {
        private NpgsqlConnection connection;
        private string tableName = "\"ZippedHoodie\"";

        [HttpPost]
        [Route("api/Hoodies/{hoodieId}/ZippedHoodie")]
        public HttpResponseMessage AddZippedHoodie(Guid hoodieId, ZippedHoodie zippedHoodie)
        {
            try
            {
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"INSERT INTO {tableName} (\"Id\", \"Name\", \"HoodieID\") VALUES (@id, @name, @hoodieId)";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    Guid id = Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", zippedHoodie.Name);
                    cmd.Parameters.AddWithValue("@hoodieId", hoodieId);

                    cmd.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, "ZippedHoodie Created");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Hoodies/{hoodieId}/ZippedHoodie")]
        public HttpResponseMessage GetZippedHoodies(Guid hoodieId)
        {
            try
            {
                List<ZippedHoodie> zippedHoodies = new List<ZippedHoodie>();
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"SELECT * FROM {tableName} WHERE \"HoodieID\" = @hoodieId";
                using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@hoodieId", hoodieId);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ZippedHoodie zippedHoodie = ReadZippedHoodie(reader);
                            zippedHoodies.Add(zippedHoodie);
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, zippedHoodies);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("api/Hoodies/{hoodieId}/ZippedHoodie/{zippedHoodieId}")]
        public HttpResponseMessage UpdateZippedHoodie(Guid hoodieId, Guid zippedHoodieId, ZippedHoodie zippedHoodie)
        {
            try
            {
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                var commandText = $"UPDATE {tableName} SET \"Name\" = @name WHERE \"Id\" = @zippedHoodieId AND \"HoodieID\" = @hoodieId";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@zippedHoodieId", zippedHoodieId);
                    cmd.Parameters.AddWithValue("@hoodieId", hoodieId);
                    cmd.Parameters.AddWithValue("@name", zippedHoodie.Name);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "ZippedHoodie Updated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "ZippedHoodie not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Hoodies/{hoodieId}/ZippedHoodie/{zippedHoodieId}")]
        public HttpResponseMessage DeleteZippedHoodie(Guid hoodieId, Guid zippedHoodieId)
        {
            try
            {
                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                string commandText = $"DELETE FROM {tableName} WHERE \"Id\" = @zippedHoodieId AND \"HoodieID\" = @hoodieId";
                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@zippedHoodieId", zippedHoodieId);
                    cmd.Parameters.AddWithValue("@hoodieId", hoodieId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "ZippedHoodie Deleted");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "ZippedHoodie not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
       
        [Route("api/Hoodies/{hoodieId}/ZippedHoodie")]
        public Hoodie GetHoodieById(Guid hoodieId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("\"Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;\""))
                {
                    connection.Open();

                    string commandText = "SELECT * FROM \"Hoodie\" WHERE \"Id\" = @id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", hoodieId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Guid id = reader["Id"] != DBNull.Value ? (Guid)reader["Id"] : Guid.Empty;
                                string name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty;
                                string size = reader["Size"] != DBNull.Value ? reader["Size"].ToString() : string.Empty;
                                string style = reader["Style"] != DBNull.Value ? reader["Style"].ToString() : string.Empty;

                                return new Hoodie(id, name, size, style);
                            }
                            else
                            {
                                return null; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Hoodie by Id: " + ex.Message);
            }
        }
        private ZippedHoodie ReadZippedHoodie(NpgsqlDataReader reader)
        {
            Guid id = (Guid)reader["Id"];
            string name = reader["Name"].ToString();
            Guid hoodieID= (Guid)reader["HoodieID"];

            Hoodie hoodie = GetHoodieById(hoodieID);

            ZippedHoodie zippedHoodie = new ZippedHoodie(id, name, hoodie); 
            return zippedHoodie;
        }
    }
}
