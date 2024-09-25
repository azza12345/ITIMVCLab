namespace MVCLab.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Image { get; set; }
        public decimal Salary { get; set; }
        public int? CompanyId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        
        public virtual Company? Company { get; set; }

    }
}
