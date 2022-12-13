using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;

namespace Lab2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("1. Выборка из таблицы TransportTypes");
            var transportTypes = await DataHelper.GetEntities<TransportType>();
            Console.WriteLine(Serialize(transportTypes));

            var transportTypeFilterValue = "Автобус";
            Console.WriteLine($"2. Выборка из таблицы TransportTypes с названием \"{transportTypeFilterValue}\"");
            var filteredTransportTypes = await DataHelper.GetEntities<TransportType>(transportTypeFilterValue, nameof(TransportType.Name));
            Console.WriteLine(Serialize(filteredTransportTypes));

            Console.WriteLine("3. Количество из таблицы Employees с по должности");
            var groupedCars = DataHelper.GetQuery<Employee>()
                .Include(x => x.Rank)
                .GroupBy(c => c.Rank.Name)
                .Select(c => new
                {
                    Count = c.Count(),
                    Mark = c.Key
                })
                .ToList();
            Console.WriteLine(Serialize(groupedCars));

            var employeeFirstFilter = "Иван";
            var employeeSecondFilter = "Иванов";
            Console.WriteLine($"4. Выборка из таблицы Employees с именем \"{employeeFirstFilter}\" и фамилией \"{employeeSecondFilter}\"");
            var filteredDrivers = await DataHelper.GetEntities<Employee>(employeeFirstFilter, nameof(Employee.FirstName), employeeSecondFilter, nameof(Employee.SecondName));
            Console.WriteLine(Serialize(filteredDrivers));

            var routeTypeFilterValue = "Северная";
            Console.WriteLine($"5. Выборка из таблицы RouteStations с названием маршрута \"{routeTypeFilterValue}\"");
            var filteredStations = DataHelper.GetQuery<RouteStation>()
                .Include(x => x.Route)
                .Where(x => x.Route.Name == routeTypeFilterValue)
                .ToList();
            Console.WriteLine(Serialize(filteredStations));

            var rankToInsert = new Rank
            {
                Name = "Test_Rank"
            };
            Console.WriteLine($"6. Вставка должности {Serialize(rankToInsert)}");
            await DataHelper.Add(rankToInsert);

            var employeeToInsert = new Employee
            {
                FirstName = "test_name",
                SecondName = "test_last_name",
                IsSecondShift = false,
                RankId = rankToInsert.Id,
            };
            Console.WriteLine($"7. Вставка сотрудника {Serialize(employeeToInsert)}");
            await DataHelper.Add(employeeToInsert);

            Console.WriteLine($"9. Удаление сотрудника {Serialize(employeeToInsert)}");
            await DataHelper.DeleteEntity<Employee>(employeeToInsert.Id);

            Console.WriteLine($"10. Обновление всех сотрудников со второй смены");
            var entitiesToUpdate = DataHelper.GetQuery<Employee>()
                .Where(x => x.IsSecondShift)
                .ToList();
            entitiesToUpdate
                .ForEach(t => t.IsSecondShift = true);
            await DataHelper.UpdateRange(entitiesToUpdate);

        }

        static string Serialize(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });

            return serialized;
        }
    }
}