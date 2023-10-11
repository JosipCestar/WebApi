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
    public class HoodieService : IHoodieService
{
        private IHoodieRepository repository;


    public HoodieService(IHoodieRepository _repository)
        {
            repository = _repository;
        }
    
    public async Task<string> Delete(Guid id)
    {
            string acc= await repository.DeleteHoodie(id);
        return acc;
    }

    public async Task<List<Hoodie>> GetAll()
    {
        List<Hoodie> hoodies= await repository.GetAllHoodies();
        return hoodies;
    }

    public async Task<Hoodie> GetHoodieById(Guid id)
    {
        Hoodie hoodie = await repository.GetHoodieById(id);
        return hoodie;
    }

    public async Task<string> Post(Hoodie hoodie)
    {
        string acc= await repository.PostHoodie(hoodie);
        return acc;
    }

    public async Task<string> Update(Hoodie hoodie,Guid id)
    {
        string acc=await repository.UpdateHoodie(hoodie,id);
            return acc;
    }
}
}
