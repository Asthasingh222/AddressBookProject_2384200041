
using RepositoryLayer.Context;
using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace RepositoryLayer.Service
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly IDistributedCache _cache;
        private readonly AddressBookContext _context;
        private readonly IMapper _mapper;
        public AddressBookRL(AddressBookContext context, IMapper mapper, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public List<AddressBookDTO> GetAllContactsRL()
        {
            string cacheKey = "all_contacts";
            string serializedContacts = _cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(serializedContacts))
            {
                return JsonConvert.DeserializeObject<List<AddressBookDTO>>(serializedContacts);
            }

            var contacts = _context.AddressBookEntries.ToList();
            var contactDTOs = _mapper.Map<List<AddressBookDTO>>(contacts);

            serializedContacts = JsonConvert.SerializeObject(contactDTOs);
            _cache.SetString(cacheKey, serializedContacts, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Cache expiry 10 min
            });

            return contactDTOs;
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
                throw new Exception("Contact already exists");
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

            // Remove old cache since data is updated
            _cache.Remove("all_contacts");

            return _mapper.Map<AddressBookDTO>(newContact);
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