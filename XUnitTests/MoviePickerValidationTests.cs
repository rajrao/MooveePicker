using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MoviePicker.Common;
using MoviePicker.Common.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTests
{
	public abstract class MoviePickerTestBase
	{
		protected ITestOutputHelper OutputHelper { get; set; }

		protected MoviePickerValidationTestsContext Context { get; set; }

		public MoviePickerTestBase(ITestOutputHelper outputHelper, MoviePickerValidationTestsContext context)
		{
			OutputHelper = outputHelper;
			Context = context;
		}
		
		[Fact]
		public void MoviePicker_ChooseBest()
		{
			var test = ConstructTestObject();
			var movies = new List<IMovie>();

			int id = 1;
			movies.Add(ConstructMovie(id++, "The Hitman's Bodyguard", 10.13375m, 203));
			movies.Add(ConstructMovie(id++, "Annabelle: Creation", 7.0268m, 143));
			movies.Add(ConstructMovie(id++, "Birth of the Dragon", 2.85m, 99));
			movies.Add(ConstructMovie(id++, "Dunkirk", 4.01425m, 74));
			movies.Add(ConstructMovie(id++, "Logan Lucky", 3.96825m, 73));
			movies.Add(ConstructMovie(id++, "Leap!", 4.4m, 68));
			movies.Add(ConstructMovie(id++, "All Saints", 3.425m, 67));
			movies.Add(ConstructMovie(id++, "The Nut Job 2: Nutty by Nature", 2.8905m, 58));
			movies.Add(ConstructMovie(id++, "The Emoji Movie", 2.658m, 53));
			movies.Add(ConstructMovie(id++, "Spider-Man: Homecoming", 2.7845m, 52));
			movies.Add(ConstructMovie(id++, "Wonder Woman", 2.4m, 50));
			movies.Add(ConstructMovie(id++, "Girls Trip", 2.15333333333333m, 40));
			movies.Add(ConstructMovie(id++, "The Dark Tower", 1.576m, 33));
			movies.Add(ConstructMovie(id++, "Baby Driver", 1.354m, 32));
			movies.Add(ConstructMovie(id++, "Kidnap", 1.46m, 30));

			WriteMovies(movies);

			test.AddMovies(movies);

			var best = test.ChooseBest();
			OutputHelper.WriteLine("---- Best Performer Enabled ----");
			WritePicker(test);
			WriteMovies(best);

			OutputHelper.WriteLine("---- Best Performer Disabled ----");
			test.EnableBestPerformer = false;
			best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			//Assert.Equal(1, best.Movies.Count(movie => movie.Name == "Transformers"));
			//Assert.Equal(7, best.Movies.Count(movie => movie.Name == "47 Meters Down"));
		}

		/*
		[Fact]
		public void MoviePicker_ChooseBest_WeekEnding_2017MMDD_Week_N_Template()
		{
			var test = ConstructTestObject();
			var movies = new List<IMovie>();

			int id = 1;

			

			test.AddMovies(movies);

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			//Assert.Equal(1, best.Movies.Count(movie => movie.Name == "Transformers"));
			//Assert.Equal(7, best.Movies.Count(movie => movie.Name == "47 Meters Down"));
		}
		 */

		protected IMoviePicker ConstructTestObject()
		{
			if (Context == null)
			{
				throw new NullReferenceException("Context must be set before construction the subject under test");
			}
			return Context.UnityContainer.Resolve<IMoviePicker>();
		}

		protected IMovie ConstructMovie(int id, string name, decimal millions, decimal cost)
		{
			var result = Context.UnityContainer.Resolve<IMovie>();

			result.Id = id;
			result.Name = name;
			result.Earnings = millions * 1000000m;
			result.Cost = cost;

			return result;
		}

		protected void WriteMovies(IMovieList movies)
		{
			int screen = 1;

			OutputHelper.WriteLine($"Total Cost (Bux): {movies.TotalCost}");
			OutputHelper.WriteLine($"Total Earnings  : ${movies.TotalEarnings:N0}");

			foreach (var movie in movies.Movies.OrderByDescending(item => item.Earnings))
			{
				OutputHelper.WriteLine($"{movie.Name,-30}\t\t${movie.Earnings:N2}\t${movie.Efficiency:N2}");
			}
		}

		protected void WritePicker(IMoviePicker moviePicker)
		{
			OutputHelper.WriteLine($"Picker: {moviePicker.GetType().Name}");
			OutputHelper.WriteLine($"Total Comparisons: {moviePicker.TotalComparisons:N0} [{moviePicker.TotalComparisons / Math.Pow(16, 8) * 100}% of {Math.Pow(16, 8):N0}]");
			OutputHelper.WriteLine($"Total Sub-problems: {moviePicker.TotalSubProblems:N0}");
		}

		protected void WriteMovies(IEnumerable<IMovie> movies)
		{
			int screen = 1;
			OutputHelper.WriteLine($"{"name",-30}\t{"bux",5}\t{"est.earn",-15}\t{"efficiency",-10}");
			foreach (var movie in movies.OrderByDescending(item => item.Cost))
			{
				OutputHelper.WriteLine($"{movie.Name,-30}\t{movie.Cost,5}\t${movie.Earnings:N2}\t${movie.Efficiency:N2}");
			}
		}
	}
}
