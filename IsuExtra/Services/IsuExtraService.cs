using System;
using System.Collections.Generic;
using Isu.Services;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<Discipline> _disciplinesForChoose = new List<Discipline>();
        private IIsuService _isuService;
        private List<ScheduleOfGroup> _groupsSchedules = new List<ScheduleOfGroup>();

        public IsuExtraService()
        {
            _isuService = new Isu.Services.Isu();
        }

        public IsuExtraService(IIsuService isuService)
        {
            _isuService = isuService;
        }

        public Discipline GetDiscipline(int disciplineId)
        {
            foreach (Discipline currDiscipline in _disciplinesForChoose)
            {
                if (currDiscipline.GetId() == disciplineId)
                {
                    return currDiscipline;
                }
            }

            throw new IsuExtraException("The discipline was not found!");
        }

        public void SetSchedulesofGroup(GroupName groupName, Schedule schedule)
        {
            _groupsSchedules.Add(new ScheduleOfGroup(groupName, schedule));
        }

        public Discipline AddNewOgnpDiscipline(string name, char megafaculty)
        {
            _disciplinesForChoose.Add(new Discipline(name, megafaculty));
            return _disciplinesForChoose[_disciplinesForChoose.Count - 1];
        }

        public void SignUpForOgnp(Student currStudent,  List<DisciplineAndStreamId> disciplineAndStreams)
        {
            if (disciplineAndStreams.Count != Constans.NumberOfDisciplines)
            {
                throw new IsuExtraException("Incorrect number of discipline!");
            }

            GroupName groupName = SearchStudentsGroupName(currStudent);
            foreach (DisciplineAndStreamId currDisciplineAndStreamId in disciplineAndStreams)
            {
                SignUpForDiscipline(currStudent, groupName, currDisciplineAndStreamId.GetDisciplineId(), currDisciplineAndStreamId.GetStreamId());
            }
        }

        public void SignOutFromOgnp(Student currStudent)
        {
            foreach (Discipline currDiscipline in _disciplinesForChoose)
            {
                if (currDiscipline.IsStudentSignedUp(currStudent))
                {
                    currDiscipline.DeleteStudent(currStudent);
                }
            }
        }

        public List<Stream> GetStreamsOfOgnp(int disciplineId)
        {
            foreach (Discipline currDiscipline in _disciplinesForChoose)
            {
                if (currDiscipline.GetId() == disciplineId)
                {
                    return currDiscipline.GetStreams();
                }
            }

            throw new IsuExtraException("The discipline was not found!");
        }

        public List<Student> GetStudentsOfOgnpStream(int disciplineId, int streamId)
        {
            foreach (Discipline currDiscipline in _disciplinesForChoose)
            {
                if (currDiscipline.GetId() == disciplineId)
                {
                    return currDiscipline.SearchStream(streamId).GetListStudents();
                }
            }

            throw new IsuExtraException("Discipline or stream were not found!");
        }

        public List<Student> GetUnsignedStudents()
        {
            List<Student> unsignedStudents = new List<Student>();
            for (int currCourse = Isu.Tools.Constans.MinCourse; currCourse <= Isu.Tools.Constans.MaxCourse; currCourse++)
            {
                List<Student> currCoursesStudents = _isuService.FindStudents(new CourseNumber(currCourse));
                foreach (Student currStudent in currCoursesStudents)
                {
                    if (!IsStudentSignedUp(currStudent))
                    {
                        unsignedStudents.Add(currStudent);
                    }
                }
            }

            return unsignedStudents;
        }

        private GroupName SearchStudentsGroupName(Student student)
        {
            for (int currCourse = Isu.Tools.Constans.MinCourse; currCourse <= Isu.Tools.Constans.MaxCourse; currCourse++)
            {
                var courseNumber = new CourseNumber(currCourse);
                List<Group> currCourseGroups = _isuService.FindGroups(courseNumber);
                foreach (Group currGroup in currCourseGroups)
                {
                    if (currGroup.FindStudentById(student.GetId()) != null)
                    {
                        return currGroup.GetGroupName();
                    }
                }
            }

            throw new IsuExtraException("There is no student!");
        }

        private Schedule SearchScheduleOfGroup(GroupName groupName)
        {
            foreach (var groupNameAndSchedule in _groupsSchedules)
            {
                if (groupNameAndSchedule.GetGroupName() == groupName.GetName())
                    return groupNameAndSchedule.GetSchedule();
            }

            return null;
        }

        private void SignUpForDiscipline(Student currStudent, GroupName groupName, int disciplineId, int streamId)
        {
            foreach (Discipline currDiscipline in _disciplinesForChoose)
            {
                if (currDiscipline.GetId() == disciplineId)
                {
                    if (groupName.GetName()[0] == currDiscipline.GetMegafaculty())
                    {
                        throw new IsuExtraException("Student is from the same megafaculty!");
                    }

                    Stream currSteam = currDiscipline.SearchStream(streamId);
                    if (HasAnyCrossingInSchedules(SearchScheduleOfGroup(groupName), currSteam.GetSchedule()))
                    {
                        throw new IsuExtraException("Schedules is crossing!");
                    }

                    currSteam.AddStudent(currStudent);
                    return;
                }
            }

            throw new IsuExtraException("Student failed to sign up!");
        }

        private bool HasAnyCrossingInSchedules(Schedule scheduleOfGroup, Schedule scheduleOfDiscipline)
        {
            for (int i = 0; i < Constans.DayInWeek; i++)
            {
                for (int j = 0; j < Constans.LessonsInDay; j++)
                {
                    if (scheduleOfGroup.IsThereALesson(i, j) && scheduleOfDiscipline.IsThereALesson(i, j))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsStudentSignedUp(Student student)
        {
            foreach (Discipline discipline in _disciplinesForChoose)
            {
                if (discipline.IsStudentSignedUp(student))
                {
                    return true;
                }
            }

            return false;
        }
    }
}