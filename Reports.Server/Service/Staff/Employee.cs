using System.Collections.Generic;

namespace Server.Service.Staff
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Employees { get; set; }
        public int CurReportId{ get; set; }
        public List<int> Tasks{ get; set; }

        public Employee(string name)
        {
            Id = StaffIdGenerator.GetId();
            Name = name;
            Employees = new List<int>();
            Tasks = new List<int>();
            CurReportId = -1;
        }
    }
}