﻿using ASPBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPBase.DataAccess.Repository.IReopsitory
{
    public interface ICategoryRepository : IReopsitory<Category>
    {
        void Update(Category obj);
    }
}
