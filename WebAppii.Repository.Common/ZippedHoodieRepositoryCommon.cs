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
        string Delete(Guid id);
        List<ZippedHoodie> GetAll();
        ZippedHoodie GetHoodieById(Guid id);
        string Post(ZippedHoodie zippedHoodie);
        string Update(ZippedHoodie zippedHoodie);
    }
}
