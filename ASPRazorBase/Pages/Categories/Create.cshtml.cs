using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASPRazorBase.Data;
using ASPRazorBase.Models;

namespace ASPRazorBase.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ASPRazorBase.Data.ApplicationDbContext _context;

        public CreateModel(ASPRazorBase.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully";

            return RedirectToPage("./Index");
        }
    }
}
