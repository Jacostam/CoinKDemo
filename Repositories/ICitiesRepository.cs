using System.Collections.Generic;

using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public interface ICitiesRepository
    {
        IEnumerable<Cities> ListCities();

        IEnumerable<Cities> ListCitiesDepartment(int id);

    }
}
