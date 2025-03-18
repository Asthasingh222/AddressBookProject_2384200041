
using RepositoryLayer.Context;
using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Service
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly AddressBookContext _context;
        private readonly IMapper _mapper;
        public AddressBookRL(AddressBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AddressBookDTO> GetAllContactsRL()
        {
            var contacts = _context.AddressBookEntries.ToList();
            return _mapper.Map<List<AddressBookDTO>>(contacts);
        }

        public AddressBookDTO GetContactByIdRL(int Id)
        {
            var contact = _context.AddressBookEntries.Find(Id);
            return _mapper.Map<AddressBookDTO>(contact);
        }

        public AddressBookDTO AddContactRL(AddressBookDTO addressBookDTO)
        {
            var contact = _context.AddressBookEntries.FirstOrDefault(addBook => addBook.Email == addressBookDTO.Email);
            if (contact != null)
            {
                throw new Exception("Contact already Exist");
            }
            var newContact = new AddressBookEntry
            {
                Name = addressBookDTO.Name,
                Phone = addressBookDTO.Phone,
                Email = addressBookDTO.Email,
                UserId = addressBookDTO.UserId
            };
            _context.AddressBookEntries.Add(newContact);
            _context.SaveChanges();

            return new AddressBookDTO
            {
                Name = addressBookDTO.Name,
                Phone = addressBookDTO.Phone,
                Email = addressBookDTO.Email,
                UserId = addressBookDTO.UserId
            };
        }

        public AddressBookDTO UpdateContactRL(int Id, AddressBookDTO addressBookDTO)
        {
            var existingContact = _context.AddressBookEntries.FirstOrDefault(addBook => addBook.Id == Id);
            if (existingContact == null)
                return null;

            existingContact.Name = addressBookDTO.Name;
            existingContact.Phone = addressBookDTO.Phone;
            existingContact.Email = addressBookDTO.Email;
            existingContact.UserId = addressBookDTO.UserId;

            _context.SaveChanges();

            return new AddressBookDTO
            {
                Name = addressBookDTO.Name,
                Phone = addressBookDTO.Phone,
                Email = addressBookDTO.Email,
                UserId = addressBookDTO.UserId
            };
        }

        public bool DeleteContactRL(int Id)
        {
            var contact = _context.AddressBookEntries.FirstOrDefault(addBook => addBook.Id == Id);
            if (contact == null)
                return false;

            _context.AddressBookEntries.Remove(contact);
            _context.SaveChanges();

            return true;
        }
    }
}