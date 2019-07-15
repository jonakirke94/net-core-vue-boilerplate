using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crochet.Domain
{
    public class Person
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}
