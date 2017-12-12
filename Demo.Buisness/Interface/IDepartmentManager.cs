using Demo.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Buisness
{
    public interface IDepartmentManager
    {
        IEnumerable<Department> GetDepartments();
    }
}
