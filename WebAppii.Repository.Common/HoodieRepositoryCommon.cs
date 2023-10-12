using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPractice.Common;
using WebAppii.Models;
using WepAppii.Models;

namespace WebAppii.Repository.Common
{
    public interface IHoodieRepository
   {
        Task<List<Hoodie>> GetAllHoodies(Paging paging,Sorting sorting, Filtering filtering);
        Task<Hoodie> GetHoodieById(Guid id);
        Task<String> PostHoodie(Hoodie hoodie);
        Task<String> DeleteHoodie(Guid id);

        Task<String> UpdateHoodie(Hoodie hoodie,Guid id);
    }
}
