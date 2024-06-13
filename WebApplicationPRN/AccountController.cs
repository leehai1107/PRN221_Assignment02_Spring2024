using Microsoft.AspNetCore.Mvc;
namespace LeChiHaiRazorPages;
public class AccountController : Controller
{
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToPage("/Index");
    }
}
