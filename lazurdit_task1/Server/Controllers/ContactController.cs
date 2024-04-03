using lazurdit_task1.Server.Data;
using lazurdit_task1.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lazurdit_task1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext _dataContext;
       public ContactController(DataContext context) {

            _dataContext= context;
        }

        [HttpGet]
        public async Task <ActionResult<List<Contact>>> GetContacts()
        {
            var contacts =await _dataContext.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Contact>>> GetSingleContact(int id )
        {   
            var contact = _dataContext.Contacts.FirstOrDefault(h => h.Id == id);
            if(contact == null)
            {
                return NotFound("There is no contact with this id ");
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact) 
        {
            _dataContext.Contacts.Add(contact);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Contact>>> UpdateContact(Contact hero, int id)
        {
            var dbHero = await _dataContext.Contacts
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbHero == null)
                return NotFound("Sorry, but no hero for you. :/");

            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Email = hero.Email;
            dbHero.PhoneNumber = hero.PhoneNumber;

            await _dataContext.SaveChangesAsync();

            return Ok(await GetDbContact());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Contact>>> DeleteContact(int id)
        {
            try
            {
                var dbHero = await _dataContext.Contacts.FirstOrDefaultAsync(sh => sh.Id == id);
                if (dbHero == null)
                    return NotFound("Sorry, but no hero for you. :/");

                _dataContext.Contacts.Remove(dbHero);
                await _dataContext.SaveChangesAsync();

                return Ok(await GetDbContact());
            }
            catch (DbUpdateException ex)
            {
                // Handle database update errors
                // Log the exception or provide an appropriate error response
                return StatusCode(500, "An error occurred while deleting the contact.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception or provide an appropriate error response
                return StatusCode(500, "An error occurred while deleting the contact.");
            }
        }
        private async Task<List<Contact>> GetDbContact()
        {
            return await _dataContext.Contacts.ToListAsync();
        }




    }
}
