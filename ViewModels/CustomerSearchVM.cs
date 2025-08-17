using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppTest.Models;

namespace WebAppTest.ViewModels
{
    public class CustomerSearchVM
    {
        public string SearchText { get; set; }

        public string Suburb { get; set; }

        public SelectList SuburbList { get; set; }

        public List<Customer> CustomerList { get; set; }

        public List<string> NameList { get; set; }
    }
}
