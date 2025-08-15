using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.ViewModels;

namespace WebAppTest.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public ItemsController(AmazonOrders2025Context context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(ItemSearchViewModel vm, string? sortOrder)
        {
            #region CategoriesQuery
            var Categories = await _context.ItemCategories
                .Where(c => c.ParentCategory == null)
                .OrderBy(c => c.CategoryName)
                .Select(p => new
                {
                    p.CategoryId,
                    p.CategoryName
                })
                .ToListAsync();

            vm.CategoryList = new SelectList(Categories,
                                    nameof(ItemCategory.CategoryId),
                                    nameof(ItemCategory.CategoryName),
                                    vm.CategoryId); // keeps the category value in the view
            #endregion

            #region ItemQuery

            var amazonOrders2025Context = _context.Items
                .Include(i => i.Category) // Include() performs join from FK -> PK
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.SearchText)) // determine if search text is empty
            {
                amazonOrders2025Context = amazonOrders2025Context
                    .Where(i => i.ItemName.Contains(vm.SearchText));
            }

            if (vm.CategoryId.HasValue)
            {
                amazonOrders2025Context = amazonOrders2025Context
                    .Where(i => i.Category.ParentCategoryId == vm.CategoryId);
            }

            ViewBag.sortOrder = sortOrder;
            switch (sortOrder)
            {
                case "name_desc":
                    amazonOrders2025Context = amazonOrders2025Context.OrderByDescending(i => i.ItemName);
                    break;

                case "price_asc":
                    amazonOrders2025Context = amazonOrders2025Context.OrderBy(i => i.ItemCost);
                    break;

                case "price_desc":
                    amazonOrders2025Context = amazonOrders2025Context.OrderByDescending(i => i.ItemCost);
                    break;

                default:
                    amazonOrders2025Context = amazonOrders2025Context.OrderBy(i => i.ItemName);
                    break;
            }

            vm.ItemList = await amazonOrders2025Context
                .Select(i => new Item_ItemDetail
                {
                    TheItem = i,
                    ReviewCount = i.Reviews.Count,
                    AvgRating = i.Reviews.Count > 0 ? i.Reviews.Average(r => r.Rating) : 0
                })
                .ToListAsync();

            ViewBag.itemCount = vm.ItemList.Count;
            ViewBag.plural = (ViewBag.itemCount == 1) ? "result:" : "results:";
            #endregion

            return View(vm);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
