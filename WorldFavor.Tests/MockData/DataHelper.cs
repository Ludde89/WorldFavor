using System;
using System.Collections.Generic;
using System.Linq;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Tests.MockData
{
    public static class DataHelper
    {
        private static List<BookEntity> _books = new List<BookEntity>();
        private static List<ReaderEntity> _readers = new List<ReaderEntity>();
        public static IEnumerable<BookEntity> GetBooks()
        {
            if (!_books.Any())
            {
                _books.AddRange(Enumerable.Range(0, 5).Select(x => new BookEntity
                {
                    Checkout = DateTime.Now,
                    ISBN = $"foo{x}",
                    Id = x,
                    IsLost = false,
                    Title = $"bar{x}"
                }));
            }

            return _books;
        }

        public static IEnumerable<ReaderEntity> GetReaders()
        {
            if (!_readers.Any())
            {
                _readers.AddRange(Enumerable.Range(0, 5).Select(x => new ReaderEntity
                {
                    Id = x,
                    Name = $"foobar{x}",
                    Birth = DateTime.Now.AddDays(-x)
                }));
            }

            return _readers;
        }
    }
}