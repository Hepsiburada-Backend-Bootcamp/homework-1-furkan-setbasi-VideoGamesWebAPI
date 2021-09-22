using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGamesWebAPI.DTOs
{
  public class UpdateVideoGamePriceDtoValidator : AbstractValidator<UpdateVideoGamePriceDto>
  {
    public UpdateVideoGamePriceDtoValidator()
    {
      RuleFor(dto => dto.Price).GreaterThan(0);
    }
  }
}
