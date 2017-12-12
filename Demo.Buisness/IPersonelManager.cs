using Demo.DTO;
using System.Collections.Generic;

namespace Demo.Buisness
{    public interface IPersonelManager<T> where T : IPerson
    {
        IEnumerable<T> Search( string firstName, string lastName );

        T Get( int ID );

        T Create( T person );
    }
}
