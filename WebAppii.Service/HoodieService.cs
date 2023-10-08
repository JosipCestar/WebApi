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
     private HoodieRepositoryCommon repository { get; set; }

    public HoodieService(HoodieRepositoryCommon repository)
        {
            this.repository = repository;
        }
    
    public string DeleteHoodie(Guid id)
    {
            string acc= repository.DeleteHoodie(id);
        return acc;
    }

    public List<Hoodie> GetAllHoodies()
    {
        List<Hoodie> hoodies=repository.GetAllHoodies();
        return hoodies;
    }

    public Hoodie GetHoodieById(Guid id)
    {
        Hoodie hoodie = repository.GetHoodieById(id);
        return hoodie;
    }

    public string PostHoodie(Hoodie hoodie)
    {
        string acc= repository.PostHoodie(hoodie);
        return acc;
    }

    public string UpdateHoodie(Hoodie hoodie)
    {
        string acc= repository.UpdateHoodie(hoodie);
            return acc;
    }
}
}
