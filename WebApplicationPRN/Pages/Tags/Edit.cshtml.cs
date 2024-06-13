using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

namespace WebApplicationPRN.Pages.Tags
{
    public class EditModel : PageModel
    {
        private readonly ITagSvc _tagSvc;


        public EditModel(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagSvc.GetTagByIdAsync((int)id);
            if (tag == null)
            {
                return NotFound();
            }
            Tag = tag;
            return Page();
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
                await _tagSvc.UpdateTagAsync(Tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(Tag.TagId))
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

        private bool TagExists(int id)
        {
            return _tagSvc.GetTagByIdAsync(id) != null;
        }
    }
}
