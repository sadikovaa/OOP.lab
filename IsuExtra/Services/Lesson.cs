namespace IsuExtra.Services
{
    public class Lesson
    {
        private string _teacher;
        private int _auditorium;

        public Lesson(string teacher, int auditorium)
        {
            _teacher = teacher;
            _auditorium = auditorium;
        }

        public int GetAuditorium()
        {
            return _auditorium;
        }

        public string GetTeacher()
        {
            return _teacher;
        }
    }
}