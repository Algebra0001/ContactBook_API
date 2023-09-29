using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.APi.Entities;
//using Model.API.Entities;

namespace DATA.API
{
    public class ContactAPIContext : IdentityDbContext
    {
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public ContactAPIContext(DbContextOptions<ContactAPIContext> option): base(option) { }
        
    }
}