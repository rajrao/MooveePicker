﻿using System.Collections.Generic;

namespace MoviePicker.Common.Interfaces
{
	public interface IMovieList
	{
		bool IsFull { get; }

		IEnumerable<IMovie> Movies { get; }

		decimal TotalCost { get; }

		decimal TotalEarnings { get; }

		void Add(IMovie movie);

		void Add(IEnumerable<IMovie> movies);

		bool CanAdd(IMovie movie);

		void Clear();

		IMovieList Clone();

		int GetHashCode();

		void Remove(IMovie movie);
	}
}