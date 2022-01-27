using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class Schedule
    {
        private Dictionary<int, Lesson> _lessons;

        public Schedule()
        {
            _lessons = new Dictionary<int, Lesson>();
        }

        public Lesson AddLesson(Lesson newLesson, int day, int number)
        {
            int key = KeyOfDayAndNumber(day, number);

            // _lessons.Add(key, newLesson);
            _lessons[key] = newLesson;
            return _lessons[key];
        }

        public void DeleteLesson(int day, int number)
        {
            _lessons.Remove(KeyOfDayAndNumber(day, number));
        }

        public bool IsThereALesson(int day, int number)
        {
            return _lessons.ContainsKey(KeyOfDayAndNumber(day, number));
        }

        private int KeyOfDayAndNumber(int day, int number)
        {
            if (day > Constans.DayInWeek || day < 0 || number < 0 || number > Constans.LessonsInDay)
            {
                throw new IsuExtraException("Week day or time out of range!");
            }

            int key = (day * 10) + number;
            return key;
        }
    }
}