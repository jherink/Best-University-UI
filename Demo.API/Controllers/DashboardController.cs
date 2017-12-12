using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Buisness;
using Demo.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Produces( "application/json" )]
    [Route( "api/[controller]" )]
    public class DashboardController : Controller
    {
        [HttpGet( "[action]" )]
        public int GetEnrolledStudentCount()
        {
            return new EnrollmentManager().GetNumberOfEnrolledStudents();
        }

        [HttpGet( "[action]" )]
        public Ratio GetTeacherStudentRatio()
        {
            var eManager = new EnrollmentManager();
            var tManager = new TeacherManager();
            var ratio = new StudentTeacherRatio()
            {
                Numerator = eManager.GetNumberOfEnrolledStudents(),
                Denominator = tManager.GetNumberOfTeachers()             
            };
            ratio.Simplify();
            return ratio;
        }

        [HttpGet( "[action]" )]
        public int GetNumberOfTeachers()
        {
            return new TeacherManager().GetNumberOfTeachers();
        }

        [HttpGet("[action]")]
        public IEnumerable<YearlyEnrollmentFact> GetEnrollmentStatistics()
        {
            return new EnrollmentManager().GetEnrollmentStatistics();
        }
    }
}