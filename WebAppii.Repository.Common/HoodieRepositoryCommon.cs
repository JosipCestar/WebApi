﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;

namespace WebAppii.Repository.Common
{
    public interface HoodieRepositoryCommon
    {
        List<Hoodie> GetAllHoodies();
        Hoodie GetHoodieById(Guid id);
        String PostHoodie(Hoodie hoodie);
        String DeleteHoodie(Guid id);

        String UpdateHoodie(Hoodie hoodie);
    }
}