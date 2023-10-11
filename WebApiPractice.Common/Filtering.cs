using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPractice.Common
{
    public class Filtering
    {
        public string QuerryName { get; set; }
        public string QuerrySize { get; set; }

        public string QuerryStyle { get; set; }
        public Filtering(string querryName,string querrySize,string querryStyle) { 
        
            QuerryName = querryName;
            QuerrySize = querrySize;
            QuerryStyle = querryStyle;
        }
    }
}
