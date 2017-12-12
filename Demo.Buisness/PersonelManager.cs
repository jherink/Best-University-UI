using Demo.Buisness.Validators;
using Demo.DTO;
using System;
using System.Collections.Generic;

namespace Demo.Buisness
{
    public abstract class PersonelManager<T> : IPersonelManager<T> where T : IPerson
    {
        protected string CreateNewEmailAddress( T person )
        {
            return $"{person.FirstName}.{person.LastName}@bestu.edu".ToLower();
        }

        #region Template Pattern

        public T Create( T person )
        {
            if ( new AddressValidator().Validate( person.Address ) )
            {
                person.Email = CreateNewEmailAddress( person );
                return CreateImpl( person );
            }
            else
            {
                throw new InvalidOperationException( "Invalid Address!!" );
            }
        }

        protected abstract T CreateImpl( T person );
#endregion

        public abstract T Get( int ID );

        public abstract IEnumerable<T> Search( string firstName, string lastName );
    }
}
