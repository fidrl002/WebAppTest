using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppTest.ViewModels
{
    public class ItemSearchViewModel
    {
        public string SearchText { get; set; }

        public int? CategoryId { get; set; }

        public SelectList CategoryList { get; set; }

        public List<Item_ItemDetail> ItemList { get; set; } // updated to include ViewModel, which contains the Item
    }
}
