using Demo.DataAccess;
using Demo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Buisness
{
    public class DepartmentManager : IDepartmentManager
    {
        public IEnumerable<Department> GetDepartments()
        {
            using (var repository = new DepartmentRepository( DBFactory.GetConnection() ) )
            {
                return repository.ReadAll().ToArray();
            }
        }
    }
}
