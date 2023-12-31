﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppii.Models;
using WebApiPractice.Common;

namespace WebAppii.Service.Common
{
    public interface IHoodieService
    {
        Task<List<Hoodie>> GetAll(Paging paging, Sorting sorting, Filtering filtering);
        Task<Hoodie>GetHoodieById(Guid id);
        Task<String> Post(Hoodie hoodie);

        Task<String> Delete(Guid id);
        Task<String> Update(Hoodie hoodie,Guid id);
    }
}
