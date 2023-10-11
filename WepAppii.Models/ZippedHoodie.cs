using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAppii.Model.Common;

namespace WebAppii.Models
{
    public class ZippedHoodie:IZippedHoddieModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        
        public Hoodie HoodieID { get; set; }
       
        public ZippedHoodie()
        {
            
        }

        public ZippedHoodie(Guid id, string name, Hoodie hoodie)
        {
            Id = id;
            Name = name;
            HoodieID = hoodie;
        }

    }
    


}
