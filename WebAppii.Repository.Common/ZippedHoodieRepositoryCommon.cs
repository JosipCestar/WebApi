using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;

namespace WebAppii.Repository.Common
{
    public interface ZippedHoodieRepositoryCommon
    {
        Task<List<ZippedHoodie>> GetAllHoodies();
        Task<ZippedHoodie> GetHoodieById(Guid id);
        Task<String> PostHoodie(ZippedHoodie hoodie);
        Task<String> DeleteHoodie(Guid id);

        Task<String> UpdateHoodie(ZippedHoodie hoodie, Guid id);
    }
}
