using Demo.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Buisness.Validators
{
    class AddressValidator : IValidator<IAddress>
    {
        public bool Validate( IAddress item )
        {
            // TODO: Validate that state is actually a state.
            return !( item == default( IAddress ) ||
                      string.IsNullOrWhiteSpace( item.Address1 ) ||
                      string.IsNullOrWhiteSpace( item.Address2 ) ||
                      //string.IsNullOrWhiteSpace( item.State ) || // Per User: Don't check state
                    ( item.ZipCode < 10000 || item.ZipCode > 99999 ) );
        }
    }
}
