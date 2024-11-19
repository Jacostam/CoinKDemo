namespace DemoCoink.Models
{
    public class Cities
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public int DepartmentsId { get; set; }
        public Departments? Department { get; set; }   
    }
}
