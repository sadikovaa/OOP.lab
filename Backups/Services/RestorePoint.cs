using System;
using System.Collections.Generic;

namespace Backups.Services
{
    public class RestorePoint
    {
        private DateTime _date;
        private List<string> _pathToZip;

        public RestorePoint(DateTime date, List<string> pathsToZip)
        {
            _date = date;
            _pathToZip = pathsToZip;
        }

        public RestorePoint(DateTime date, string pathToZip)
        {
            _date = date;
            _pathToZip = new List<string> { pathToZip };
        }

        public DateTime GetTime()
        {
            return _date;
        }

        public void SetTime(DateTime date)
        {
            _date = date;
        }

        public List<string> GetPathsToZips()
        {
            return _pathToZip;
        }
    }
}