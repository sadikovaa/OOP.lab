using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
        : IEquatable<Group>
    {
        private List<Student> _students;
        private GroupName _group_name;
        public Group(GroupName name)
        {
            _group_name = name;
            _students = new List<Student>();
        }

        public GroupName GetGroupName()
        {
            return _group_name;
        }

        public Student FindStudentById(int id)
        {
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].GetId() == id)
                    return _students[i];
            }

            return null;
        }

        public Student FindStudentByName(string name)
        {
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].GetName() == name)
                    return _students[i];
            }

            return null;
        }

        public void AddAllStudentsToList(ref List<Student> students)
        {
            students.AddRange(_students);
        }

        public int CountStudents()
        {
            return _students.Count;
        }

        public Student AddNewStudent(string name)
        {
            Student student = new Student(name);
            AddStudent(student);
            return student;
        }

        public void AddStudent(Student student)
        {
            if (_students.Count >= Constans.MaxNumberOfStudents)
            {
                throw new IsuException("Reach max student per group");
            }

            _students.Add(student);
        }

        public CourseNumber CourseNumber()
        {
            return _group_name.GetCourseNumber();
        }

        public void DeleteStudent(int id)
        {
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].GetId() == id)
                {
                    _students.RemoveAt(i);
                }
            }
        }

        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_group_name.GetName(), other._group_name.GetName());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Group)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_group_name);
        }
    }
}