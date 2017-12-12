using System;

namespace Demo.DTO
{
    // Should mirror status in EnrollmentStatus Table
    public enum EnrollmentStatus
    {
        Active = 100,
        Graduated = 101,
        InActive = 102
    };

    public sealed class Student : IPerson
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MiddleName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
