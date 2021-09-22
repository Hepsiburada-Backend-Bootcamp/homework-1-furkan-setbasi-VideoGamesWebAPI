using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGamesWebAPI.QueryFilters
{
  public class VideoGamesFilter
  {
    public string Name { get; set; }
    public string MoreExpensiveThan { get; set; }
    public string CheaperThan { get; set; }
    public string Category { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }

    /// <summary>
    /// You can sort the result by name, price, category, developer 
    /// and publisher. Give any field name as input. Result will be 
    /// sorted in ascending order by the field name you provided.
    /// </summary>
    public string SortBy { get; set; }

    /// <summary>
    /// Give "desc" if you want to sort the result in descending order.
    /// Default sort order is ascending.
    /// </summary>
    public string SortOrder { get; set; }
  }
}
