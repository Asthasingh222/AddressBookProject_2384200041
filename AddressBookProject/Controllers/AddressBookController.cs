using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using ModelLayer.Model;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressBookController : ControllerBase
    {
        AddressBookContext _context;

        public AddressBookController(AddressBookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _context.AddressBookEntries.ToList();
            ResponseModel<List<AddressBookEntry>> response = new ResponseModel<List<AddressBookEntry>>();
            if (contacts == null)
            {
                response.Success = false;
                response.Message = "Contacts not found!";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = "All Contacts retrieved successfully!";
            response.Data = contacts;
            return Ok(response);
        }


        [HttpGet("{Id}")]
        public IActionResult GetContactsByID(int Id)
        {
            var contact = _context.AddressBookEntries.Find(Id);
            ResponseModel<AddressBookEntry> response = new ResponseModel<AddressBookEntry>();
            if (contact == null)
            {
                response.Success = false;
                response.Message = "Contact by Id not found!";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = "Contact by Id retrieved successfully!";
            response.Data = contact;
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddContact(AddressBookDTO addressBookDTO)
        {
            var contact = new AddressBookEntry
            {
                Name = addressBookDTO.Name,
                Phone = addressBookDTO.Phone,
                Email = addressBookDTO.Email,
                UserId = addressBookDTO.UserId
            };
            _context.AddressBookEntries.Add(contact);
            _context.SaveChanges();

            ResponseModel<AddressBookEntry> response = new ResponseModel<AddressBookEntry>();
            response.Success = true;
            response.Message = "Contact added successfully!";
            response.Data = contact;
            return Ok(response);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateContact(int Id, AddressBookDTO addressBookDTO)
        {
            var existingContact = _context.AddressBookEntries.FirstOrDefault(addBook => addBook.Id == Id);
            if (existingContact == null)
                return null;

            existingContact.Name = addressBookDTO.Name;
            existingContact.Phone = addressBookDTO.Phone;
            existingContact.Email = addressBookDTO.Email;
            existingContact.UserId = addressBookDTO.UserId;

            _context.SaveChanges();

            ResponseModel<AddressBookEntry> response = new ResponseModel<AddressBookEntry>();
            response.Success = true;
            response.Message = "Contact updated successfully!";
            response.Data = existingContact;
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteContact(int Id)
        {
            ResponseModel<AddressBookEntry> response = new ResponseModel<AddressBookEntry>();
            var contact = _context.AddressBookEntries.FirstOrDefault(addBook => addBook.Id == Id);
            if (contact == null)
            {
                response.Success = false;
                response.Message = "Contact not found!";
                response.Data = null;
                return NotFound(response);
            }

            _context.AddressBookEntries.Remove(contact);
            _context.SaveChanges();
            response.Success = true;
            response.Message = "Contact deleted successfully!";
            response.Data = contact;
            return Ok(response);
        }
    }
}