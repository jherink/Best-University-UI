using System;
using System.Collections.Generic;
using System.Data;

namespace Demo.DataAccess
{
    /// <summary>
    /// Interface for repository objects.
    /// </summary>
    /// <typeparam name="TKey">The key type of the object.</typeparam>
    /// <typeparam name="TValue">The record type of the repository.</typeparam>
    public interface ITypedRepository<TKey, TValue> : IRepository, ITypedReadOnlyRepository<TKey, TValue>, IDisposable
    {
        /// <summary>
        /// Insert the specified item into the table with a new key.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        /// <returns>The key of the record if inserted.</returns>
        TKey Insert( TValue item );

        /// <summary>
        /// Read a collection of items based on the supplied command.
        /// This repository will supply connection & transaction 
        /// information.
        /// </summary>
        /// <param name="command">The repository command.</param>
        /// <returns>A set of qualified objects.</returns>
        //IEnumerable<TValue> ReadMany( IDbCommand command );

        /// <summary>
        /// Update the record matching this item.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="item">The item to update the record with.</param>
        /// <returns></returns>
        bool Update( TKey key, TValue item );

        /// <summary>
        /// Delete the item records.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns></returns>
        bool Delete( TValue item );

        /// <summary>
        /// Delete the qualified records. Connection & Transaction
        /// information will be supplied by the repository. 
        /// </summary>
        /// <param name="command">The delete command to perform.</param>
        /// <returns>True if command executes successfully.</returns>
        bool DeleteMany( IDbCommand command );

        /// <summary>
        /// Create a new key for the specified item.
        /// </summary>
        /// <returns>A new key.</returns>
        TKey CreateKey( TValue item );

        /// <summary>
        /// Set the transaction to perform these operations under.
        /// </summary>
        /// <param name="transaction">The transaction to apply the operations under.</param>
        void SetTransaction( IDbTransaction transaction, bool allowCommit );

        /// <summary>
        /// Get the current transaction if there is one.
        /// </summary>
        /// <returns>The active transaction of the repository.</returns>
        IDbTransaction GetTransaction();

        /// <summary>
        /// Set the connection of the repository.
        /// </summary>
        /// <param name="connection">The ADO.NET connection.</param>
        void SetConnection( IDbConnection connection );

    }
}