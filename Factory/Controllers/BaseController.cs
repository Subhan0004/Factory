using Factory.Core.Domain.Abstract;
using Factory.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork DB;
        protected readonly UserManager<User> UserManager;

        public BaseController(IUnitOfWork db, UserManager<User> userManager)
        {
            DB = db;
            UserManager = userManager;
        }

        public User CurrentUser => UserManager.GetUserAsync(User).Result;

    }
}
