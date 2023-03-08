using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Infrastructure;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages
{
    public class CartModel : PageModel
    {

        private IBookStoreRepository repo { get; set; }

        public CartModel (IBookStoreRepository temp)
        {
            repo = temp;
        }

        public Basket Basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Basket = HttpContext.Session.GetJson<Basket>("Basket") ?? new Basket();
        }
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            Basket = HttpContext.Session.GetJson<Basket>("Basket") ?? new Basket();

            Basket.AddItem(b, 1);

            HttpContext.Session.SetJson("Basket", Basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
