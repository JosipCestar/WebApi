using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;
using WebAppii.Repository.Common;
using WebAppii.Service.Common;
namespace WebAppii.Service
{
    internal class ZippedHoodieService : ZippedHoodieServiceCommon
    {
        private ZippedHoodieRepositoryCommon repository;
        public ZippedHoodieService(ZippedHoodieRepositoryCommon repository)
        {
            this.repository = repository;
        }


        public string Delete(Guid id)
        {
            string acc = repository.Delete(id);
            return acc;
        }

        public List<ZippedHoodie> GetAll()
        {
            List<ZippedHoodie> hoodies = repository.GetAll();
            return hoodies;
        }

        public ZippedHoodie GetHoodieById(Guid id)
        {
            ZippedHoodie hoodie = repository.GetHoodieById(id);
            return hoodie;
        }

        public string Post(ZippedHoodie hoodie)
        {
            string acc = repository.Post(hoodie);
            return acc;
        }

        public string Update(ZippedHoodie hoodie)
        {
        string acc = repository.Update(hoodie);
            return acc;
        }
    }
}
