using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Buisness.Validators
{
    public interface IValidator<T>
    {
        bool Validate( T item );
    }
}
