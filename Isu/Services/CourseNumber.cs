using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        private int _number;
        public CourseNumber(int number)
        {
            if (number >= Constans.MinCourse && number <= Constans.MaxCourse)
            {
                _number = number;
            }
            else
            {
                throw new IsuException("Incorrect course number");
            }
        }

        public int GetNumber()
        {
            return _number;
        }
    }
}