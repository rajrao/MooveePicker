﻿using System.Collections.Generic;
using System.Linq;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MoviePicker.Common;
using MoviePicker.Common.Interfaces;

namespace MoviePicker.Tests.MsfMovieSolver
{
	[TestClass]
	public class MsfMoviePickerTests : MoviePickerTestBase
	{
		// Unity Reference: https://msdn.microsoft.com/en-us/library/ff648211.aspx
		private static IUnityContainer _unity;

		public override IUnityContainer UnityContainer => _unity;

		[ClassInitialize]
		public static void InitializeBeforeAllTests(TestContext context)
		{
			_unity = new UnityContainer();

			_unity.RegisterType<IMovie, Movie>();
			_unity.RegisterType<IMovieList, MovieList>();
			_unity.RegisterType<IMoviePicker, Msf.MsfMovieSolver>();
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf01()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(1).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(1, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf02()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(2).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(1, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf03()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(3).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(2, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf04()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(4).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(6, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf05()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(5).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(6, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf06()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(6).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(7, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf07()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(7).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(7, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf08()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(8).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(7, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf09()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(9).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(8, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_OutOf10()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks().Take(10).ToList());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(8, best.Movies.Count());
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_WeekEnding_20170604()
		{
			var test = ConstructTestObject();
			var movies = new List<IMovie>();
			int id = 1;

			// FML 
			movies.Add(ConstructMovie(id++, "Wonder Woman", 103, 845));
			movies.Add(ConstructMovie(id++, "Captain Underpants", 23.9m, 239));
			movies.Add(ConstructMovie(id++, "Pirates", 22.1m, 193));
			movies.Add(ConstructMovie(id++, "Guardians", 9.8m, 74));
			movies.Add(ConstructMovie(id++, "Baywatch", 8.7m, 62));
			movies.Add(ConstructMovie(id++, "Alien", 4.1m, 31));
			movies.Add(ConstructMovie(id++, "Everything Everything", 3.3m, 22));
			movies.Add(ConstructMovie(id++, "Diary of a Wimpy Kid", 1.3m, 17));
			movies.Add(ConstructMovie(id++, "Snatched", 1.3m, 14));
			movies.Add(ConstructMovie(id++, "King Arthur", 1.2m, 12));

			// From Raj
			//movies.Add(ConstructMovie(id++, "Wonder Woman", 103.25m, 845));
			//movies.Add(ConstructMovie(id++, "Captain Underpants", 23.9m, 239));
			//movies.Add(ConstructMovie(id++, "Pirates of the Caribbean: Dead Men Tell No Tales", 22.1m, 193));
			//movies.Add(ConstructMovie(id++, "Guardians of the Galaxy Vol. 2", 9.8m, 74));
			//movies.Add(ConstructMovie(id++, "Baywatch", 8.7m, 62));
			//movies.Add(ConstructMovie(id++, "Alien: Covenant", 4.1m, 31));
			//movies.Add(ConstructMovie(id++, "Everything Everything", 3.3m, 22));
			//movies.Add(ConstructMovie(id++, "Diary of a Wimpy Kid: The Long Haul", 1.3m, 17));
			//movies.Add(ConstructMovie(id++, "Snatched", 1.3m, 14));
			//movies.Add(ConstructMovie(id++, "King Arthur: Legend of the Sword", 1.2m, 12));

			// These movies seem inconsequential.
			//movies.Add(ConstructMovie(id++, "The Mummy", 38, 526));
			//movies.Add(ConstructMovie(id++, "It Comes at Night", 20, 150));
			//movies.Add(ConstructMovie(id++, "Meagan Leavey", 3.3m, 59));
			//movies.Add(ConstructMovie(id++, "My Cousin Rachel", 1, 15));
			//movies.Add(ConstructMovie(id++, "Best of the Rest", 1.1m, 9));

			test.AddMovies(movies);

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(1, best.Movies.Count(movie => movie.Name == "Wonder Woman"));
			Assert.AreEqual(7, best.Movies.Count(movie => movie.Name == "Everything Everything"));
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_WeekEnding_20170611()
		{
			var test = ConstructTestObject();
			var movies = new List<IMovie>();
			int id = 1;

			// Weekend gross from box office mojo (with FML bux)

			movies.Add(ConstructMovie(id++, "Wonder Woman", 58520672m, 613));
			movies.Add(ConstructMovie(id++, "The Mummy", 31688375m, 526));
			movies.Add(ConstructMovie(id++, "It Comes at night", 5988370m, 150));
			movies.Add(ConstructMovie(id++, "Pirates of the caribbean", 10704103m, 143));
			movies.Add(ConstructMovie(id++, "Baywatch", 4648207m, 60));
			movies.Add(ConstructMovie(id++, "Megan Leavey", 3810867m, 59));
			movies.Add(ConstructMovie(id++, "Alien: Covenant", 1826579m, 26));
			movies.Add(ConstructMovie(id++, "My Cousin Rachel", 968506m, 15));
			movies.Add(ConstructMovie(id++, "Diary of a wimpy Kid", 656843m, 8));
			movies.Add(ConstructMovie(id++, "Captain Underpants", 12180704m, 198));
			movies.Add(ConstructMovie(id++, "Everything, Everything", 1627295m, 28));
			movies.Add(ConstructMovie(id++, "Guardians of the Galaxy", 6312367m, 70));
			movies.Add(ConstructMovie(id++, "King Arthur", 455744m, 7));
			movies.Add(ConstructMovie(id++, "Snatched", 491644m, 9));
			movies.Add(ConstructMovie(id++, "Best of the rest", 512445m, 9));

			test.AddMovies(movies);

			((Msf.MsfMovieSolver)test).DisplayDebugMessage = true;

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);

			Assert.AreEqual(1, best.Movies.Count(movie => movie.Name == "Wonder Woman"));
			Assert.AreEqual(5, best.Movies.Count(movie => movie.Name == "Guardians of the Galaxy"));
			Assert.AreEqual(1, best.Movies.Count(movie => movie.Name == "Alien: Covenant"));
			Assert.AreEqual(1, best.Movies.Count(movie => movie.Name == "Diary of a wimpy Kid"));
		}

		[TestMethod]
		public void MsfMoviePicker_ChooseBest_ThisWeeksPicks()
		{
			var test = ConstructTestObject();

			test.AddMovies(ThisWeeksMoviesPicks());

			var best = test.ChooseBest();

			WritePicker(test);
			WriteMovies(best);
		}

		//----==== PRIVATE ====---------------------------------------------------------
		private List<IMovie> MoviesPicks_20170609()
		{
			var movies = new List<IMovie>();

			int id = 1;

			movies.Add(ConstructMovie(id++, "Wonder Woman", 55, 613));
			movies.Add(ConstructMovie(id++, "The Mummy", 38, 526));
			movies.Add(ConstructMovie(id++, "Captain Underpants", 12, 198));
			movies.Add(ConstructMovie(id++, "It Comes at Night", 20, 150));
			movies.Add(ConstructMovie(id++, "Pirates", 12, 143));
			movies.Add(ConstructMovie(id++, "Guardians", 5, 70));
			movies.Add(ConstructMovie(id++, "Baywatch", 5, 60));
			movies.Add(ConstructMovie(id++, "Meagan Leavey", 3.3m, 59));
			movies.Add(ConstructMovie(id++, "Everything", 1.5m, 28));
			movies.Add(ConstructMovie(id++, "Alien", 2.1m, 26));
			movies.Add(ConstructMovie(id++, "My Cousin Rachel", 1, 15));
			movies.Add(ConstructMovie(id++, "Snatched", 0.6m, 9));
			movies.Add(ConstructMovie(id++, "Best of the Rest", 1.1m, 9));
			movies.Add(ConstructMovie(id++, "Diary of a Wimpy Kid", 0.6m, 8));
			movies.Add(ConstructMovie(id++, "King Arthur", 0.5m, 7));

			return movies;
		}

		private List<IMovie> ThisWeeksMoviesPicks()
        {
            var movies = new List<IMovie>();

            int id = 1;
			//Mine
			movies.Add(ConstructMovie(id++, "Cars 3", 60, 719));
			movies.Add(ConstructMovie(id++, "Wonder Woman", 30, 478));
			movies.Add(ConstructMovie(id++, "All Eyez on Me", 24, 327));
			movies.Add(ConstructMovie(id++, "Rough Night", 15, 243));
			movies.Add(ConstructMovie(id++, "The Mummy", 14, 167));
			movies.Add(ConstructMovie(id++, "47 Meters Down", 7, 105));
			movies.Add(ConstructMovie(id++, "Captain Underpants", 6, 78));
			movies.Add(ConstructMovie(id++, "Pirates of the caribbean", 5, 71));
			movies.Add(ConstructMovie(id++, "Guardians of the Galaxy", 3, 60));
			movies.Add(ConstructMovie(id++, "It Comes at night", 2.5m, 34));
			movies.Add(ConstructMovie(id++, "The Book of Henry", 0.5m, 31));
			movies.Add(ConstructMovie(id++, "Baywatch", 2, 29));
			movies.Add(ConstructMovie(id++, "Megan Leavey", 1.5m, 25));
			movies.Add(ConstructMovie(id++, "Alien: Covenant", 0.8m, 11));
			movies.Add(ConstructMovie(id++, "Everything, Everything", 0.8m, 10));

			//todd thatcher
			//movies.Add(ConstructMovie(id++, "Cars 3", 57.8M, 719));
			//movies.Add(ConstructMovie(id++, "Wonder Woman", 31.2m, 478));
			//movies.Add(ConstructMovie(id++, "All Eyez on Me", 24.4m, 327));
			//movies.Add(ConstructMovie(id++, "Rough Night", 15.1m, 243));
			//movies.Add(ConstructMovie(id++, "The Mummy", 13.2m, 167));
			//movies.Add(ConstructMovie(id++, "47 Meters Down", 7, 105));
			//movies.Add(ConstructMovie(id++, "Captain Underpants", 5.6m, 78));
			//movies.Add(ConstructMovie(id++, "Pirates of the caribbean", 5.2m, 71));
			//movies.Add(ConstructMovie(id++, "Guardians of the Galaxy", 4.2m, 60));
			//movies.Add(ConstructMovie(id++, "It Comes at night", 2.2m, 34));
			//movies.Add(ConstructMovie(id++, "The Book of Henry", 1.8m, 31));
			//movies.Add(ConstructMovie(id++, "Baywatch", 0.82m, 29));
			//movies.Add(ConstructMovie(id++, "Megan Leavey", 2.1m, 25));
			//movies.Add(ConstructMovie(id++, "Alien: Covenant", 0.82m, 11));
			//movies.Add(ConstructMovie(id++, "Everything, Everything", 0.75m, 10));

			//medians
			//movies.Add(ConstructMovie(id++, "Cars 3", 60M, 719));
			//movies.Add(ConstructMovie(id++, "Wonder Woman", 30m, 478));
			//movies.Add(ConstructMovie(id++, "All Eyez on Me", 22m, 327));
			//movies.Add(ConstructMovie(id++, "Rough Night", 15.08m, 243));
			//movies.Add(ConstructMovie(id++, "The Mummy", 14.83m, 167));
			//movies.Add(ConstructMovie(id++, "47 Meters Down", 5.75m, 105));
			//movies.Add(ConstructMovie(id++, "Captain Underpants", 6m, 78));
			//movies.Add(ConstructMovie(id++, "Pirates of the caribbean", 5.2m, 71));
			//movies.Add(ConstructMovie(id++, "Guardians of the Galaxy", 3.16m, 60));
			//movies.Add(ConstructMovie(id++, "It Comes at night", 2.97m, 34));
			//movies.Add(ConstructMovie(id++, "The Book of Henry", 0.59m, 31));
			//movies.Add(ConstructMovie(id++, "Baywatch", 1.83m, 29));
			//movies.Add(ConstructMovie(id++, "Megan Leavey", 2.05m, 25));
			//movies.Add(ConstructMovie(id++, "Alien: Covenant", 0.81m, 11));
			//movies.Add(ConstructMovie(id++, "Everything, Everything", 0.775m, 10));

			return movies;
        }
    }
}
