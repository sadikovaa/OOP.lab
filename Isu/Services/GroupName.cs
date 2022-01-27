using System;
using Isu.Tools;

namespace Isu.Services
{
    public class GroupName
    {
        private string _name;
        public GroupName(CourseNumber course_number, int group_number, char megafaculty)
        {
            if (IsValidCourse(group_number))
            {
                throw new IsuException("Incorrect group Number");
            }

            if (ConsistsOfTwoDigits(group_number))
            {
                _name = megafaculty + "3" + course_number.GetNumber() + group_number;
            }
            else
            {
                _name = megafaculty + "3" + course_number.GetNumber() + "0" + group_number;
            }
        }

        public string GetName()
        {
            return _name;
        }

        public CourseNumber GetCourseNumber()
        {
            return new CourseNumber(Convert.ToInt32(_name[2]) - Convert.ToInt32('0')); // была ошибка
        }

        private bool IsValidCourse(int group_number_)
        {
            return group_number_ > Constans.MaxGroupNumber || group_number_ < Constans.MinGroupNumber;
        }

        private bool ConsistsOfTwoDigits(int group_number_)
        {
            return group_number_ > Constans.MaxDigit;
        }
    }
}