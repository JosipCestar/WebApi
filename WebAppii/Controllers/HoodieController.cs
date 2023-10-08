using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using Npgsql;
using WebAppii.Models;
using WebAppii.Service.Common;
namespace WebAppii.Controllers
{
    public class HoodieController : ApiController
    {
        private NpgsqlConnection connection;
        private string tableName = "\"Hoodie\"";

        private HoodieServiceCommon service { get; set; }

        public HoodieController(HoodieServiceCommon service)
        {
            this.service = service;
        }

        [HttpPost]
        public HttpResponseMessage Add(Hoodie hoodie)
        {
            string acc = service.PostHoodie(hoodie);
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

        public HttpResponseMessage Get(Guid id)
        {
            Hoodie hoodie = service.GetHoodieById(id);
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

        public HttpResponseMessage GetAll() { 
         
            List<Hoodie> hoodies = service.GetAllHoodies();
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
        public HttpResponseMessage Update(Guid id, Hoodie hoodie)
            
        {
            if (hoodie==null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            }
            else
            {
                string acc = service.UpdateHoodie(hoodie);
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
        public HttpResponseMessage Delete(Guid id)
        {
            string acc = service.DeleteHoodie(id);
            if (acc == "Deleted")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not deleted");
            }
        }

        
    }
}
