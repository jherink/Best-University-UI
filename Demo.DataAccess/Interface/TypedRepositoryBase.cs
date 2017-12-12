using System;
using System.Data;

namespace Demo.DataAccess
{
    public abstract class TypedRepositoryBase<TKey, TValue> :
        TypedReadOnlyRepositoryBase<TKey, TValue>, ITypedRepository<TKey, TValue>
    {
        #region Template Pattern

        // Since most CRUD operations will perform the same 
        // with the exception of the actual operation performed we can 
        // let the interface operation contain the boilerplate code for 
        // performing the operation and call the abstract implementation of
        // the inheritors to get the "where" & "how" to get our data.

        protected abstract void ConfigureInsertCommand( TKey key, TValue item, IDbCommand command );

        protected abstract void ConfigureUpdateCommand( TKey key, TValue item, IDbCommand command );

        protected abstract void ConfigureDeleteCommand( TValue item, IDbCommand command );

        public abstract TKey CreateKey( TValue item );

        #endregion

        public TypedRepositoryBase( IDbConnection connection ) : base( connection )
        {
        }

        protected bool PerformCUD( IDbCommand cudCommand )
        {
            // Set the connection.
            //if ( Connection.State != ConnectionState.Open ) Connection.Open(); // REMOVE
            cudCommand.Connection = Connection;

            // Setup transaction.
            var commitTransaction = false;
            if ( Transaction == null )
            {
                commitTransaction = true;
                cudCommand.Transaction = Connection.BeginTransaction();
            }
            else
            {
                commitTransaction = false;
                cudCommand.Transaction = Transaction;
            }

            // Perform operation.
            try
            {
                try
                {
                    cudCommand.ExecuteNonQuery();
                    if ( commitTransaction ) cudCommand.Transaction.Commit();
                    return true;
                }
                catch ( Exception e )
                {
                    Log( e.Message );
                    cudCommand.Transaction.Rollback();
                }
            }
            catch ( Exception transactionException )
            {
                HandleTransactionException( transactionException, cudCommand.Transaction );
                throw transactionException;
            }

            return false;
        }

        #region [C]RUD

        public bool Insert( IDbCommand command )
        {
            if ( CheckOpenConnection() )
            {
                return PerformCUD( command );
            }
            return false;
        }

        public virtual TKey Insert( TValue item )
        {
            if ( CheckOpenConnection() )
            {
                var cmd = Connection.CreateCommand();  // Create the command.
                var key = CreateKey( item );           // Create the key.   
                cmd.Transaction = Transaction;
                ConfigureInsertCommand( key, item, cmd );    // Call the implementation.

                return PerformCUD( cmd ) ? key : default( TKey );
            }
            return default( TKey );
        }

        #endregion

        #region C[R]UD

        // Inherited from ReadOnlyRepository Base Class.

        #endregion

        #region CR[U]D

        public bool Update( IDbCommand cmd )
        {
            if ( CheckOpenConnection() )
            {
                return PerformCUD( cmd );
            }
            return false;
        }

        public virtual bool Update( TKey key, TValue item )
        {
            var cmd = Connection.CreateCommand();         // Create the command.
            ConfigureUpdateCommand( key, item, cmd );     // Call implementation

            return Update( cmd );
        }

        #endregion

        #region CRU[D]

        public virtual bool Delete( TValue item )
        {
            if ( CheckOpenConnection() )
            {
                var cmd = Connection.CreateCommand();   // Create the command.
                ConfigureDeleteCommand( item, cmd );     // Call the implementation.

                return PerformCUD( cmd );
            }
            return false;
        }

        public bool DeleteMany( IDbCommand command )
        {
            return PerformCUD( command );
        }

        #endregion               

    }
}
