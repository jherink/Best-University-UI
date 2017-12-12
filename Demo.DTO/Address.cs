using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO
{
    public interface IAddress
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string State { get; set; }
        int ZipCode { get; set; }
    }

    public sealed class Address : IAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }
}
