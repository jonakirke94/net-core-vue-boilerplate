using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crochet.Infrastructure.Interfaces
{
    public interface ICurrentUserAccessor
    {
        string GetCurrentUsername();
    }
}

