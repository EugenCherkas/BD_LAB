using System.Collections.Generic;
using Lab3_.ViewModels;

namespace Lab3_.Services
{
    public interface ITransportService
    {
        HomeViewModel GetHomeViewModel(string cacheKey);
        List<string> GetRanks();

        List<EmployeeViewModel> SearchEmployees(string rank, string lastname);
    }
}