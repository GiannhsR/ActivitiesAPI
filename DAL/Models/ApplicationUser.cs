﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.Models;

namespace ToDoActivities.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Activity> Activity { get; set; }
    }
}