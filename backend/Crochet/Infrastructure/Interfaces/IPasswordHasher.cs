using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crochet.Infrastructure.Interfaces
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
    }
}
