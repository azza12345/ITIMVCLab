namespace MVCLab.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateStart { get; set; }
        public string Address { get; set; }

        
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
