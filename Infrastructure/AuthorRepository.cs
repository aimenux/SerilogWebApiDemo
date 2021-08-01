﻿using Domain.Ports;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMemoryCache _cache;

        public AuthorRepository(IMemoryCache cache)
        {
            _cache = cache;
            AddToBlackListAuthors("foo");
            AddToBlackListAuthors("bar");
        }

        public void AddToBlackListAuthors(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return;
            }

            var key = BuildKey(author);
            if (_cache.TryGetValue(key, out var _))
            {
                return;
            }

            _cache.Set(key, author);
        }

        public bool IsBlackListedAuthor(string author)
        {
            var key = BuildKey(author);
            return _cache.TryGetValue(key, out var _);
        }

        private static string BuildKey(string author)
        {
            return author?.ToUpper();
        }
    }
}
