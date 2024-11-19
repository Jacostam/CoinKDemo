using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public IEnumerable<Users> ListUser()
        {
            var users_ = new List<Users>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("list_users", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users_.Add(new Users
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Phone = reader.GetString("phone"),
                                CityId = reader.GetInt32("city_id"),
                                Address = reader.GetString("address"),
                                City = reader.GetString("city"),
                                CountryId = reader.GetInt32("country_id"),
                                Country = reader.GetString("country"),
                                DepartmentId = reader.GetInt32("department_id"),
                                Department = reader.GetString("department"),
                            });
                        }
                    }
                }
            }
            return users_;
        }
        public Dictionary<string, string> Create(Users User)
        {
            var result = new Dictionary<string, string>();
            var Message = "Usuario registrado exitosamente";
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("create_user", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("name", User.Name);
                        command.Parameters.AddWithValue("phone", User.Phone);
                        command.Parameters.AddWithValue("city_id", User.CityId);
                        command.Parameters.AddWithValue("address", User.Address);
                        command.ExecuteNonQuery();
                        result["status"] = "200";  // Código de estado de éxito
                        result["message"] = "Usuario registrado exitosamente.";
                    }
                }
            }
            catch (MySqlException ex)
            {
                result["status"] = "200";  // Código de estado de éxito
                result["message"] = ex.Message;
                //Message = ex.Message;

            }
            Console.WriteLine("Error : " + Message);
            return result;
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("delete_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("user_id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}