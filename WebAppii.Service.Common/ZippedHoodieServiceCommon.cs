using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;

namespace WebAppii.Service.Common
{
    public interface IZippedHoodieService
    {
        List<ZippedHoodie> GetAll();
        ZippedHoodie GetHoodieById(Guid id);
        String Post(ZippedHoodie hoodie);

        String Delete(Guid id);
        String Update(ZippedHoodie hoodie);
    }
}
