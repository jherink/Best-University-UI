using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Demo.DTO;

namespace Demo.DataAccess
{
    public sealed class DepartmentRepository : TypedRepositoryBase<int, Department>
    {
        public DepartmentRepository( IDbConnection connection ) : base( connection )
        {
        }

        public override int CreateKey( Department item )
        {
            return item.DepartmentID;
        }

        public void SetDepartmentHead( Department department, Teacher teacher )
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SetDepartmentHead";
            cmd.CommandType = CommandType.StoredProcedure;

            AddCommandParameter( cmd, "@DepartmentID", department.DepartmentID );
            AddCommandParameter( cmd, "@TeacherID", teacher.ID );

            Insert( cmd );
        }

        protected override void ConfigureDeleteCommand( Department item, IDbCommand command )
        {
            throw new NotImplementedException("Departments cannot be deleted.");
        }

        protected override void ConfigureInsertCommand( int key, Department item, IDbCommand command )
        {
            command.CommandText = "InsertDepartment";
            command.CommandType = CommandType.StoredProcedure;

            AddCommandParameter( command, "@DepartmentName", item.Name );
        }

        protected override void ConfigureReadAllCommand( IDbCommand command )
        {
            command.CommandText = "ReadAllDepartment";
            command.CommandType = CommandType.StoredProcedure;
        }

        protected override void ConfigureReadCommand( int key, IDbCommand command )
        {
            command.CommandText = "ReadDepartment";
            command.CommandType = CommandType.StoredProcedure;

            AddCommandParameter( command, "@DepartmentID", key );
        }

        protected override void ConfigureUpdateCommand( int key, Department item, IDbCommand command )
        {
            throw new NotImplementedException("Departments cannot be updated.");
        }

        internal override Department Parse( IReadOnlyDictionary<string, object> record )
        {
            return new Department
            {
                DepartmentID = Convert.ToInt32( record["DepartmentID"] ),
                Name = record["DepartmentName"] as string
            };
        }
    }
}
