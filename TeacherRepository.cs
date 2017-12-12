using System;
using System.Collections.Generic;
using System.Data;
using Demo.DTO;

namespace Demo.DataAccess
{
    public sealed class TeacherRepository : TypedRepositoryBase<int, Teacher>
    {
        public TeacherRepository( IDbConnection connection ) : base( connection )
        {
        }

        public override int CreateKey( Teacher item )
        {
            return item.ID;
        }

        protected override void ConfigureDeleteCommand( Teacher item, IDbCommand command )
        {
            throw new NotImplementedException( "Teachers cannot be deleted" );
        }

        protected override void ConfigureInsertCommand( int key, Teacher item, IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "InsertTeacher";

            AddCommandParameter( command, "@FirstName", item.FirstName );
            AddCommandParameter( command, "@MiddleName", item.MiddleName );
            AddCommandParameter( command, "@LastName", item.LastName );
            AddCommandParameter( command, "@Email", item.Email );
            AddCommandParameter( command, "@Department", item.Department.DepartmentID );
            AddCommandParameter( command, "@Title", item.Title );
            AddCommandParameter( command, "@Address1", item.Address.Address1 );
            AddCommandParameter( command, "@Address2", item.Address.Address2 );
            AddCommandParameter( command, "@ZipCode", item.Address.ZipCode );
            AddCommandParameter( command, "@PhoneNumber", item.PhoneNumber );
            AddCommandParameter( command, "@DateOfBirth", item.DateOfBirth );
        }

        protected override void ConfigureReadAllCommand( IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ReadAllTeacher";
        }

        protected override void ConfigureReadCommand( int key, IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ReadTeacher";

            AddCommandParameter( command, "@TeacherID", key );
        }

        protected override void ConfigureUpdateCommand( int key, Teacher item, IDbCommand command )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateStudent";

            AddCommandParameter( command, "@FirstName", item.FirstName );
            AddCommandParameter( command, "@LastName", item.LastName );
            AddCommandParameter( command, "@Email", item.Email );
            AddCommandParameter( command, "@Department", item.Department.DepartmentID );
        }

        public int GetNumberOfTeachers()
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "GetNumberOfTeachers";
            cmd.CommandType = CommandType.StoredProcedure;

            var result = Read( cmd );
            if ( result != null ) return Convert.ToInt32( result["TeacherCount"] );

            return 0;
        }

        internal override Teacher Parse( IReadOnlyDictionary<string, object> record )
        {
            using ( var departmentRepo = new DepartmentRepository( Connection ) )
            {
                return new Teacher
                {
                    ID = Convert.ToInt32( record["TeacherID"] ),
                    FirstName = record["FirstName"].ToString(),
                    MiddleName = record["MiddleName"].ToString(),
                    LastName = record["LastName"].ToString(),
                    Department = departmentRepo.Parse( record ),
                    Email = record["Email"].ToString(),
                    Title = record["Title"].ToString(),
                    EmploymentDate = Convert.ToDateTime( record["EmploymentDate"] ),
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
        }
    }
}
