using System.Collections.Generic;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(GroupName name);
        Student AddStudent(Group group, string name);
        public bool IsIsuEmpty();
        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(GroupName groupName);
        List<Student> FindStudents(CourseNumber courseNumber);

        Group FindGroup(string groupName);
        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}