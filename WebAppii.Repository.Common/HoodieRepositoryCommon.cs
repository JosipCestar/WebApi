using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;

namespace WebAppii.Repository.Common
{
    public interface IHoodieRepository
   {
        Task<List<Hoodie>> GetAllHoodies();
        Task<Hoodie> GetHoodieById(Guid id);
        Task<String> PostHoodie(Hoodie hoodie);
        Task<String> DeleteHoodie(Guid id);

        Task<String> UpdateHoodie(Hoodie hoodie,Guid id);
    }
}
