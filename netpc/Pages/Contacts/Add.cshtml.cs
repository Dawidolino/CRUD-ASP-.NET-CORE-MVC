using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using netpc.Data;
using netpc.Models;
using System.Text.RegularExpressions;

namespace netpc.Pages.Contacts
{
    public class AddModel : PageModel
    {
        private readonly ContactsDbContext dbContext;

        public AddModel(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [BindProperty]
        public AddContactViewModel AddContactRequest { get; set; }

        
        public async Task OnPost()
        {
            //convert viewmodel to domainmodel(Contacts.cs) for the purpose of dbContext
            var contactDomainModel = new Contact
            {
                FirstName = AddContactRequest.FirstName,
                LastName = AddContactRequest.LastName,
                Email = AddContactRequest.Email,
                Password = AddContactRequest.Password,
                DateOfBirth = AddContactRequest.DateOfBirth,
                Category = AddContactRequest.Category,
                Subcategory = AddContactRequest.Subcategory,
                PhoneNumber = AddContactRequest.PhoneNumber,

            };
            if (await dbContext.Contacts.AnyAsync(c => c.Email == AddContactRequest.Email))
            {
                ViewData["EmailInUseError"] = "Email jest ju¿ w u¿yciu";                
            }
            if (!Regex.IsMatch(AddContactRequest.Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z]).{8,}$"))
            {
                ViewData["PasswordComplexityError"] = "Has³o musi mieæ co najmniej 8 znaków, zawieraæ przynajmniej jedn¹ cyfrê, jedn¹ ma³¹ i jedn¹ wielk¹ literê oraz jeden znak specjalny.";                
            }
            if (!ModelState.IsValid)
            {
                return;
            }
            await dbContext.Contacts.AddAsync(contactDomainModel);
            await dbContext.SaveChangesAsync();

            ViewData["Message"] = "Kontakt dodany pomyœlnie";

        }
    }
}
