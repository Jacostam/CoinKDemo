using Microsoft.AspNetCore.Mvc;
using DemoCoink.Repositories;
using DemoCoink.Models;

namespace DemoCoink.Controllers;

[ApiController]
[Route("[controller]")]
public class CitiesController : ControllerBase
{

    private readonly ICitiesRepository _citiesRepository;

    public CitiesController(ICitiesRepository citiesRepository)
    {
        _citiesRepository = citiesRepository;
    }

    /// <summary>
    /// Obtiene todos los Ciudades registrados en la base de datos .
    /// </summary>
    /// <returns>Lista de Ciudades.</returns>
    [HttpGet]
    public IActionResult all()
    {
        var countries_ = _citiesRepository.ListCities();
        return Ok(countries_);
    }
    /// <summary>
    /// Obtiene todos las Ciudades registrados a una Departamento {{id}} en la base de datos
    /// </summary>
    /// <param name="id">ID del Departamento.</param>
    /// <returns>Lista de Ciudades.</returns>
    /// 
    [HttpGet("{id}")]
    public IActionResult allCountry(int id)
    {
        var countries_ = _citiesRepository.ListCitiesDepartment(id);
        return Ok(countries_);
    }
}
