using System;
using System.Collections.Generic;
using System.Linq;
using Lab3_.Data;
using Lab3_.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Transport.Infrastructure.Data;

namespace Lab3_.Services
{
    // Класс выборки 10 записей из таблиц 
    public class TransportService : ITransportService
    {
        private readonly TransportContext _context;
        private readonly IMemoryCache _cache;

        private const int NumberRows = 20;

        public TransportService(TransportContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public List<string> GetRanks()
        {
            var ranks = _context.Ranks
                .Select(rank => rank.Name)
                .Distinct()
                .ToList();

            return ranks;
        }

        public List<EmployeeViewModel> SearchEmployees(string rank, string lastname)
        {
            rank = rank.ToLower();
            lastname = lastname.ToLower();

            var result = _context.Employees
                .Include(x => x.Rank)
                .Where(x => x.Rank.Name.ToLower().StartsWith(rank) && x.SecondName.ToLower().StartsWith(lastname))
                .Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    Rank = x.Rank.Name,
                    FirstName = x.FirstName,
                    IsSecondShift = x.IsSecondShift,
                    LastName = x.SecondName
                })
                .ToList();

            return result;
        }

        public HomeViewModel GetHomeViewModel(string cacheKey)
        {
            if (_cache.TryGetValue(cacheKey, out HomeViewModel result))
            {
                return result;
            }

            var transportTypes = _context.TransportTypes.Take(NumberRows).ToList();
            var ranks = _context.Ranks.Take(NumberRows).ToList();

            var employees = _context.Employees
                .Include(t => t.Rank)
                .Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    Rank = x.Rank.Name,
                    FirstName = x.FirstName,
                    IsSecondShift = x.IsSecondShift,
                    LastName = x.SecondName
                })
                .Take(NumberRows)
                .ToList();

            result = new HomeViewModel
            {
                Ranks = ranks,
                TransportTypes = transportTypes,
                Employees = employees
            };
            _cache.Set(cacheKey, result,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 14 + 240)));

            return result;
        }
    }
}