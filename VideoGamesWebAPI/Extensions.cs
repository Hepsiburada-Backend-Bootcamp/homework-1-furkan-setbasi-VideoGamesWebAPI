using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGamesWebAPI.DTOs;
using VideoGamesWebAPI.Entities;

namespace VideoGamesWebAPI
{
  public static class Extensions
  {
    public static ReadVideoGameDto ConvertToDto(this VideoGame vg)
    {
      return new ReadVideoGameDto()
      {
        Id = vg.Id,
        Name = vg.Name,
        Price = vg.Price,
        Category = vg.Category,
        Developer = vg.Developer,
        Publisher = vg.Publisher,
      };
    }
  }
}
