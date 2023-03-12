using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Services
{
    public interface IUserUtility
    {
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    }
}