using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order and Name must not be same.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}