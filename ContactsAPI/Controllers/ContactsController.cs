using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext _dbcontext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet("GetAllContacts")]
        public async Task<IActionResult> GetAllContactsAsync()
        {
            return  Ok(await _dbcontext.Contacts.ToListAsync());
        }
        [HttpGet("GetContactById/{Id:guid}")]
        public async Task<IActionResult> GetContactsAsync(Guid Id)
        {
            var contacts = await _dbcontext.Contacts.FindAsync(Id);
            if (contacts == null)
            {
                return NotFound();
            }
            return Ok(contacts);
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContacts(AddContactsRequest addContactsRequest)
        {
            var contacts = new Contacts() { 
                
                Id=Guid.NewGuid(),
                Name=addContactsRequest.Name,
                Email=addContactsRequest.Email,
                PhoneNumber = addContactsRequest.PhoneNumber,
                Address = addContactsRequest.Address 
            };
            await _dbcontext.AddAsync(contacts);
            await _dbcontext.SaveChangesAsync();
            return Ok(contacts);
        }
        [HttpPut("UpdateContact/{Id:guid}")]
        public async Task<IActionResult> UpdateContacts(Guid Id,UpdateContactsRequest updateContactsRequest)
        {
            var contacts = await _dbcontext.Contacts.FindAsync(Id);
            if (contacts != null)
            {
                contacts.Name = updateContactsRequest.Name;
                contacts.Email = updateContactsRequest.Email;
                contacts.PhoneNumber = updateContactsRequest.PhoneNumber;
                contacts.Address = updateContactsRequest.Address;
                await _dbcontext.SaveChangesAsync();
                return Ok(contacts);
            }
            return NotFound();
        }
        [HttpDelete("DeleteContact/{Id:guid}")]
        public async Task<IActionResult> DeleteContacts(Guid Id, DeleteContactsRequest deleteContactsRequest)
        {
            var contacts = await _dbcontext.Contacts.FindAsync(Id);
            if (contacts != null)
            {
                _dbcontext.Remove(contacts);
                await _dbcontext.SaveChangesAsync();
                return Ok(contacts);
            }
            return NotFound();
        }
    }
}
