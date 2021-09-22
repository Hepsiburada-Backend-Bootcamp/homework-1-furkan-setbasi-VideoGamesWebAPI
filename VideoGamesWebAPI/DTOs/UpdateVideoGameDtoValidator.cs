using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGamesWebAPI.DTOs
{
  public class UpdateVideoGameDtoValidator : AbstractValidator<UpdateVideoGameDto>
  {
    public UpdateVideoGameDtoValidator()
    {
      RuleFor(dto => dto.Name).NotEmpty().MinimumLength(2);
      RuleFor(dto => dto.Price).GreaterThan(0);
      RuleFor(dto => dto.Category).NotEmpty().MinimumLength(2);
      RuleFor(dto => dto.Developer).NotEmpty().MinimumLength(2);
      RuleFor(dto => dto.Publisher).NotEmpty().MinimumLength(2);
    }
  }
}
