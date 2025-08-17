using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [NotMapped]
    [Display(Name = "Customer Full Name")]
    public string FullName => FirstName + " " + LastName;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Main Phone Number")]
    public string MainPhoneNumber { get; set; } = null!;

    [Display(Name = "Secondary Phone Number")]
    public string? SecondaryPhoneNumber { get; set; }

    [NotMapped]
    [Display(Name = "Contact Number")]
    public string ContactNumber
    {
        get
        {
            var contact = "";
            if (!string.IsNullOrWhiteSpace(MainPhoneNumber)) { contact = MainPhoneNumber; }
            if (!string.IsNullOrWhiteSpace(SecondaryPhoneNumber)) { contact += (contact.Length > 0 ? "<br />" : "") + SecondaryPhoneNumber; } // stack if value in MainPhoneNumber
            return contact;
        }
    }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    [NotMapped]
    [Display(Name = "Address")]
    public string FullAddress
    {
        get
        {
            var address = "";
            if (!string.IsNullOrWhiteSpace(Address.AddressLine)) { address = Address.AddressLine; }
            if (!string.IsNullOrWhiteSpace(Address.Suburb)) { address += (address.Length > 0 ? "<br />" : "") + Address.Suburb;  }
            if (!string.IsNullOrWhiteSpace(Address.Postcode)) { address += ", " + Address.Postcode; }
            if (!string.IsNullOrWhiteSpace(Address.Region)) { address += " " + Address.Region; }
            return address;
        }
    }

    public virtual ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
