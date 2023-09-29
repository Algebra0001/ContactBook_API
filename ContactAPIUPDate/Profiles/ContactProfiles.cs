using Model.APi.Entities;
using Model.APi.Model;
using AutoMapper;


namespace ContactAPIUPDate.Profiles
{
    public class ContactProfiles : Profile
    {
        public ContactProfiles()
        {
            CreateMap<Contacts, ContactDTO>();
            CreateMap<CreateContactDTO, Contacts>();
        }
       
    }
}
