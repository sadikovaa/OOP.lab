using System.Collections.Generic;
using Isu.Services;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class Discipline
    {
        private int _id;
        private List<Stream> _streams;
        private string _name;
        private char _megafaculty;

        public Discipline(string name, char megafaculty)
        {
            _id = DisciplineIdGenerator.GetId();
            _streams = new List<Stream>();
            _name = name;
            _megafaculty = megafaculty;
        }

        public void DeleteStudent(Student student)
        {
            GetStudentStream(student).DeleteStudent(student);
        }

        public Stream AddStream(Stream newStream)
        {
            _streams.Add(newStream);
            return newStream;
        }

        public Student AddStudent(Student student, int sreamId)
        {
            SearchStream(sreamId).AddStudent(student);
            return student;
        }

        public string GetName()
        {
            return _name;
        }

        public char GetMegafaculty()
        {
            return _megafaculty;
        }

        public Stream SearchStream(int streamId)
        {
            foreach (Stream currStream in _streams)
            {
                if (currStream.GetId() == streamId)
                {
                    return currStream;
                }
            }

            throw new IsuExtraException("The stream was not found!");
        }

        public List<Stream> GetStreams()
        {
            return _streams;
        }

        public int GetId()
        {
            return _id;
        }

        public bool IsStudentSignedUp(Student student)
        {
            foreach (Stream currStream in _streams)
            {
                if (currStream.IsStudentSignedUp(student))
                {
                    return true;
                }
            }

            return false;
        }

        public Stream GetStudentStream(Student student)
        {
            foreach (Stream currStream in _streams)
            {
                if (currStream.IsStudentSignedUp(student))
                {
                    return currStream;
                }
            }

            throw new IsuExtraException("The student did not sign up!");
        }
    }
}