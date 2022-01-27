using System.Collections.Generic;
using NUnit.Framework;
using IsuExtra.Services;
using IsuExtra.Tools;
using Isu.Services;
using Isu.Tools;


namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IIsuExtraService _isuExtraService;
        private List<Student> _mainCharacters;
        [SetUp]
        public void Setup()
        {
            _mainCharacters = new List<Student>();
            IIsuService _isuService = new Isu.Services.Isu();
            Group group1 = _isuService.AddGroup(new GroupName(new CourseNumber(1), 3, 'M'));
            Group group2 = _isuService.AddGroup(new GroupName(new CourseNumber(1), 4, 'H'));
            Group group3 = _isuService.AddGroup(new GroupName(new CourseNumber(3), 3, 'K'));
            _mainCharacters.Add( _isuService.AddStudent(group1, "NoName1"));
            _mainCharacters.Add(_isuService.AddStudent(group2, "NoName2"));
            _mainCharacters.Add(_isuService.AddStudent(group3, "NoName3"));
            _isuExtraService = new IsuExtraService(_isuService);
        }

        [Test]
        public void SignUpStudent_StudentHasSignedUp()
        {
            Discipline discipline1 = _isuExtraService.AddNewOgnpDiscipline("CiberBez", 'H');
            Discipline discipline2 = _isuExtraService.AddNewOgnpDiscipline("Inovatica", 'K');
            Stream streamOfDiscipline1 = discipline1.AddStream(new Stream(30, new Schedule()));
            Stream streamOfDiscipline2 = discipline2.AddStream(new Stream(30, new Schedule()));
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(1), 3, 'M'), new Schedule());
            _isuExtraService.SignUpForOgnp(_mainCharacters[0], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            Assert.True(_isuExtraService.GetDiscipline(discipline1.GetId()).IsStudentSignedUp(_mainCharacters[0]));
            Assert.True(_isuExtraService.GetDiscipline(discipline2.GetId()).IsStudentSignedUp(_mainCharacters[0]));
        }
        
        [Test]
        public void SignUpStudentOnTheSameMegafaculty()
        {
            Discipline discipline1 = _isuExtraService.AddNewOgnpDiscipline("Matan", 'M');
            Discipline discipline2 = _isuExtraService.AddNewOgnpDiscipline("Inovatica", 'K');
            Stream streamOfDiscipline1 = discipline1.AddStream(new Stream(30, new Schedule()));
            Stream streamOfDiscipline2 = discipline2.AddStream(new Stream(30, new Schedule()));
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(1), 3, 'M'), new Schedule());
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.SignUpForOgnp(_mainCharacters[0], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            });

        }
        
        [Test]
        public void SignUpStudent_NoEmptyPlaces()
        {
            Discipline discipline1 = _isuExtraService.AddNewOgnpDiscipline("Komunic", 'P');
            Discipline discipline2 = _isuExtraService.AddNewOgnpDiscipline("Draw", 'D');
            Stream streamOfDiscipline1 = discipline1.AddStream(new Stream(2, new Schedule()));
            Stream streamOfDiscipline2 = discipline2.AddStream(new Stream(2, new Schedule()));
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(1), 3, 'M'), new Schedule());
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(1), 4, 'H'), new Schedule());
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(3), 3, 'K'), new Schedule());
            _isuExtraService.SignUpForOgnp(_mainCharacters[2], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            _isuExtraService.SignUpForOgnp(_mainCharacters[1], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.SignUpForOgnp(_mainCharacters[0], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            });
        }
        
        [Test]
        public void SignUpStudent_SchedulesAreCrossing()
        {
            Schedule currSchedule = new Schedule();
            currSchedule.AddLesson(new Lesson("VozianovaAV", 302), 1, 2);
            currSchedule.AddLesson(new Lesson("VozianovaAV", 302), 3, 2);
            Discipline discipline1 = _isuExtraService.AddNewOgnpDiscipline("CiberBez", 'H');
            Discipline discipline2 = _isuExtraService.AddNewOgnpDiscipline("Inovatica", 'K');
            Stream streamOfDiscipline1 = discipline1.AddStream(new Stream(30, currSchedule));
            Stream streamOfDiscipline2 = discipline2.AddStream(new Stream(30, new Schedule()));
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(1), 3, 'M'), currSchedule);
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.SignUpForOgnp(_mainCharacters[0], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            });
        }
        
        [Test]
        public void SignOutStudent_StudentHasSignedOut()
        {
            Discipline discipline1 = _isuExtraService.AddNewOgnpDiscipline("CiberBez", 'H');
            Discipline discipline2 = _isuExtraService.AddNewOgnpDiscipline("Inovatica", 'K');
            Stream streamOfDiscipline1 = discipline1.AddStream(new Stream(30, new Schedule()));
            Stream streamOfDiscipline2 = discipline2.AddStream(new Stream(30, new Schedule()));
            _isuExtraService.SetSchedulesofGroup(new GroupName(new CourseNumber(1), 3, 'M'), new Schedule());
            _isuExtraService.SignUpForOgnp(_mainCharacters[0], new List<DisciplineAndStreamId>(){new DisciplineAndStreamId(discipline1.GetId(), streamOfDiscipline1.GetId()), new DisciplineAndStreamId( discipline2.GetId(), streamOfDiscipline2.GetId())});
            _isuExtraService.SignOutFromOgnp(_mainCharacters[0]);
            List<Student> unsignedStudents = _isuExtraService.GetUnsignedStudents();
            foreach (Student currStudent in unsignedStudents)
            {
                if (currStudent.GetId() == _mainCharacters[0].GetId())
                {
                    return;
                }
            }

            Assert.Fail();
        }
    }
}