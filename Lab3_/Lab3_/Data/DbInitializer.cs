using System;
using System.Linq;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;

namespace Lab3_.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TransportContext db)
        {
            db.Database.EnsureCreated();

            if (db.Ranks.Any()) return;

            const int ranksNumber = 35;
            const int employeesNumber = 35;
            const int transportTypesNumber = 300;

            var randObj = new Random(1);

            var ranks = Enumerable.Range(1, ranksNumber)
                .Select(orgId => new Rank
                {
                    Name = "TestRank_" + orgId
                })
                .ToList();
            db.Ranks.AddRange(ranks);
            db.SaveChanges();

            var employees = Enumerable.Range(1, employeesNumber)
                .Select(driverId => new Employee
                {
                    FirstName = "TestDriverFirstName" + driverId,
                    SecondName = "TestDriverLastName" + driverId,
                    IsSecondShift = driverId % 2 == 0,
                    RankId = randObj.Next(1, ranksNumber)
                })
                .ToList();
            db.Employees.AddRange(employees);
            db.SaveChanges();

            var transportTypes = Enumerable.Range(1, transportTypesNumber)
                .Select(num => new TransportType
                {
                    Name = "TestTransportType_" + num
                })
                .ToList();

            db.TransportTypes.AddRange(transportTypes);
            db.SaveChanges();
        }
    }
}