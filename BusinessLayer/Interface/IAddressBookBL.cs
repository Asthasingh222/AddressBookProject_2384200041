using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IAddressBookBL
    {
        List<AddressBookDTO> GetAllContactsBL();
        AddressBookDTO GetContactByIdBL(int Id);
        AddressBookDTO AddContactBL(AddressBookDTO addressBookDTO);
        AddressBookDTO UpdateContactBL(int Id, AddressBookDTO addressBookDTO);
        bool DeleteContactBL(int Id);
    }
}