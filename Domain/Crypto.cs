using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Domain
{
  public class Crypto
  {
    public Guid CryptoId { get; set; }

    public string CryptoName { get; set; }

    public List<CryptoHistory> History { get; set; }
  }
}