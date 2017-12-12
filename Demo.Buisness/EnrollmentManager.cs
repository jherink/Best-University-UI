using System;
using System.Collections.Generic;
using Demo.DataAccess;
using Demo.DTO;

namespace Demo.Buisness
{
    public class EnrollmentManager : PersonelManager<Student>
    {
        public int GetNumberOfEnrolledStudents()
        {
            using ( var studentRepo = new StudentRepository( DBFactory.GetConnection() ) )
            {
                return studentRepo.GetEnrolledStudentCount();
            }
        }

        protected override Student CreateImpl( Student person )
        {
            // When enrolling a new student the must always be active.
            // Also create their email address.
            person.EnrollmentStatus = EnrollmentStatus.Active;

            using ( var studentRepo = new StudentRepository( DBFactory.GetConnection() ) )
            {
                person.ID = studentRepo.Insert( person );
            }

            return person;
        }

        public override Student Get( int ID )
        {
            using ( var studentRepo = new StudentRepository( DBFactory.GetConnection() ) )
            {
                return studentRepo.Read( ID );
            }
        }

        public override IEnumerable<Student> Search( string firstName, string lastName )
        {
            using ( var studentRepo = new StudentRepository( DBFactory.GetConnection() ) )
            {
                if ( string.IsNullOrWhiteSpace( firstName ) ) firstName = string.Empty;
                if ( string.IsNullOrWhiteSpace( lastName ) ) lastName = string.Empty;

                return studentRepo.Search( firstName, lastName );
            }
        }
        
        public IEnumerable<YearlyEnrollmentFact> GetEnrollmentStatistics()
        {
            using ( var studentRepo = new StudentRepository( DBFactory.GetConnection() ) )
            {
                return studentRepo.GetYearlyEnrollmentFacts();
            }
        }
    }
}
