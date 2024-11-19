namespace DemoCoink.Models
{
    public class Users
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  string Phone { get; set; }
        public  string? Country { get; set; }
        public int CountryId { get; set; }
        public int DepartmentId { get; set; }
        public string? Department { get; set; }
        public int CityId { get; set; }
        public string? City { get; set; }
        public  string Address { get; set; }   
       // public Cities Cities { get; set; }    
    }
}
