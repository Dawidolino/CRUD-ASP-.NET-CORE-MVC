using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using netpc.Data;
using netpc.Models;

namespace netpc.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ContactsDbContext _dbContext;


        public IndexModel(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Contacts = await _dbContext.Contacts.ToListAsync();
        }

        public List<Contact> Contacts { get; private set; }

    }
}