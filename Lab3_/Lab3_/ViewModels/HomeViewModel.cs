using System.Collections.Generic;
using Transport.Infrastructure.Data.Entities;

namespace Lab3_.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Rank> Ranks { get; set; }
        public IEnumerable<TransportType> TransportTypes { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}