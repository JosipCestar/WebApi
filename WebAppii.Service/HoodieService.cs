using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;
using WebAppii.Service.Common;
using WebAppii.Repository.Common;

namespace WebAppii.Service
{ 
    public class HoodieService : HoodieServiceCommon
{
        private HoodieRepositoryCommon repository;


    public HoodieService(HoodieRepositoryCommon _repository)
        {
            repository = _repository;
        }
    
    public async Task<string> DeleteHoodie(Guid id)
    {
            string acc= await repository.DeleteHoodie(id);
        return acc;
    }

    public async Task<List<Hoodie>> GetAllHoodies()
    {
        List<Hoodie> hoodies= await repository.GetAllHoodies();
        return hoodies;
    }

    public async Task<Hoodie> GetHoodieById(Guid id)
    {
        Hoodie hoodie = await repository.GetHoodieById(id);
        return hoodie;
    }

    public async Task<string> PostHoodie(Hoodie hoodie)
    {
        string acc= await repository.PostHoodie(hoodie);
        return acc;
    }

    public async Task<string> UpdateHoodie(Hoodie hoodie,Guid id)
    {
        string acc=await repository.UpdateHoodie(hoodie,id);
            return acc;
    }
}
}
