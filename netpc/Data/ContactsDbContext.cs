using Microsoft.EntityFrameworkCore;
using netpc.Models;

namespace netpc.Data
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { } //constructor for accessing DB

        public DbSet<Contact> Contacts { get; set; }
    }
}
