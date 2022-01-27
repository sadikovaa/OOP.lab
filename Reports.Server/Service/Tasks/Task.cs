using System.Collections.Generic;
using Server.Service.Reports;
using Server.Service.Tools;

namespace Server.Service.Tasks
{
    public class Task
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public string Comment { get; set; }
        public List<Change> Changes { get; set; } 
        public Condition Condition { get; set; }

        public Task(int employerId, string comment)
        {
            Id = TaskIdGenerator.GetId();
            EmployerId = employerId;
            Comment = comment;
            Changes = new List<Change>();
            Condition = Condition.Active;
            Changes.Add(new Change(TypeOfChanges.TaskOpened));
            Changes.Add(new Change(TypeOfChanges.NewEmployer, employerId.ToString()));
            Changes.Add(new Change(TypeOfChanges.NewComment, comment));
        }
    }
}