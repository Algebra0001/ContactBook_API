using Core.API.Services;
using DATA.API;
using Microsoft.EntityFrameworkCore;
using Model.APi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactAPIContext _dbContext;

        public ContactRepository(ContactAPIContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException();
        }

        #region AddContactAsync
        public async Task<Contacts> AddContactAsync(Contacts newContactEntity)
        {
            await _dbContext.Contacts.AddAsync(newContactEntity);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return newContactEntity;
            }
            else
            {
                throw new Exception("Contact Not Added Successfully");
            }
        }
        #endregion

        #region GetAllContactAsync
        public async Task<IEnumerable<Contacts>> GetAllContactAsync()
        {
            return await _dbContext.Contacts.ToListAsync();

        }
        #endregion

        #region GetContactAsync
        public async Task<Contacts> GetContactAsync(int ContactId)
        {

            return await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == ContactId);
        }
        #endregion

        #region SearchContactAsync
        public async Task<IEnumerable<Contacts>> SearchContactAsync(string Firstname, string Lastname)
        {
            if (string.IsNullOrEmpty(Firstname) && string.IsNullOrEmpty(Lastname))
            {
                return await GetAllContactAsync();
            }
            var loadSearchContact = await _dbContext.Contacts.Where(item =>
            item.FirstName.ToLower().Contains(Firstname.ToLower().Trim()) &&
            item.LastName.ToLower().Contains(Lastname.ToLower().Trim())).ToListAsync();
            return loadSearchContact;
        }
        #endregion

        #region UpdateContactAsync
        public async Task<int> UpdateContactAsync(Contacts updateContactEntity)
        {
            _dbContext.Contacts.Update(updateContactEntity);
            var affectedresult = await _dbContext.SaveChangesAsync();
            return affectedresult;
        }
        #endregion
    }
}
