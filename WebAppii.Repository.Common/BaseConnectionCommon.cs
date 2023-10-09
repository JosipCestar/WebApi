using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppii.Repository.Common
{
    public interface BaseConnectionCommon
    {
        void OpenConnection();
        void CloseConnection();

       }
}
