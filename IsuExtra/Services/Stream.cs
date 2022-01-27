using System.Collections.Generic;
using Isu.Services;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class Stream
    {
        private int _id;
        private Schedule _schedule;
        private int _numberOfPlaces;
        private List<Student> _students;

        public Stream(int numberOfPlaces)
        {
            _id = StreamIdGenerator.GetId();
            _schedule = null;
            _numberOfPlaces = numberOfPlaces;
            _students = new List<Student>();
        }

        public Stream(int numberOfPlaces, Schedule schedule)
        {
            _id = StreamIdGenerator.GetId();
            _schedule = schedule;
            _numberOfPlaces = numberOfPlaces;
            _students = new List<Student>();
        }

        public Student AddStudent(Student student)
        {
            if (_numberOfPlaces < 1)
            {
                throw new IsuExtraException("The are no empty places!");
            }

            _numberOfPlaces--;
            _students.Add(student);
            return student;
        }

        public void DeleteStudent(Student student)
        {
            _students.Remove(student);
            _numberOfPlaces++;
        }

        public int GetNubmerOfFreePlaces()
        {
            return _numberOfPlaces;
        }

        public Schedule GetSchedule()
        {
            return _schedule;
        }

        public int GetId()
        {
            return _id;
        }

        public List<Student> GetListStudents()
        {
            return _students;
        }

        public void ChangeSchedue(Schedule newSchedule)
        {
            _schedule = newSchedule;
        }

        public bool IsStudentSignedUp(Student student)
        {
            return _students.Find(thisStudent => thisStudent.GetId() == student.GetId()) != null;
        }
    }
}