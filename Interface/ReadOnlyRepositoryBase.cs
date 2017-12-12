using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo.DataAccess
{
    public abstract class ReadOnlyRepositoryBase : IReadOnlyRepository
    {
        protected IDbConnection Connection;
        protected IDbTransaction Transaction;
        private bool m_recievedOpen = false;
        protected bool AllowCommit { get; private set; } = true;

        public ReadOnlyRepositoryBase( IDbConnection connection )
        {
            SetConnection( connection );
        }

        public IReadOnlyDictionary<string, object> Read( IDbCommand cmd )
        {
            var record = default( IReadOnlyDictionary<string, object> );

            if ( CheckOpenConnection() )
            {
                if ( Transaction == null )
                {
                    Transaction = Connection.BeginTransaction();
                }
                cmd.Transaction = Transaction;

                try
                {
                    var reader = cmd.ExecuteReader();
                    while ( reader.Read() )
                    {
                        record = Enumerable.Range( 0, reader.FieldCount )
                                           .ToDictionary( reader.GetName, reader.GetValue );
                        // there will only be one.  Close for parse implementations.
                    }
                    reader.Close();
                }
                catch ( Exception ex )
                {
                    Log( ex.Message );
                    throw ex;
                }
            }
            return record;
        }

        public IEnumerable<IReadOnlyDictionary<string, object>> ReadManyCommand( IDbCommand cmd )
        {
            var records = new List<IReadOnlyDictionary<string, object>>();
            if ( CheckOpenConnection() )
            {
                if ( Transaction == null )
                {
                    Transaction = Connection.BeginTransaction();
                }
                cmd.Transaction = Transaction;

                try
                {
                    var reader = cmd.ExecuteReader();
                    while ( reader.Read() )
                    {
                        records.Add( Enumerable.Range( 0, reader.FieldCount )
                                           .ToDictionary( reader.GetName, reader.GetValue ) );
                        // there will only be one.  Close for parse implementations.
                    }
                    reader.Close();
                }
                catch ( Exception ex )
                {
                    Log( ex.Message );
                    throw ex;
                }
            }
            return records;
        }

        public void SetTransaction( IDbTransaction transaction, bool allowCommit = true )
        {
            Transaction = transaction;
            AllowCommit = allowCommit;
        }

        public IDbTransaction GetTransaction()
        {
            return Transaction;
        }

        public void SetConnection( IDbConnection connection )
        {
            Connection = connection;
            m_recievedOpen = Connection.State == ConnectionState.Open;
        }

        protected void HandleTransactionException( Exception transactionException, IDbTransaction transaction = null )
        {
            Log( $"Error during transaction: {transactionException.Message}" );
            if ( transactionException != null )
            {
                try
                {
                    transaction.Rollback();
                }
                catch ( Exception rollbackException )
                {
                    Log( $"Error during transaction rollback {rollbackException.Message}" );
                }
            }
        }

        protected bool CheckOpenConnection()
        {
            if ( Connection != null )
            {
                if ( Connection.State != ConnectionState.Open )
                {
                    Connection.Open();
                }
                return Connection.State == ConnectionState.Open;
            }
            return false;
        }

        protected void CompleteTransaction()
        {
            if ( Transaction != null && AllowCommit )
            { // if there is an active transaction on this repository then try to commit it.
                try
                {
                    Transaction.Commit();
                }
                catch ( Exception transactionException )
                {
                    Log( transactionException.Message );
                    try
                    {
                        Transaction.Rollback();
                    }
                    catch ( Exception rollbackException )
                    {
                        Log( rollbackException.Message );
                    }
                    throw transactionException;
                }
                Transaction.Dispose();
                Transaction = null;
            }
        }

        protected void Log( string message )
        {   // I imagine that we will have some sort of log for this eventually.
            // Here is a placeholder until that is fully defined.
            System.Diagnostics.Debug.WriteLine( message );
        }

        public void Dispose()
        {
            CompleteTransaction();
            Transaction = null; // regardless.
            if ( !m_recievedOpen )
            {   // If this repository opened the connection then we must close it.
                // Otherwise leave it open for the caller who gave us the connection
                // to close when they are ready.
                if ( Connection.State != ConnectionState.Closed ) Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
