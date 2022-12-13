using System.Collections.Generic;
using System.ComponentModel;

namespace Transport.Infrastructure.Data.Entities
{



    public class Rank
    {
        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }


        public List<Employee> Employees { get; set; }
    }
}