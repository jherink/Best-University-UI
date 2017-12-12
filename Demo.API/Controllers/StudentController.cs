using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Buisness;
using Demo.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        [HttpGet( "[action]" )]
        public Student GetStudent( int id )
        {
            return new EnrollmentManager().Get( id );
        }

        [HttpGet( "[action]" )]
        public IEnumerable<Student> Search( string firstName, string lastName )
        {
            var arr = new EnrollmentManager().Search( firstName, lastName );
            return arr;
        }

        [HttpPost( "[action]" )]
        public Student EnrollStudent( [FromBody]Student student )
        {
            new EnrollmentManager().Create( student );
            return student; // return new student.
        }
        
    }
}