using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using Npgsql;
using WebAppii.Models;
namespace WebAppii.Controllers
{
    public class HoodieController : ApiController
    {
        private NpgsqlConnection connection;
        private string tableName = "\"Hoodie\"";

        public HoodieController()
        {
            
        
        }

        [HttpPost]
        public HttpResponseMessage Add(Hoodie hoodie)
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
                    return Request.CreateResponse(HttpStatusCode.OK, "Created");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage Get(Guid id)
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
                            return Request.CreateResponse(HttpStatusCode.OK, hoodie);
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]

        public HttpResponseMessage GetAll() { 
         
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
                        return Request.CreateResponse(HttpStatusCode.OK, hoodies);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        
        
        }


        private Hoodie ReadHoodie(NpgsqlDataReader reader)
        {
            Guid? id = reader["id"] as Guid?;
            string name = reader["name"].ToString();
            string size = reader["size"].ToString();
            string style = reader["style"].ToString();
            

            Hoodie hoodie = new Hoodie(id.Value,name, size, style);
            return hoodie;
        }

        [HttpPut]
        public HttpResponseMessage Update(Guid id, Hoodie hoodie)
            //dohvati prvo pa izmjeni
        {
            try
            {
                var hoodieSearch = Get(id);
                //make it so if a hoodie is found put the hoodie values to hoodieSearch;
                if (hoodieSearch.StatusCode == HttpStatusCode.OK)
                {
                    hoodieSearch.TryGetContentValue<Hoodie>(out Hoodie hoodieSearchValue);
                    hoodieSearchValue.Name = hoodie.Name;
                    hoodieSearchValue.Size = hoodie.Size;
                    hoodieSearchValue.Style = hoodie.Style;
                    hoodie = hoodieSearchValue;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
                }



                connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=neznam555;Database=postgres;");
                connection.Open();
                var commandText = $"UPDATE {tableName} SET \"Name\" = @name, \"Size\" = @size, \"Style\" = @style WHERE \"id\" = @id";

                using (var cmd = new NpgsqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", hoodie.Name);
                    cmd.Parameters.AddWithValue("@size", hoodie.Size);
                    cmd.Parameters.AddWithValue("@style", hoodie.Style);

                    cmd.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
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
                        return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        
    }
}
