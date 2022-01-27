using System;

namespace Isu.Services
{
    public class Student
        : IEquatable<Student>
    {
        private int _id;
        private string _name;
        public Student(string name)
        {
            _name = name;
            _id = IdGenerator.GetId();
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public bool Equals(Student other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id && _name == other._name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Student)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _name);
        }
    }
}