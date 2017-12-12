using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Demo.DTO;

namespace Demo.DataAccess
{
    public sealed class StudentRepository : TypedRepositoryBase<int, Student>
    {
        public StudentRepository( IDbConnection connection ) : base( connection )
        {
        }

        public override int CreateKey( Student item )
        {
            return item.ID;
        }

        protected override void ConfigureDeleteCommand( Student item, IDbCommand command )
        {
            throw new NotImplementedException( "Students cannot be deleted" );
        }

        protected override void ConfigureInsertCommand( int key, Student item, IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "InsertStudent";

            AddCommandParameter( command, "@FirstName", item.FirstName );
            AddCommandParameter( command, "@MiddleName", item.MiddleName );
            AddCommandParameter( command, "@LastName", item.LastName );
            AddCommandParameter( command, "@Email", item.Email );
            //AddCommandParameter( command, "@EnrollmentDate", item.EnrollmentDate );
            AddCommandParameter( command, "@EnrollmentStatus", (int)item.EnrollmentStatus );
            AddCommandParameter( command, "@DateOfBirth", item.DateOfBirth );
            AddCommandParameter( command, "@Address1", item.Address.Address1 );
            AddCommandParameter( command, "@Address2", item.Address.Address2 );
            AddCommandParameter( command, "@ZipCode", item.Address.ZipCode );
            AddCommandParameter( command, "@PhoneNumber", item.PhoneNumber );
        }

        protected override void ConfigureReadAllCommand( IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ReadAllStudent";
        }

        protected override void ConfigureReadCommand( int key, IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ReadStudent";

            AddCommandParameter( command, "@StudentID", key );
        }

        protected override void ConfigureUpdateCommand( int key, Student item, IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateStudent";

            AddCommandParameter( command, "@FirstName", item.FirstName );
            AddCommandParameter( command, "@LastName", item.LastName );
            AddCommandParameter( command, "@Email", item.Email );
            AddCommandParameter( command, "@EnrollmentStatus", (int)item.EnrollmentStatus );
        }

        internal override Student Parse( IReadOnlyDictionary<string, object> record )
        {
            return new Student
            {
                ID = Convert.ToInt32( record["StudentID"] ),
                FirstName = record["FirstName"].ToString(),
                MiddleName = record["MiddleName"].ToString(),
                LastName = record["LastName"].ToString(),
                Email = record["Email"].ToString(),
                EnrollmentDate = Convert.ToDateTime( record["EnrollmentDate"] ),
                EnrollmentStatus = (EnrollmentStatus)Convert.ToInt32( record["EnrollmentStatus"] ),
                DateOfBirth = Convert.ToDateTime( record["DateOfBirth"] ),
                Address = new Address
                {
                    Address1 = record["Address1"].ToString(),
                    Address2 = record["Address2"].ToString(),
                    ZipCode = Convert.ToInt32( record["ZipCode"] )
                },
                PhoneNumber = record["PhoneNumber"].ToString()
            };
        }

        public IEnumerable<Student> Search( string firstName, string lastName )
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SearchForStudent";

            AddCommandParameter( cmd, "@firstName", firstName );
            AddCommandParameter( cmd, "@lastName", lastName );

            return ReadMany( cmd ).ToArray();
        }

        public int GetEnrolledStudentCount()
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "GetEnrolledStudents";

            var result = Read( cmd );
            if ( result != null ) return Convert.ToInt32( result["EnrolledStudents"] );

            return 0;
        }

        public IEnumerable<YearlyEnrollmentFact> GetYearlyEnrollmentFacts()
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "GetEnrollmentCounts";

            var result = ReadManyCommand( cmd );
            if ( result != null )
            {
                var data = new List<YearlyEnrollmentFact>();
                foreach ( var record in result ) {
                    data.Add( new YearlyEnrollmentFact
                    {
                        Year = Convert.ToInt32( record["EnrollmentYear"] ),
                        Students = Convert.ToInt32( record["EnrollmentCount"] )
                    } );
                }
                return data;
            }
            return new YearlyEnrollmentFact[] { };
        }
    }
}
