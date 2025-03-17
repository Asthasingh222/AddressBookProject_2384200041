using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;

namespace RepositoryLayer.Service
{
    public class AddressBookRL
    {
        private readonly AddressBookContext _context;
        public AddressBookRL(AddressBookContext context)
        {
            _context = context;
        }
    }
}