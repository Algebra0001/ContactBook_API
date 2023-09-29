using Microsoft.AspNetCore.Identity;
using Model.APi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.Services
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUp signUp, string role);
        Task<string> LoginAsync(SignInModel signIn);
    }
}
