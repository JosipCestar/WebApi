using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;

namespace WebAppii.Service.Common
{
    public interface ZippedHoodieServiceCommon
    {
        Task<List<Hoodie>> GetAllZippedHoodies();
        Task<Hoodie> GetZippedHoodieById(Guid id);
        Task PostZippedHoodie(Hoodie hoodie);

        Task DeleteZippedHoodie(Guid id);
        Task UpdateZippedHoodie(Hoodie hoodie);
    }
}
