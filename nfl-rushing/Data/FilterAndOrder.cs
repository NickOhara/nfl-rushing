using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl_rushing.Data
{
    public static class FilterAndOrder
    {
        public static IQueryable<PlayerStats> AddSearch(this IQueryable<PlayerStats> toSearch, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                toSearch = toSearch.Where(x => x.Player.ToLower().Contains(searchString));
            }

            return toSearch;
        }

        public static IQueryable<PlayerStats> AddSort(this IQueryable<PlayerStats> toSort, string sortBy)
        {
            switch (sortBy)
            {
                case "player":
                    toSort = toSort.OrderBy(s => s.Player);
                    break;
                case "player_desc":
                    toSort = toSort.OrderByDescending(s => s.Player);
                    break;
                case "team":
                    toSort = toSort.OrderBy(s => s.Team);
                    break;
                case "team_desc":
                    toSort = toSort.OrderByDescending(s => s.Team);
                    break;
                case "pos":
                    toSort = toSort.OrderBy(s => s.Pos);
                    break;
                case "pos_desc":
                    toSort = toSort.OrderByDescending(s => s.Pos);
                    break;
                case "att/g":
                    toSort = toSort.OrderBy(s => s.AttPerG);
                    break;
                case "att/g_desc":
                    toSort = toSort.OrderByDescending(s => s.AttPerG);
                    break;
                case "att":
                    toSort = toSort.OrderBy(s => s.Att);
                    break;
                case "att_desc":
                    toSort = toSort.OrderByDescending(s => s.Att);
                    break;
                case "yds":
                    toSort = toSort.OrderBy(s => s.Yds);
                    break;
                case "yds_desc":
                    toSort = toSort.OrderByDescending(s => s.Yds);
                    break;
                case "avg":
                    toSort = toSort.OrderBy(s => s.Avg);
                    break;
                case "avg_desc":
                    toSort = toSort.OrderByDescending(s => s.Avg);
                    break;
                case "yds/g":
                    toSort = toSort.OrderBy(s => s.YdsPerG);
                    break;
                case "yds/g_desc":
                    toSort = toSort.OrderByDescending(s => s.YdsPerG);
                    break;
                case "td":
                    toSort = toSort.OrderBy(s => s.TD);
                    break;
                case "td_desc":
                    toSort = toSort.OrderByDescending(s => s.TD);
                    break;
                //this is not great. I think if I were to do this again I would have split the Lng column into two, LongestRush and DidLongestRushEndInTD.
                //or maybe not use sqlLite and instead try to use a noSQL db
                case "lng":
                    toSort = toSort.AsEnumerable().OrderBy(s => Convert.ToInt32(s.Lng.Replace('T', ' ').Trim())).AsQueryable();
                    break;
                case "lng_desc":
                    toSort = toSort.AsEnumerable().OrderByDescending(s => Convert.ToInt32(s.Lng.Replace('T', ' ').Trim())).AsQueryable();
                    break;
                case "1st":
                    toSort = toSort.OrderBy(s => s.First);
                    break;
                case "1st_desc":
                    toSort = toSort.OrderByDescending(s => s.First);
                    break;
                case "1st%":
                    toSort = toSort.OrderBy(s => s.FirstPercentage);
                    break;
                case "1st%_desc":
                    toSort = toSort.OrderByDescending(s => s.FirstPercentage);
                    break;
                case "20+":
                    toSort = toSort.OrderBy(s => s.TwentyPlus);
                    break;
                case "20+_desc":
                    toSort = toSort.OrderByDescending(s => s.TwentyPlus);
                    break;
                case "40+":
                    toSort = toSort.OrderBy(s => s.FortyPlus);
                    break;
                case "40+_desc":
                    toSort = toSort.OrderByDescending(s => s.FortyPlus);
                    break;
                case "fum":
                    toSort = toSort.OrderBy(s => s.FUM);
                    break;
                case "fum_desc":
                    toSort = toSort.OrderByDescending(s => s.FUM);
                    break;
                default:
                    break;
            }

            return toSort;
        }
    }
}
