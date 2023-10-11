using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using WebAppii.Models; 
using WebAppii.Service.Common;
namespace WebAppii.Controllers
{
    public class ZippedHoodieController : ApiController
    { 
        private IZippedHoodieService service { get; set; }
        public ZippedHoodieController(IZippedHoodieService service)
        {
            this.service = service;
        }

        public HttpResponseMessage GetAll(string query,int pageSize,int pageNumber,string sortBY,string size) { 
        
        List<ZippedHoodie> zippedHoodies = service.GetAll();
            if (zippedHoodies != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, zippedHoodies);
            }
            else
            {
                
                return Request.CreateResponse(HttpStatusCode.NotFound, "No zipped hoodies found.");
            }
        }

        public HttpResponseMessage Get(Guid id)
        {
            ZippedHoodie hoodie = service.GetHoodieById(id);
            if (hoodie != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, hoodie);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
            }
        }

        [HttpPost]
        public HttpResponseMessage Add(ZippedHoodie hoodie)
        {
            string acc = service.Post(hoodie);
            if (acc == "Created")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not added");
            }
        }

        [HttpPut]
        public HttpResponseMessage Update(ZippedHoodie hoodie)
        {
            string acc = service.Update(hoodie);
            if (acc == "Updated")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not updated");
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            string acc = service.Delete(id);
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
