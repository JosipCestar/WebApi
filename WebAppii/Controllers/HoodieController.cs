using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using Npgsql;
using WebAppii.Models;
using WebAppii.Service;
namespace WebAppii.Controllers
{
    public class HoodieController : ApiController
    {
        private NpgsqlConnection connection;
        private string tableName = "\"Hoodie\"";

        private HoodieService service;

        public HoodieController()
        {
            service = new HoodieService();
        }

        [HttpPost]
        public async Task<HttpResponseMessage>  Add(Hoodie hoodie)
        {
            string acc = await service.PostHoodie(hoodie);
            if (acc == "Created")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not added");
            }
        }
        [HttpGet]

        public async Task<HttpResponseMessage> Get(Guid id)
        {
            Hoodie hoodie =await service.GetHoodieById(id);
            if (hoodie != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, hoodie);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
            }
        }


        [HttpGet]

        public async Task<HttpResponseMessage> GetAll() { 
         
            List<Hoodie> hoodies = await service.GetAllHoodies();
            if (hoodies != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, hoodies);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodies not found");
            }
        }


        
        [HttpPut]
        public async Task<HttpResponseMessage> Update(Guid id, Hoodie hoodie)
            
        {
            if (hoodie==null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            }
            else
            {
                string acc = await service.UpdateHoodie(hoodie, id);
                if (acc == "Updated")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not updated");
                }
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            string acc = await service.DeleteHoodie(id);
            if (acc == "Deleted")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            else if (acc == "Hoodie not found")
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
            }
            else if (acc == "Error")
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not deleted");
            }
        }

        
    }
}
