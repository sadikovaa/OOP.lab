using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Discipline AddNewOgnpDiscipline(string name, char megafaculty);
        void SignUpForOgnp(Student currStudent, List<DisciplineAndStreamId> disciplineAndStreams);
        void SignOutFromOgnp(Student currStudent);
        List<Stream> GetStreamsOfOgnp(int disciplineId);
        List<Student> GetStudentsOfOgnpStream(int disciplineId, int streamId);
        List<Student> GetUnsignedStudents();

        Discipline GetDiscipline(int disciplineId);
        void SetSchedulesofGroup(GroupName groupName, Schedule schedule);
    }
}