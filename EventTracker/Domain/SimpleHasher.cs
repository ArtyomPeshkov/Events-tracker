using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SimpleHasher : IHasher
    {
        public string Hash(string s)
        {
            return s.GetHashCode().ToString();
        }
    }
}
