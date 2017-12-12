using System;

namespace Demo.DTO
{
    public sealed class Teacher : IPerson
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public string Title { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
