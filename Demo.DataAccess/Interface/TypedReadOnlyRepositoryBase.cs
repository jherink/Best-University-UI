using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo.DataAccess
{
    public abstract class TypedReadOnlyRepositoryBase<TKey, TValue> : ReadOnlyRepositoryBase, ITypedReadOnlyRepository<TKey, TValue>
    {
        public TypedReadOnlyRepositoryBase( IDbConnection connection ) : base( connection )
        {
        }

        #region Template Pattern

        protected abstract void ConfigureReadCommand( TKey key, IDbCommand command );

        protected abstract void ConfigureReadAllCommand( IDbCommand command );

        internal abstract TValue Parse( IReadOnlyDictionary<string, object> record );

        #endregion

        public virtual TValue Read( TKey key )
        {
            var cmd = Connection.CreateCommand(); // Create the command.
            ConfigureReadCommand( key, cmd );

            var result = Read( cmd );
            if ( result != null ) return Parse( result );

            return default( TValue );
        }

        public new virtual IEnumerable<TValue> ReadMany( IDbCommand command )
        {
            if ( CheckOpenConnection() )
            {
                command.Connection = Connection;
                if ( command.Transaction == null ) command.Transaction = Transaction;

                var records = new List<IReadOnlyDictionary<string, object>>();
                using ( var reader = command.ExecuteReader() )
                {
                    while ( reader.Read() )
                    {
                        records.Add( Enumerable.Range( 0, reader.FieldCount )
                                               .ToDictionary( reader.GetName, reader.GetValue ) );
                    }
                }
                foreach ( var record in records )
                {
                    yield return Parse( record );
                }
            }
            //yield return default( TValue ); 
        }

        public virtual IEnumerable<TValue> ReadAll()
        {
            if ( CheckOpenConnection() )
            {
                var cmd = Connection.CreateCommand();
                ConfigureReadAllCommand( cmd );
                return ReadMany( cmd );
            }
            return new TValue[] { };
        }

        protected IDbDataParameter AddCommandParameter<T>( IDbCommand command, string parameterName, T value )
        {
            var cmd = command.CreateParameter();
            cmd.ParameterName = parameterName;
            cmd.Value = value;
            command.Parameters.Add( cmd );
            return cmd;
        }    

    }
}
