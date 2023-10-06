using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppii.Model.Common
{
    public interface IHoodieModelCommon
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Size { get; set; }

        string Style { get; set; }

    }
}
