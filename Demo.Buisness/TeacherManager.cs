using Demo.DataAccess;
using Demo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Buisness
{
    public sealed class TeacherManager : PersonelManager<Teacher>
    {
        public int GetNumberOfTeachers()
        {
            using ( var teacherRepo = new TeacherRepository( DBFactory.GetConnection() ) )
            {
                return teacherRepo.GetNumberOfTeachers();
            }
        }

        public Teacher[] GetAllTeachers()
        {
            using ( var teacherRepo = new TeacherRepository( DBFactory.GetConnection() ) )
            {
                return teacherRepo.ReadAll().ToArray();
            }
        }

        public override Teacher Get( int ID )
        {
            using ( var teacherRepo = new TeacherRepository( DBFactory.GetConnection() ) )
            {
                return teacherRepo.Read( ID );
            }
        }

        public override IEnumerable<Teacher> Search( string firstName, string lastName )
        {
            throw new NotImplementedException();
            //using ( var teacherRepo = new TeacherRepository( DBFactory.GetConnection() ) )
            //{
            //    if ( string.IsNullOrWhiteSpace( firstName ) ) firstName = string.Empty;
            //    if ( string.IsNullOrWhiteSpace( lastName ) ) lastName = string.Empty;

            //    return teacherRepo.Search( firstName, lastName );
            //}
        }

        protected override Teacher CreateImpl( Teacher person )
        {
            using ( var teacherRepo = new TeacherRepository( DBFactory.GetConnection() ) )
            {
                person.ID = teacherRepo.Insert( person );
            }
            return person;
        }
    }
}
