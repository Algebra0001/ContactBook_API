using Model.APi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Services
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contacts>> GetAllContactAsync();
        Task<Contacts> GetContactAsync(int ContactId);
        Task<IEnumerable<Contacts>> SearchContactAsync(string firstname, string lastname);

        Task<Contacts> AddContactAsync(Contacts contacts);
        Task<int> UpdateContactAsync(Contacts updateContactEntity);
    }
}
