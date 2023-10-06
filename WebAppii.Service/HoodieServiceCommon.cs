using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;

namespace WebAppii.Service.Common
{
    public interface HoodieServiceCommon
    {
        Task<List<Hoodie>> GetAllHoodies();
        Task<Hoodie> GetHoodieById(Guid id);
        Task PostHoodie(Hoodie hoodie);

        Task DeleteHoodie(Guid id);
        Task UpdateHoodie(Hoodie hoodie);
    }
}
