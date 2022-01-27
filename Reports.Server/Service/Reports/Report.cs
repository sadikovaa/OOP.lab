using System;
using System.Collections.Generic;

namespace Server.Service.Reports
{
    public class Report
    {
        public DateTime Date { get; set; }
        public int Id{ get; set; }
        public int EmployerId{ get; set; }
        public List<int> Tasks { get; set; }
        public Condition Condition { get; set; }

        public Report(int employerId)
        {
            Date = DateTime.Now;
            Id = ReportIdGenerator.GetId();
            EmployerId = employerId;
            Condition = Condition.Active;
            Tasks = new List<int>();
        }
    }
}