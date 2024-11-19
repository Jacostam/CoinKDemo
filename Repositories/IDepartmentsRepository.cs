using System.Collections.Generic;

using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public interface IDepartmentsRepository
    {
        IEnumerable<Departments> ListDepartment();

        IEnumerable<Departments> ListDepartmentCountry(int id);

    }
}
