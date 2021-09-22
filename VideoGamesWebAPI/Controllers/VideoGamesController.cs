namespace VideoGamesWebAPI.Controllers
{
  using FluentValidation;
  using Microsoft.AspNetCore.Mvc;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using VideoGamesWebAPI.DTOs;
  using VideoGamesWebAPI.Entities;
  using VideoGamesWebAPI.QueryFilters;

  /// <summary>
  /// Defines the <see cref="VideoGamesController" />.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class VideoGamesController : ControllerBase
  {
    /// <summary>
    /// Defines the _videoGames.
    /// </summary>
    public static List<VideoGame> _videoGames = new List<VideoGame>(){
        new VideoGame{Id=1, Name="Rainbow Six Siege", Price=40, Category="FPS", Developer="Ubisoft Montreal", Publisher="Ubisoft"},
        new VideoGame{Id=2, Name="Fifa 21", Price=60, Category="Sports", Developer="EA Canada", Publisher="EA"},
        new VideoGame{Id=3, Name="The Elder Scrolls V: Skyrim", Price=30, Category="RPG", Developer="Bethesda Game Studios", Publisher="Bethesda Softworks"},
      };

    /// <summary>
    /// Returns all video games, applies filters if there is any.
    /// </summary>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns all video games with given filters.</response>
    [HttpGet]
    [ProducesResponseType(200)]
    public IActionResult Get([FromQuery] VideoGamesFilter filters)
    {
      try
      {
        List<ReadVideoGameDto> videoGames = _videoGames.Select(vg => vg.ConvertToDto()).ToList();

        VideoGamesFilterValidator validator = new VideoGamesFilterValidator();

        validator.ValidateAndThrow(filters);

        videoGames = videoGames.FindAll(vg =>
          vg.Name.ToLower() == (filters.Name is not null ? filters.Name.ToLower() : vg.Name.ToLower()) &&
          vg.Category.ToLower() == (filters.Category is not null ? filters.Category.ToLower() : vg.Category.ToLower()) &&
          vg.Developer.ToLower() == (filters.Developer is not null ? filters.Developer.ToLower() : vg.Developer.ToLower()) &&
          vg.Publisher.ToLower() == (filters.Publisher is not null ? filters.Publisher.ToLower() : vg.Publisher.ToLower()) &&
          vg.Price <= (filters.CheaperThan is not null ? int.Parse(filters.CheaperThan) : vg.Price) &&
          vg.Price >= (filters.MoreExpensiveThan is not null ? int.Parse(filters.MoreExpensiveThan) : vg.Price)
        );

        switch(filters.SortBy.ToLower())
        {
          case "name":
            videoGames.Sort((a, b) => a.Name.CompareTo(b.Name));
            break;

          case "price":
            videoGames.Sort((a, b) => a.Price.CompareTo(b.Price));
            break;

          case "category":
            videoGames.Sort((a, b) => a.Category.CompareTo(b.Category));
            break;

          case "developer":
            videoGames.Sort((a, b) => a.Developer.CompareTo(b.Developer));
            break;

          case "publisher":
            videoGames.Sort((a, b) => a.Publisher.CompareTo(b.Publisher));
            break;

          default:
            videoGames.Sort((a, b) => a.Id.CompareTo(b.Id));
            break;
        }

        if(filters.SortOrder == "desc")
        {
          videoGames.Reverse();
        }

        return Ok(videoGames);

      }
      catch(Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }

    /// <summary>
    /// Returns the video game with given Id.
    /// </summary>
    /// <param name="id">The id<see cref="int"/>.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the video game with given Id.</response>
    /// <response code="404">No video game found with given Id.</response>

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    public IActionResult Get(int id)
    {
      VideoGame videoGame = _videoGames.SingleOrDefault(vg => vg.Id == id);

      if(videoGame is null)
      {
        return NotFound();
      }

      return Ok(videoGame.ConvertToDto());
    }

    /// <summary>
    /// Creates new video game with given inputs.
    /// </summary>
    /// <param name="createVideoGameDto">The createVideoGameDto<see cref="CreateVideoGameDto"/>.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="201">Creates new video game with given inputs and returns it.</response>
    /// <response code="400">A Validation error occured due to invalid inputs.</response>
    [HttpPost]
    [ProducesResponseType(201)]
    public IActionResult Post([FromBody] CreateVideoGameDto createVideoGameDto)
    {
      try
      {
        int maxId = _videoGames.Max(x => x.Id);

        CreateVideoGameDtoValidator validator = new CreateVideoGameDtoValidator();
        validator.ValidateAndThrow(createVideoGameDto);

        VideoGame newVideoGame = new VideoGame()
        {
          Id = maxId + 1,
          Name = createVideoGameDto.Name,
          Price = createVideoGameDto.Price,
          Category = createVideoGameDto.Category,
          Developer = createVideoGameDto.Developer,
          Publisher = createVideoGameDto.Publisher
        };

        _videoGames.Add(newVideoGame);

        VideoGame createdVideoGame = _videoGames.Find(vg => vg.Id == maxId + 1);

        return Created($"/api/videogames/{maxId + 1}", createdVideoGame);
      }
      catch(Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    /// <summary>
    /// Updates whole entity with given inputs.
    /// </summary>
    /// <param name="id">The id<see cref="int"/>.</param>
    /// <param name="updateVideoGameDto">The updateVideoGameDto<see cref="UpdateVideoGameDto"/>.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the updated video game.</response>
    /// <response code="400">A Validation error occured due to invalid inputs.</response>
    /// <response code="404">No video game found with given Id.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    public IActionResult Put(int id, [FromBody] UpdateVideoGameDto updateVideoGameDto)
    {
      try
      {
        UpdateVideoGameDtoValidator validator = new UpdateVideoGameDtoValidator();
        validator.ValidateAndThrow(updateVideoGameDto);

        VideoGame videoGame = _videoGames.SingleOrDefault(vg => vg.Id == id);

        if(videoGame is null)
        {
          return NotFound();
        }

        videoGame.Name = updateVideoGameDto.Name;
        videoGame.Price = updateVideoGameDto.Price;
        videoGame.Category = updateVideoGameDto.Category;
        videoGame.Developer = updateVideoGameDto.Developer;
        videoGame.Publisher = updateVideoGameDto.Publisher;

        VideoGame updatedVG = _videoGames.Find(vg => vg.Id == id);
        return Ok(updatedVG.ConvertToDto());
      }
      catch(Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }

    /// <summary>
    /// Updates video game price.
    /// </summary>
    /// <param name="id">The id<see cref="int"/>.</param>
    /// <param name="updateVideoGamePriceDto">The updateVideoGameDto<see cref="UpdateVideoGamePriceDto"/>.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="200">Returns the video game with new price.</response>
    /// <response code="400">A Validation error occured due to invalid inputs.</response>
    /// <response code="404">No video game found with given Id.</response> 
    [HttpPatch("{id}")]
    public IActionResult UpdatePrice(int id, [FromBody] UpdateVideoGamePriceDto updateVideoGamePriceDto)
    {

      try
      {
        UpdateVideoGamePriceDtoValidator validator = new UpdateVideoGamePriceDtoValidator();
        validator.ValidateAndThrow(updateVideoGamePriceDto);

        VideoGame videoGame = _videoGames.SingleOrDefault(vg => vg.Id == id);

        if(videoGame is null)
        {
          return NotFound();
        }

        videoGame.Price = updateVideoGamePriceDto.Price;

        VideoGame updatedVG = _videoGames.Find(vg => vg.Id == id);
        return Ok(updatedVG.ConvertToDto());
      }
      catch(Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }

    /// <summary>
    /// Deletes the video game with given Id.
    /// </summary>
    /// <param name="id">The id<see cref="int"/>.</param>
    /// <returns>The <see cref="IActionResult"/>.</returns>
    /// <response code="204">Video game deleted successfully.</response>
    /// <response code="404">No video game found with given Id.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public IActionResult Delete(int id)
    {
      VideoGame videoGameToRemove = _videoGames.SingleOrDefault(vg => vg.Id == id);
      if(videoGameToRemove is null)
      {
        return NotFound();
      }

      _videoGames.Remove(videoGameToRemove);

      return NoContent();
    }
  }
}
