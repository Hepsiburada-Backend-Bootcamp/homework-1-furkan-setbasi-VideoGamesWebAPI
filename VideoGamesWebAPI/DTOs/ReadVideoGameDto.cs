using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGamesWebAPI.DTOs {
  public class ReadVideoGameDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Category { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }
  }
}
