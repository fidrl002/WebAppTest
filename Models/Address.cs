using System;
using System.Collections.Generic;

namespace WebAppTest.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string AddressLine { get; set; } = null!;

    public string Suburb { get; set; } = null!;

    public string Postcode { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
