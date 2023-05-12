using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using netpc.Data;

namespace netpc.Pages.Contacts
{
    public class ListModel : PageModel
    {
        private readonly ContactsDbContext dbContext;

        public List<netpc.Models.Contact> Contacts { get; set; }

        public ListModel(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task OnGet()
        {
            Contacts = await dbContext.Contacts.ToListAsync();
        }
    }
}
