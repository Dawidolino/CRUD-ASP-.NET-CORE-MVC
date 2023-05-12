using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using netpc.Data;
using netpc.Models;
using System.Text.RegularExpressions;

namespace netpc.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly ContactsDbContext dbContext;

        [BindProperty]
        public EditContactViewModel EditContactViewModel { get; set; }

        public EditModel(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;

        }   
        public async Task OnGet(int id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                //convert to viewModel
                EditContactViewModel = new EditContactViewModel()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    Password = contact.Password,
                    DateOfBirth = contact.DateOfBirth,
                    Category = contact.Category,
                    Subcategory = contact.Subcategory,
                    PhoneNumber = contact.PhoneNumber,
                };
            }
        }

        public async Task OnPostUpdate()
        {
            if (EditContactViewModel != null)
            {
                var existingContact = await dbContext.Contacts.FindAsync(EditContactViewModel.Id);

                if (existingContact != null)
                {
                    existingContact.FirstName = EditContactViewModel.FirstName;
                    existingContact.LastName = EditContactViewModel.LastName;
                    existingContact.Email = EditContactViewModel.Email;
                    existingContact.Password = EditContactViewModel.Password;
                    existingContact.DateOfBirth = EditContactViewModel.DateOfBirth;
                    existingContact.Category = EditContactViewModel.Category;
                    existingContact.Subcategory = EditContactViewModel.Subcategory;
                    existingContact.PhoneNumber = EditContactViewModel.PhoneNumber;
                }
                if (await dbContext.Contacts.AnyAsync(c => c.Email == EditContactViewModel.Email && c.Id != EditContactViewModel.Id))
                {
                    ViewData["EmailInUseError"] = "U�y�e� istniej�cego emaila, kt�ry jest przypisany do innego kontaktu!";
                }
                if (!Regex.IsMatch(EditContactViewModel.Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z]).{8,}$"))
                {
                    ViewData["PasswordComplexityError"] = "Nowe has�o nie spe�nia wymaga�! Popraw je ponownie w edycji. (Has�o musi mie� co najmniej 8 znak�w, zawiera� przynajmniej jedn� cyfr�, jedn� ma�� i jedn� wielk� liter� oraz jeden znak specjalny.)";
                }                
                await dbContext.SaveChangesAsync();

                ViewData["Message"] = "Kontakt zaktualizowany pomy�lnie";
            }

        }
        public async Task<IActionResult> OnPostDelete()
        {
            var existingContact = await dbContext.Contacts.FindAsync(EditContactViewModel.Id);
            if (existingContact != null)
            {
                dbContext.Contacts.Remove(existingContact);
                await dbContext.SaveChangesAsync();

                return RedirectToPage("/contacts/list");
            }
            ViewData["Message"] = "Kontakt usuni�ty pomy�lnie";

            return Page();
        }
    }
}
