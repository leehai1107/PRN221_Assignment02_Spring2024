using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

namespace WebApplicationPRN.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ICategorySvc _categorySvc;

        public EditModel(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Index");

            }
            else
            {


                if (id == null)
                {
                    return NotFound();
                }

                var category = await _categorySvc.GetCategoryByIdAsync((short)id);
                if (category == null)
                {
                    return NotFound();
                }
                Category = category;
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                await _categorySvc.UpdateCategoryAsync(Category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(short id)
        {
            return _categorySvc.GetCategoryByIdAsync(id) != null;
        }
    }
}
