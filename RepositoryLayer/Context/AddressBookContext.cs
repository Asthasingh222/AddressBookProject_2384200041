using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options) { }
        public DbSet<AddressBookEntry> AddressBookEntries { get; set; }
        public DbSet<User> Users { get; set; }
    }
}