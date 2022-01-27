using Isu.Services;

namespace IsuExtra.Services
{
    public class ScheduleOfGroup
    {
        private GroupName _groupName;
        private Schedule _schedule;

        public ScheduleOfGroup(GroupName groupName, Schedule schedule)
        {
            _groupName = groupName;
            _schedule = schedule;
        }

        public string GetGroupName()
        {
            return _groupName.GetName();
        }

        public Schedule GetSchedule()
        {
            return _schedule;
        }
    }
}