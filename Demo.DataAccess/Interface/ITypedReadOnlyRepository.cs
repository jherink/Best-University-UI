using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataAccess
{
    public interface ITypedReadOnlyRepository<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Get the item using the following key.
        /// </summary>
        /// <param name="key">The key to the object.</param>
        /// <returns>The associated object value.</returns>
        TValue Read( TKey key );

        /// <summary>
        /// Get all items for the repository.
        /// </summary>
        /// <returns>The repository items.</returns>
        IEnumerable<TValue> ReadAll();
    }
}
