

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using WebAppii.Model.Common;

namespace WebAppii.Models
{


    public class Hoodie:IHoodieModelCommon
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }

        public string Style { get; set; }

        public List<ZippedHoodie> ZippedHoodies { get; set; }

        public Hoodie()
        {

        }

        public Hoodie(Guid id, string name, string size, string style)
        {
            Id = id;
            Name = name;
            Size = size;
            Style = style;
            ZippedHoodies = new List<ZippedHoodie>();
        }
    }
}