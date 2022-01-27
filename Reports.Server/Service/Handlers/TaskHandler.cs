using System.Collections.Generic;
using Server.Service.Reports;
using Server.Service.Staff;
using Server.Service.Tasks;
using Server.Service.Tools;
using Server.ServiceRepository;

namespace Server.Service.Handlers
{
    public class TaskHandler
    {
        private Repository<Task> _repository;

        public TaskHandler(Repository<Task> repository)
        {
            _repository = repository;
        }

        public List<Task> GetAllTasks()
        {
            return _repository.GetList();
        }

        public Task GetById(int id)
        {
            List<Task> tasks = _repository.GetList();
            return tasks.Find(task => task.Id == id);
        }

        public List<Task> GetTasksOfEmployee(Employee employee)
        {
            var tasksOfEmployee = new List<Task>();
            List<Task> allTasks = _repository.GetList();
            foreach (Task task in allTasks)
            {
                foreach (int taskId in employee.Tasks)
                {
                    if (task.Id == taskId)
                    {
                        tasksOfEmployee.Add(task);
                    }
                }
            }

            return tasksOfEmployee;
        }

        public void AddNewTask(Task task)
        {
            List<Task> tasks = _repository.GetList();
            tasks.Add(task);
            _repository.SetList(tasks);
        }
        
        public void ChangeConditionOfTask(int id, Condition condition)
        {
            Task task = GetById(id);
            task.Condition = condition;
            task.Changes.Add(new Change((condition == Condition.Active) ? TypeOfChanges.TaskOpened : TypeOfChanges.TaskResolved));
        }

        public void AddCommentOfTask(int id, string comment)
        {
            Task task = GetById(id);
            task.Changes.Add(new Change(TypeOfChanges.NewComment, comment));
        }

        public void ChangeTaskEmployee(int taskId, int newEmployeeId)
        {
            Task task = GetById(taskId);
            task.EmployerId = newEmployeeId;
            task.Changes.Add(new Change(TypeOfChanges.NewEmployer, newEmployeeId.ToString()));
        }
    }
}