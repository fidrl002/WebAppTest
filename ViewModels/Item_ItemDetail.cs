using System.ComponentModel.DataAnnotations;
using WebAppTest.Models;

namespace WebAppTest.ViewModels
{
    public class Item_ItemDetail
    {
        public Item TheItem { get; set; }

        public int ReviewCount { get; set; }

        [DisplayFormat(DataFormatString="{0:N2}")]
        public double AvgRating { get; set; }
    }
}
