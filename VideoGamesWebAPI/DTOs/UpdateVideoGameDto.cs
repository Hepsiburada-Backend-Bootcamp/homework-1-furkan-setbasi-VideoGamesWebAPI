using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGamesWebAPI.DTOs
{
  public class UpdateVideoGameDto
  {
    [MinLength(2)]
    public string Name { get; set; }

    public int Price { get; set; }

    [MinLength(2)]
    public string Category { get; set; }

    [MinLength(2)]
    public string Publisher { get; set; }

    [MinLength(2)]
    public string Developer { get; set; }
  }
}
