using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGamesWebAPI.QueryFilters
{
  public class VideoGamesFilterValidator : AbstractValidator<VideoGamesFilter>
  {
    public VideoGamesFilterValidator()
    {
      RuleFor(filter => filter.CheaperThan)
        .Must(x => int.TryParse(x, out var val) && val > 0)
        .WithMessage("Please enter a positive number.")
        .When(filter => filter.CheaperThan is not null);

      RuleFor(filter => filter.MoreExpensiveThan)
        .Must(x => int.TryParse(x, out var val) && val > 0)
        .WithMessage("Please enter a positive number.")
        .When(filter => filter.MoreExpensiveThan is not null);
    }
  }
}
