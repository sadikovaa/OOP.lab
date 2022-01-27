using System.Collections.Generic;
using Server.Service.Reports;
using Server.ServiceRepository;

namespace Server.Service.Handlers
{
    public class ReportHandler
    {
        private Repository<Report> _repository;
        
        public ReportHandler(Repository<Report> repository)
        {
            _repository = repository;
        }

        public List<Report> GetAllReports()
        {
            List<Report> reports = _repository.GetList();
            return reports;
        }
        public Report GetById(int id)
        {
            List<Report> reports = _repository.GetList();
            return reports.Find(report => report.Id == id);
        }

        public List<Report> GetEmployeeReport(int employeeId)
        {
            List<Report> reports = _repository.GetList();
            return reports.FindAll(report => report.EmployerId == employeeId);
        }

        public void AddReport(Report report)
        {
            List<Report> reports = _repository.GetList();
            reports.Add(report);
            _repository.SetList(reports);
        }

        public void DeleteReport(int id)
        {
            List<Report> reports = _repository.GetList();
            reports.Remove(GetById(id));
            _repository.SetList(reports);
        }

        public void Clear()
        {
            _repository.SetList(new List<Report>());
        }

        public void Update(Report report)
        {
            List<Report> employees = _repository.GetList();
            int index = employees.FindIndex(e => e.Id == report.Id);
            employees[index] = report;
        }
        public void ChangeReportCondition(int reportId, Condition condition)
        {
            Report report = GetById(reportId);
            report.Condition = condition;
        }

        public void AddTasksToReport(int reportId, List<int> tasksId)
        {
            Report report = GetById(reportId);
            report.Tasks.AddRange(tasksId);
        }
    }
}