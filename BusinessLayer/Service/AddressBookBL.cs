using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly IAddressBookRL _addressBookRL;

        public AddressBookBL(IAddressBookRL addressBookRL)
        {
            _addressBookRL = addressBookRL;
        }

        public List<AddressBookDTO> GetAllContactsBL()
        {
            return _addressBookRL.GetAllContactsRL();
        }

        public AddressBookDTO GetContactByIdBL(int Id)
        {
            var contact = _addressBookRL.GetContactByIdRL(Id);
            return contact;
        }

        public AddressBookDTO AddContactBL(AddressBookDTO addressBookDTO)
        {
            try
            {
                var contact = _addressBookRL.AddContactRL(addressBookDTO);
                return contact;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AddressBookDTO UpdateContactBL(int Id, AddressBookDTO addressBookDTO)
        {
            var result = _addressBookRL.UpdateContactRL(Id, addressBookDTO);
            return result;
        }

        public bool DeleteContactBL(int Id)
        {
            return _addressBookRL.DeleteContactRL(Id);
        }
    }
}