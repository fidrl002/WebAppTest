using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models;

public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    [Display(Name = "Item Name")]
    [StringLength(150, ErrorMessage = "The item name must be less than 150 characters")]
    public string ItemName { get; set; } = null!;

    [Required]
    [Display(Name = "Description")]
    public string ItemDescription { get; set; } = null!;

    [Required]
    [Display(Name = "Price")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal ItemCost { get; set; }

    [Required]
    [Display(Name = "Image")]
    public string ItemImage { get; set; } = null!;

    [Display(Name = "Category ID")]
    public int CategoryId { get; set; }

    public virtual ItemCategory Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
