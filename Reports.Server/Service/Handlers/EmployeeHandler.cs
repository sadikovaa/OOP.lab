using System.Collections.Generic;
using Server.Service.Staff;
using Server.ServiceRepository;

namespace Server.Service.Handlers
{
    public class EmployeeHandler
    {
        private Repository<Employee> _repository;

        public EmployeeHandler(Repository<Employee> repository)
        {
            _repository = repository;
        }
        public List<Employee> GetAllEmployees()
        {
            return _repository.GetList();
        }
        public Employee GetById( int id)
        {
            List<Employee> employees = _repository.GetList();
            return employees.Find(employee => employee.Id == id);
        }

        public void AddEmployee(string name)
        {
            List<Employee> employees = _repository.GetList();
            employees.Add(new Employee(name));
            _repository.SetList(employees);
        }

        public void DeleteEmployee(int id)
        {
            List<Employee> employees = _repository.GetList();
            employees.Remove(GetById(id));
            _repository.SetList(employees);
        }

        public void Clear()
        {
            _repository.SetList(new List<Employee>());
        }

        public void Update(Employee employee)
        {
            List<Employee> employees = _repository.GetList();
            int index = employees.FindIndex(e => e.Id == employee.Id);
            employees[index] = employee;
        }

        public void UpdateEmployees(int employeeId, List<int> employees)
        {
            Employee employee = GetById(employeeId);
            employee.Employees = employees;
        }
        
        public void AddTaskToEmployee(int employeeId, int taskId)
        {
            Employee employee = GetById(employeeId);
            employee.Tasks.Add(taskId);
        }
        public void DeleteTaskEmployee(int employeeId, int taskId)
        {
            Employee employee = GetById(employeeId);
            employee.Tasks.Remove(taskId);
        }
        
        public void SetReport(int employeeId, int reportId)
        {
            Employee employee = GetById(employeeId);
            employee.CurReportId = reportId;
        }

        public void DeleteReport(int employeeId)
        {
            Employee employee = GetById(employeeId);
            employee.CurReportId = -1;
        }

        public void DeleteAllReports()
        {
            List<Employee> employees = _repository.GetList();
            foreach (Employee employee in employees)
            {
                employee.CurReportId = -1;
            }
        }
    }
}