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
using WebAppii.Service.Common;
using WebApiPractice.Common;
namespace WebAppii.Controllers
{
    public class HoodieController : ApiController
    {

        private IHoodieService service;

        public HoodieController(IHoodieService _service)
        {
            service = _service;
        }
        
        [HttpPost]
        public async Task<HttpResponseMessage>  Add(Hoodie hoodie)
        {
            string acc = await service.Post(hoodie);
            if (acc == "Created")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
            
           
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not added");
           
        }
        [HttpGet]

        public async Task<HttpResponseMessage> Get(Guid id)
        {
            Hoodie hoodie =await service.GetHoodieById(id);
            if (hoodie != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, hoodie);
            }
            
           
            return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodie not found");
           
        }


        [HttpGet]

        public async Task<HttpResponseMessage> GetAll(string querryName,int pageSize,int pageNumber,string sortBy,string sortOrder,string sortSize,string sortStye) { 
         
            Paging paging = new Paging(pageSize,pageNumber);
            Sorting sorting = new Sorting(sortBy,sortOrder);
            Filtering filtering = new Filtering(querryName,sortSize,sortStye);
            List<Hoodie> hoodies = await service.GetAll(paging,sorting,filtering);
            if (hoodies != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, hoodies);
            }
           
            
                return Request.CreateResponse(HttpStatusCode.NotFound, "Hoodies not found");
            
        }


        
        [HttpPut]
        public async Task<HttpResponseMessage> Update(Guid id, Hoodie hoodie)
            
        {
           
           string acc = await service.Update(hoodie, id);
           if (acc == "Updated")
           {
                    return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                }
                
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not updated");
                
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            string acc = await service.Delete(id);
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
