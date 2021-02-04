using System;
using System.Collections.Generic;

namespace Domain
{
    public class Crypto
    {
        public Guid CryptoId { get; set; }

        public string CryptoName { get; set; }

        public List<CryptoHistory> History { get; set; }
    }
}