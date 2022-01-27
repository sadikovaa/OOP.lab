using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Service;
using Server.Service.Reports;
using Server.Service.Staff;
using Task = Server.Service.Tasks.Task;

namespace Server.Controllers
{
    [ApiController]
    [Route("/controller")]
    public class Controller : ControllerBase
    {
        private Logic _logic = new Logic();

        [HttpGet("employees/all")]

        public List<Employee> GetAllEmployees()
        {
            return _logic.GetAllEmployees();
        }

        [HttpGet("employees/id")]
        public Employee GetById([FromQuery] int id)
        {
            return _logic.GetEmployeeById(id);
        }

        [HttpPost("employees/new")]
        public void AddEmployee([FromQuery] string name)
        {
            _logic.AddEmployee(name);
        }

        [HttpPut("employees/employees")]
        public void Update([FromQuery] int employerId,[FromBody] List<int> employeesId)
        {
            _logic.UpdateEmployees(employerId, employeesId);
        }
        
        [HttpGet("tasks/all")]
        public List<Task> GetAllTasks()
        {
            return _logic.GetAllTask();
        }

        [HttpGet("tasks/id")]
        public Task GetTaskById(int id)
        {
            return _logic.GetTaskById(id);
        }

        [HttpPost("tasks/new")]
        public void AddNewTask([FromQuery] int employeeId, string comment)
        {
            _logic.AddNewTask(employeeId, comment);
        }

        [HttpPut("tasks/newcomment")]
        public void AddNewCommentToTask([FromQuery] int taskId, string comment)
        {
            _logic.AddCommentOfTask(taskId, comment);
        }

        [HttpPut("tasks/newemployee")]
        public void ChangeTaskEmployee([FromQuery] int taskId, int newEmployeeId)
        {
            _logic.ChangeTaskEmployee(newEmployeeId, taskId);
        }

        [HttpPut("tasks/finish")]
        public void FinishTask([FromQuery] int taskId)
        {
            _logic.FinishTask(taskId);
        }

        [HttpGet("reports/all")]
        public List<Task> GetAllReports()
        {
            return _logic.GetAllTask();
        }

        [HttpGet("report/id")]
        public Report GetReportById(int id)
        {
            return _logic.GetReportById(id);
        }

        [HttpPost("reports/new")]
        public void CreateReport([FromQuery] int employeeId)
        {
            _logic.CreateReport(employeeId);
        }

        [HttpPut("reports/upreport")]
        public void UpdateReport([FromBody] Report report)
        {
            _logic.UpdateReport(report);
        }

        [HttpPut("reports/emplreport")]
        public void UpdateReportOfEmployeeWork([FromQuery]int employerId)
        {
            _logic.UpdateReportOfEmployeeWork(employerId);
        }

        [HttpPut("reports/finish")]
        public void FinishReport([FromQuery] int employeeId)
        {
            _logic.FinishReport(employeeId);
        }
    }
}