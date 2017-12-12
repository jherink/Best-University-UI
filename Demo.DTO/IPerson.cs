using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO
{
    public interface IPerson
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        DateTime DateOfBirth { get; set; }
        Address Address { get; set; }
        string PhoneNumber { get; set; }
    };
}
