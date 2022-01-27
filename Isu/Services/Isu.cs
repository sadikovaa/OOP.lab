using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Isu : IIsuService
    {
        private List<Group> _groups;

        public Isu()
        {
            _groups = new List<Group>();
        }

        public bool IsIsuEmpty()
        {
            return _groups.Count == 0;
        }

        public Group AddGroup(GroupName name)
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].GetGroupName() == name)
                {
                    return _groups[i];
                }
            }

            _groups.Add(new Group(name));
            return _groups[_groups.Count - 1];
        }

        public Student AddStudent(Group group, string name)
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].Equals(group))
                {
                    return _groups[i].AddNewStudent(name);
                }
            }

            throw new IsuException("The Group does not exist");
        }

        public Student GetStudent(int id)
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                Student student = _groups[i].FindStudentById(id);
                if (student != null)
                {
                    return student;
                }
            }

            throw new IsuException("The student does not exist");
        }

        public Student FindStudent(string name)
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                for (int j = 0; j < _groups[i].CountStudents(); j++)
                {
                    Student student = _groups[i].FindStudentByName(name);
                    if (student != null)
                        return student;
                }
            }

            return null;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].GetGroupName().GetName() == groupName.GetName())
                {
                    List<Student> answer = new List<Student>();
                    _groups[i].AddAllStudentsToList(ref answer);
                    return answer;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> students = new List<Student>();
            for (int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].CourseNumber().GetNumber() == courseNumber.GetNumber())
                {
                    _groups[i].AddAllStudentsToList(ref students);
                }
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].GetGroupName().GetName() == groupName)
                {
                    return _groups[i];
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> groups = new List<Group>();
            for (int i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].CourseNumber().GetNumber() == courseNumber.GetNumber())
                {
                    groups.Add(_groups[i]);
                }
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            int ii = -1, jj = -1;
            for (int i = 0; i < _groups.Count; i++)
            {
                var currentGroup = _groups[i];
                var currentStudent = currentGroup.FindStudentById(student.GetId());
                if (currentStudent != null)
                {
                    ii = i;
                }

                if (_groups[i].Equals(newGroup))
                {
                    jj = i;
                }
            }

            if (ii != -1 && jj != -1)
            {
                _groups[jj].AddStudent(student);
                _groups[ii].DeleteStudent(student.GetId());
            }
            else
            {
                throw new IsuException("The student does not change the group");
            }
        }
    }
}