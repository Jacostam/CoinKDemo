using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly string _connectionString;

        public CitiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public IEnumerable<Cities> ListCities()
        {
            var cities_ = new List<Cities>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("list_cities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities_.Add(new Cities
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                            });
                        }
                    }
                }
            }
            return cities_;
        }

        public IEnumerable<Cities> ListCitiesDepartment(int id)
        {
            var cities_ = new List<Cities>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("list_cities_department", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                     command.Parameters.AddWithValue("department_id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities_.Add(new Cities
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                DepartmentsId = reader.GetInt32("departments_id"),
                            });
                        }
                    }
                }
            }
            return cities_;
        }
    }

}