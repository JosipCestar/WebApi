

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using WebAppii.Controllers;
using WebAppii.Models;

public class Hoodie
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
