﻿using Knizhar.Data.Models;
using Knizhar.Services.Books.Models;
using System.Collections.Generic;

namespace Knizhar.Models.Home
{
    public class IndexViewModel
    {
        public int TotalBooks { get; init; }

        public int TotalKnizhari { get; init; }

        public List<BookServiceModel> RecentlyAddedBooks { get; set; } = new List<BookServiceModel>();
        public IEnumerable<Book> BooksByGenre { get; set; } = new List<Book>();

        public IEnumerable<Book> BooksByLocation { get; set; } = new List<Book>();

    }
}
