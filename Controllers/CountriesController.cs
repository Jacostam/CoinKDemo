using Microsoft.AspNetCore.Mvc;
using DemoCoink.Repositories;
using DemoCoink.Models;

namespace DemoCoink.Controllers;

[ApiController]
[Route("[controller]")]
public class CountriesController : ControllerBase
{

    private readonly ICountriesRepository _countriesRepository;

    public CountriesController(ICountriesRepository countriesRepository)
    {
        _countriesRepository = countriesRepository;
    }

    /// <summary>
    /// Obtiene todos los Paises registrados en la base de datos .
    /// </summary>
    /// <returns>Lista de paises.</returns>
    [HttpGet]
    public IActionResult all()
    {
        var countries_ = _countriesRepository.ListCountries();
        return Ok(countries_);
    }
}
