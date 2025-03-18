using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        List<AddressBookDTO> GetAllContactsRL();
        AddressBookDTO GetContactByIdRL(int Id);
        AddressBookDTO AddContactRL(AddressBookDTO addressBookDTO);
        AddressBookDTO UpdateContactRL(int Id, AddressBookDTO addressBookDTO);
        bool DeleteContactRL(int Id);
    }
}