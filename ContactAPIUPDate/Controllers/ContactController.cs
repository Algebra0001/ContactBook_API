using AutoMapper;
using Core.API.Repository;
using Core.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.APi.Entities;
using Model.APi.Model;
using System.Data;

namespace ContactAPIUPDate.Controllers
{
    [Route("user")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, IMapper mapper)
        {

            _contactRepository = contactRepository ?? throw new ArgumentNullException();
            _mapper = mapper ?? throw new ArgumentNullException();
        }
        [Authorize]
        [HttpGet("contact")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetAllContact(int pageNumber)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;

            var defaultPageSize = 5f;
            var allContact = await _contactRepository.GetAllContactAsync();
            var totalItems = allContact.Count();
            var pageCount = Math.Ceiling(totalItems / defaultPageSize);
            if (pageCount > pageNumber)
            {
                return NoContent();
            }
            var items = allContact.Skip((pageNumber - 1) * (int)defaultPageSize)
                .Take((int)defaultPageSize).ToList();

            var result = new
            {
                TotalItems = totalItems,
                Data = _mapper.Map<IEnumerable<ContactDTO>>(items),
                CurrentPage = pageNumber,
                PageSize = (int)pageCount
            };
            return Ok(result);
        }
        [Authorize]
        [HttpGet("contact/{id}")]
        public async Task<ActionResult<ContactDTO>> GetSingleContactById(int id)
        {
            var singleContactBy = await _contactRepository.GetContactAsync(id);
            if (singleContactBy == null)
            {
                return NotFound(singleContactBy);
            }
            return Ok(singleContactBy);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("contact/search")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> SearchContact(string? Firstname, string? Lastname)
        {
            var search = await _contactRepository.SearchContactAsync(Firstname, Lastname);
            if (search == null) { return NotFound(search); }
            return Ok(search);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Contacts>> AddContact(CreateContactDTO createContact)
        {
            var mapitem = new Contacts()
            {
                FirstName = createContact.FirstName,
                LastName = createContact.LastName,
                Email = createContact.Emails,
                Address = createContact.Adderess,
                PhoneNumbers = createContact.PhoneNumber,
                WebsiteUrl = createContact.WebSiteUrl,
                Image = createContact.Image,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contactAdd = await _contactRepository.AddContactAsync(mapitem);
            return Ok(contactAdd);

        }
        [Authorize(Roles = "USER")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateContact(ContactUpdateDTO contactUpdateDTO, int id)
        {
            var mapUpdateItem = new Contacts()
            {
                Id = id,
                FirstName = contactUpdateDTO.FirstName,
                LastName = contactUpdateDTO.LastName,
                Email = contactUpdateDTO.Emails,
                Address = contactUpdateDTO.Adderess,
                PhoneNumbers = contactUpdateDTO.PhoneNumber,
                WebsiteUrl = contactUpdateDTO.WebSiteUrl,
                Image = contactUpdateDTO.Image,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updateItem = await _contactRepository.UpdateContactAsync(mapUpdateItem);
            if (updateItem > 0)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}
