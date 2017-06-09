﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MooveePicker
{
	/// <summary>
	/// Vary the earnings by some offsets and compute the BEST out of how many of that list WINS.
	/// </summary>
	public class MoviePickerVariants : IMoviePicker
	{
		private const decimal EARNINGS_ADJUSTMENT = 0.5m;
		private const decimal EARNINGS_VARIANT_MAX = 0.5m;

		private readonly Dictionary<int, int> _bestListCounts;          // Keyed using the hash code.
		private readonly Dictionary<int, IMovieList> _bestLists;                // Keyed using the hash code.
		private readonly IMovieList _movieListPrototype;
		private readonly IMoviePicker _moviePicker;
		private readonly List<IMovie> _movies;

		public MoviePickerVariants(IMovieList movieListPrototype)
		{
			_bestListCounts = new Dictionary<int, int>();
			_bestLists = new Dictionary<int, IMovieList>();
			_moviePicker = new MoviePicker(new MovieList());
			_movies = new List<IMovie>();

			_movieListPrototype = movieListPrototype;
		}

		public IEnumerable<IMovie> Movies => _moviePicker.Movies;

		public int TotalComparisons { get; set; }

		public int TotalSubProblems { get; set; }

		public void AddMovies(IEnumerable<IMovie> movies)
		{
			// Need base copies of the movie earnings, so you need clones.

			foreach (var movie in movies)
			{
				_movies.Add(movie.Clone());
			}
		}

		public IMovieList ChooseBest()
		{
			var movieLists = GenerateMovieLists();

			// Walk through the movie list adjusting the movies by the variant and then choose the best.

			foreach (var movieList in movieLists)
			{
				_moviePicker.AddMovies(movieList);

				var best = _moviePicker.ChooseBest();
				var hashCode = best.GetHashCode();
				int value;

				TotalComparisons += ((MoviePicker)_moviePicker).TotalComparisons;
				TotalSubProblems += ((MoviePicker)_moviePicker).TotalSubProblems;

				if (_bestListCounts.TryGetValue(hashCode, out value))
				{
					_bestListCounts[hashCode] = value + 1;
				}
				else
				{
					_bestListCounts.Add(hashCode, 1);
					_bestLists.Add(hashCode, best);
				}
			}

			// Sort through the MOST times a list is counted.

			var bestHash = _bestListCounts.OrderByDescending(item => item.Value).First().Key;

			return _bestLists[bestHash];
		}

		//----==== PRIVATE ====----------------------------------------------------------------------

		private List<List<IMovie>> GenerateMovieLists()
		{
			var result = new List<List<IMovie>>();
			var earningsAdjustment = EARNINGS_ADJUSTMENT * 1000000m;

			result.Add(_movies);        // Add the original list

			// Only tweak one movie in each list.

			foreach (var movie in _movies)
			{
				var list = Copy(_movies);
				var movieToAdjust = list.First(item => item.Id == movie.Id);

				movieToAdjust.Earnings += earningsAdjustment;

				result.Add(list);

				var list2 = Copy(_movies);
				var movieToAdjust2 = list2.First(item => item.Id == movie.Id);

				movieToAdjust2.Earnings -= earningsAdjustment;

				result.Add(list2);
			}

			return result;
		}

		private List<IMovie> Copy(IEnumerable<IMovie> toCopy)
		{
			return toCopy.Select(movie => movie.Clone()).ToList();
		}
	}
}