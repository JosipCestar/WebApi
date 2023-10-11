using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;
using WebAppii.Repository.Common;

namespace WebAppii.Repository
{
    internal class ZippedHoodieRepository : IZippedHoodieRepository
    {
        public Task<string> DeleteHoodie(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ZippedHoodie>> GetAllHoodies()
        {
            throw new NotImplementedException();
        }

        public Task<ZippedHoodie> GetHoodieById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> PostHoodie(ZippedHoodie hoodie)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateHoodie(ZippedHoodie hoodie, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
