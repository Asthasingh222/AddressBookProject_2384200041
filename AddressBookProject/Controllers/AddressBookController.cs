using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using ModelLayer.Model;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Interface;

namespace AddressBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressBookController : ControllerBase
    {
        AddressBookContext _context;
        IAddressBookBL _addressBookBL;

        public AddressBookController(AddressBookContext context, IAddressBookBL addressBookBL)
        {
            _context = context;
            _addressBookBL = addressBookBL;
        }
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _addressBookBL.GetAllContactsBL();
            ResponseModel<List<AddressBookDTO>> response = new ResponseModel<List<AddressBookDTO>>();
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
            var result = _addressBookBL.GetContactByIdBL(Id);
            ResponseModel<AddressBookDTO> response = new ResponseModel<AddressBookDTO>();
            if (result == null)
            {
                response.Success = false;
                response.Message = "Contact by Id not found!";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = "Contact by Id retrieved successfully!";
            response.Data = result;
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddContact(AddressBookDTO addressBookDTO)
        {
            try
            {
                if (addressBookDTO == null)
                {
                    return BadRequest("AddressBookDTO is null.");
                }

                ResponseModel<AddressBookDTO> response = new ResponseModel<AddressBookDTO>();
                var result = _addressBookBL.AddContactBL(addressBookDTO);

                response.Success = true;
                response.Message = "Contact added successfully.";
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{Id}")]

        public IActionResult UpdateContact(int Id, AddressBookDTO addressBookDTO)
        {
            var result = _addressBookBL.UpdateContactBL(Id, addressBookDTO);
            ResponseModel<AddressBookDTO> response = new ResponseModel<AddressBookDTO>();
            if (result == null)
            {
                response.Success = false;
                response.Message = "Contact not found!";
                response.Data = null;
                return NotFound(response);

            }

            response.Success = true;
            response.Message = "Contact updated successfully!";
            response.Data = result;
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteContact(int Id)
        {
            ResponseModel<string> response = new ResponseModel<string>();
            var result = _addressBookBL.DeleteContactBL(Id);
            if (result == null)
            {
                response.Success = false;
                response.Message = "Contact not found!";
                response.Data = null;
                return NotFound(response);
            }

            response.Success = true;
            response.Message = "Contact deleted successfully!";
            response.Data = $"Id: {Id}";
            return Ok(response);
        }
    }
}