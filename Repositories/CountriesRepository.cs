using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly string _connectionString;

        public CountriesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public IEnumerable<Country> ListCountries()
        {
            var country_ = new List<Country>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("list_country", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            country_.Add(new Country
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),                               
                            });
                        }
                    }
                }
            }
            return country_;
        }
       
    }
}