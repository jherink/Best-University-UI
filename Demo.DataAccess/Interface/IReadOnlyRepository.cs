using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Demo.DataAccess
{
    public interface IReadOnlyRepository : IDisposable
    {
        IReadOnlyDictionary<string, object> Read( IDbCommand cmd );

        IEnumerable<IReadOnlyDictionary<string, object>> ReadManyCommand( IDbCommand cmd );
    }
}
