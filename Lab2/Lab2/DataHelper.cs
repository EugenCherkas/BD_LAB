using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Transport.Infrastructure.Data;

namespace Lab2
{
    public static class DataHelper
    {
        private static readonly TransportContext _context;

        static DataHelper()
        {
            var appsettingsText = File.ReadAllText("appsettings.json");
            var connectionString = JObject.Parse(appsettingsText)["ConnectionString"].ToString();

            var optionsBuilder = new DbContextOptionsBuilder<TransportContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _context = new TransportContext(optionsBuilder.Options);
        }

        public static IQueryable<T> GetQuery<T>()
            where T : class
        {
            return _context.Set<T>().AsNoTracking();
        }

        public static async Task<List<T>> GetEntities<T>(string filter, string targetColumnName)
            where T : class
        {
            var entities = await _context.Set<T>()
                .Where(ExpressionUtils.BuildPredicate<T>(targetColumnName, "==", filter))
                .ToListAsync();

            return entities;
        }

        public static async Task<IEnumerable<T>> GetEntities<T>(string filter1, string targetColumnName1, string filter2, string targetColumnName2)
            where T : class
        {
            var entities = await _context.Set<T>()
                .Where(ExpressionUtils.BuildPredicate<T>(targetColumnName1, "==", filter1))
                .Where(ExpressionUtils.BuildPredicate<T>(targetColumnName2, "==", filter2))
                .ToListAsync();

            return entities;
        }

        public static async Task<IEnumerable<T>> GetEntities<T>()
            where T : class
        {
            var entities = await _context.Set<T>()
                .ToListAsync();

            return entities;
        }

        public static async Task DeleteEntity<T>(Guid id, string idColumnName = "Id")
            where T : class
        {
            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(ExpressionUtils.BuildPredicate<T>(idColumnName, "==", id.ToString()));

            if (entity is not null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task DeleteEntity<T>(int id, string idColumnName = "Id")
            where T : class
        {
            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(ExpressionUtils.BuildPredicate<T>(idColumnName, "==", id.ToString()));

            if (entity is not null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task UpdateRange<T>(IEnumerable<T> entities)
            where T : class
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public static async Task Update<T>(T entity)
            where T : class
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public static async Task Add<T>(T entity)
            where T : class
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}