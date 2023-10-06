using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAppii.Models
{
    public class ZippedHoodie
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        [Required]
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
