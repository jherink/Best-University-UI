using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Demo.DataAccess
{
    public interface IRepository : IReadOnlyRepository
    {
        bool Insert( IDbCommand cmd );

        bool Update( IDbCommand cmd );
    }
}
