using System;
using System.ComponentModel;

namespace Transport.Infrastructure.Data.Entities
{



    public class Employee
    {
        public Guid Id { get; set; }

        [DisplayName("Маршрут")]
        public Guid? RouteId { get; set; }

        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string SecondName { get; set; }

        public bool IsSecondShift { get; set; }

        [DisplayName("Должность")]
        public int RankId { get; set; }


        public Route Route { get; set; }

        public Rank Rank { get; set; }
    }
}