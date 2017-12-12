using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.DTO;
using Demo.Buisness;

namespace Demo.API.Controllers
{
    [Produces( "application/json" )]
    [Route( "api/[controller]" )]
    public class TeacherController : Controller
    {
        [HttpGet( "[action]" )]
        public Teacher GetTeacher( int id )
        {
            return new TeacherManager().Get( id );
        }

        [HttpGet( "[action]" )]
        public IEnumerable<Teacher> GetTeachers()
        {
            return new TeacherManager().GetAllTeachers();
        }

        [HttpGet( "[action]" )]
        public IEnumerable<Teacher> Search( string firstName, string lastName )
        {
            return new TeacherManager().Search( firstName, lastName );
        }

        [HttpPost( "[action]" )]
        public Teacher CreateTeacher( [FromBody]Teacher teacher )
        {
            return new TeacherManager().Create( teacher );
        }
    }
}