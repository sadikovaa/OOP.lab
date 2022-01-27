using Isu.Services;

namespace IsuExtra.Services
{
    public class DisciplineAndStreamId
    {
        private int _discipline;
        private int _stream;

        public DisciplineAndStreamId(int discipline, int stream)
        {
            _discipline = discipline;
            _stream = stream;
        }

        public int GetDisciplineId()
        {
            return _discipline;
        }

        public int GetStreamId()
        {
            return _stream;
        }
    }
}