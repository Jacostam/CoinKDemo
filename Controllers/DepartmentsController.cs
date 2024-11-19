using Microsoft.AspNetCore.Mvc;
using DemoCoink.Repositories;
using DemoCoink.Models;

namespace DemoCoink.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{

    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentsController(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    /// <summary>
    /// Obtiene todos los Departamentos registrados en la base de datos .
    /// </summary>
    /// <returns>Lista de departamentos.</returns>
    [HttpGet]
    public IActionResult all()
    {
        var countries_ = _departmentsRepository.ListDepartment();
        return Ok(countries_);
    }

    /// <summary>
    /// Obtiene todos los Departamentos registrados a una Pais {{id}} en la base de datos
    /// </summary>
    /// <param name="id">ID del pais.</param>
    /// <returns>Lista de departamentos.</returns>
    [HttpGet("{id}")]
    public IActionResult allCountry(int id)
    {
        var countries_ = _departmentsRepository.ListDepartmentCountry(id);
        return Ok(countries_);
    }
}
