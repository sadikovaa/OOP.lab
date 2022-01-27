using System.Linq;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new Services.Isu();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            _isuService.AddGroup(new GroupName(new CourseNumber(1), 3, 'M'));
            _isuService.AddStudent(new Group(new GroupName(new CourseNumber(1), 3, 'M')), "DKholopov");
            if (_isuService.IsIsuEmpty())
            {
                Assert.Fail("There are not groups!");
            }

            if (_isuService.FindStudent("DKholopov") == null)
            {
                Assert.Fail("There are no students!");
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup(new GroupName(new CourseNumber(1), 3, 'M'));
                for (int i = 0; i < 32; i++)
                {
                    _isuService.AddStudent(group, "StudentS");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group1 = _isuService.AddGroup(new GroupName(new CourseNumber(5), 3, 'M'));
            });
            Assert.Catch<IsuException>(() =>
            {
                Group group2 = _isuService.AddGroup(new GroupName(new CourseNumber(1), 100, 'M'));
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                var group = _isuService.AddGroup(new GroupName(new CourseNumber(1), 3, 'M'));
                Student student_ = _isuService.AddStudent(group, "DKholopov");
                _isuService.ChangeStudentGroup(student_, new Group(new GroupName(new CourseNumber(2),3, 'M')));
            });
        }
    }
}