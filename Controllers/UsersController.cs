using Microsoft.AspNetCore.Mvc;
using DemoCoink.Repositories;
using DemoCoink.Models;

namespace DemoCoink.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly IUsersRepository _userRepository;

    public UsersController(IUsersRepository useroRepository)
    {
        _userRepository = useroRepository;
    }

    /// <summary>
    /// Obtiene todos los usuarios registrados en la base de datos con estado 1.
    /// </summary>
    /// <returns>Lista de usuarios.</returns>
    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var users_ = _userRepository.ListUser();
        return Ok(users_);
    }

    /// <summary>
    /// Registra un nuevo usuario en la base de datos.
    /// </summary>
    /// <param name="user_">Objeto Usuario que contiene la información del nuevo usuario.</param>
    /// <returns>Mensaje indicando si el registro fue exitoso.</returns>
    [HttpPost]
    public IActionResult Create([FromBody] Users user_)
    {
       var results = new Dictionary<string, string>();
        if (user_ == null)
            return BadRequest();

        results=_userRepository.Create(user_);
        return Ok(new {results});
    }

    /// <summary>
    /// Elimina logicamente un usuario de la base de datos. Cambia su estado a 0
    /// </summary>
    /// <param name="id">ID del usuario a eliminar.</param>
    /// <returns>Mensaje indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userRepository.Delete(id);
        return Ok(new { mensaje = "Usuario eliminado exitosamente" });
    }
}
