using System.Collections.Generic;

using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public interface ICountriesRepository
    {
        IEnumerable<Country> ListCountries();
    }
}
