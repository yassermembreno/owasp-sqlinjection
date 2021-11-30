using System;
namespace owasp_sqlinjection.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Names { get; set; }
        public string? Lastnames { get; set; }
        public string? DNI { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

