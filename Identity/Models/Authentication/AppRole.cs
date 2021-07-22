﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.Authentication
{
    public class AppRole:IdentityRole<Guid>
    {
        public DateTime RecordDate { get; set; }
    }
}
