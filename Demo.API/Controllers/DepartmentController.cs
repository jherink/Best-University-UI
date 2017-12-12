using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Demo.DTO;
using Demo.Buisness;

namespace Demo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        // Wrap our API around the functionality of the Business Layer.
        private readonly IDepartmentManager mDeptManager = new DepartmentManager();

        [HttpGet("[action]")]
        public IEnumerable<Department> GetDepartments()
        {
            return mDeptManager.GetDepartments();
        }
    }
}