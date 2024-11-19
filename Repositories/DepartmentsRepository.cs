using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly string _connectionString;

        public DepartmentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public IEnumerable<Departments> ListDepartment()
        {
            var country_ = new List<Departments>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("list_departments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            country_.Add(new Departments
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),   
                                countryId = reader.GetInt32("country_id"),                             
                            });
                        }
                    }
                }
            }
            return country_;
        }

         public IEnumerable<Departments> ListDepartmentCountry(int id)
        {
            var country_ = new List<Departments>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("list_departments_country", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id_country", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            country_.Add(new Departments
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),   
                                countryId = reader.GetInt32("country_id"),                             
                            });
                        }
                    }
                }
            }
            return country_;
        }
    }
      
}