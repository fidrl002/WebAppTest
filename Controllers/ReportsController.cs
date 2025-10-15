using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;

namespace WebAppTest.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public ReportsController(AmazonOrders2025Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var yearsList = _context.CustomerOrders
                .Select(co => co.OrderDate.Year)
                .Distinct()
                .OrderByDescending(co => co)
                .ToList();

            return View("AnnualSalesReport", new SelectList(yearsList));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int Year)
        {
            if (Year > 0)
            {
                var totalItems = _context.ItemsInOrders
                    .Where(i => i.OrderNumberNavigation.OrderDate.Year == Year)
                    .GroupBy(i => new { i.OrderNumberNavigation.OrderDate.Year, i.OrderNumberNavigation.OrderDate.Month })
                    .Select(group => new
                    {
                        year = group.Key.Year,
                        monthNo = group.Key.Month,
                        monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                        totalItems = group.Sum(i => i.NumberOf),
                        totalSales = group.Sum(i => i.TotalItemCost)
                    })
                    .OrderBy(data => data.monthNo);
                return Json(totalItems);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
