using System.Collections.Generic;
using Server.Service.Handlers;
using Server.Service.Reports;
using Server.Service.Staff;
using Server.Service.Tasks;
using Server.ServiceRepository;

namespace Server.Service
{
    public class Logic
    {
        private EmployeeHandler _employees;
        private TaskHandler _tasks;
        private ReportHandler _reports;

        public Logic(string pathEmployees = "employee.json", string pathTasks = "task.json", string pathReports = "report.json")
        {
            _employees = new EmployeeHandler(new Repository<Employee>(pathEmployees));
            _tasks = new TaskHandler(new Repository<Task>(pathTasks));
            _reports = new ReportHandler(new Repository<Report>(pathReports));
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees.GetAllEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.GetById(id);
        }

        public List<Task> GetAllTask()
        {
            return _tasks.GetAllTasks();
        }

        public Task GetTaskById(int id)
        {
            return _tasks.GetById(id);
        }

        public List<Report> GetAllReports()
        {
            return _reports.GetAllReports();
        }

        public Report GetReportById(int id)
        {
            return _reports.GetById(id);
        }

        public void AddEmployee(string name)
        {
            _employees.AddEmployee(name);
        }

        public void UpdateEmployees(int employeeId, List<int> employees)
        {
            _employees.UpdateEmployees(employeeId, employees);
        }
        
        public void AddNewTask(int employeeId, string comment)
        {
            Task task = new Task(employeeId, comment);
            _employees.AddTaskToEmployee(employeeId, task.Id);
            _tasks.AddNewTask(task);
        }

        public void ChangeTaskEmployee(int newEmployeeId, int taskId)
        {
            int lastEmployeeId = _tasks.GetById(taskId).EmployerId;
            _tasks.ChangeTaskEmployee(taskId, newEmployeeId);
            _employees.AddTaskToEmployee(newEmployeeId, taskId);
            _employees.DeleteTaskEmployee(lastEmployeeId, taskId);
        }

        public void AddCommentOfTask(int taskId, string comment)
        {
            _tasks.AddCommentOfTask(taskId, comment);
        }

        public void FinishTask(int id)
        {
            _tasks.ChangeConditionOfTask(id, Condition.Finished);
        }

        public void CreateReport(int employeeId)
        {
            Report report = new Report(employeeId);
            _reports.AddReport(report);
            _employees.SetReport(employeeId, report.Id);
            _reports.AddTasksToReport(report.Id, _employees.GetById(employeeId).Tasks);
        }

        public void UpdateReportOfEmployeeWork(int employerId)
        {
            Employee employer = _employees.GetById(employerId);
            foreach (var employee in employer.Employees)
            {
                _reports.AddTasksToReport(employerId, _reports.GetById(_employees.GetById(employee).CurReportId).Tasks);
            }
        }
        public void UpdateReport(Report report)
        {
            _reports.Update(report);
        }

        public void FinishReport(int employeeId)
        {
            int reportId = _employees.GetById(employeeId).CurReportId;
            _employees.DeleteReport(employeeId);
            _reports.ChangeReportCondition(reportId, Condition.Finished);
        }
    }
}