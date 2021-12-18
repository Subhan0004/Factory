using Factory.Core;
using Factory.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Identity
{
    public class CustomPasswordHasher: IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }

        private string HashPassword(string value)
        {
            return SecurityHelper.ComputeSha256Hash(value);
        }
    }
}
