using System;
using System.ComponentModel;

namespace Lab3_.ViewModels
{
    public class EmployeeViewModel
    {
        [DisplayName("#")] public Guid Id { get; set; }

        [DisplayName("Имя")] public string FirstName { get; set; }

        [DisplayName("Фамилия")] public string LastName { get; set; }

        [DisplayName("Смена")]
        public bool IsSecondShift { get; set; }

        [DisplayName("Должность")]
        public string Rank { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}