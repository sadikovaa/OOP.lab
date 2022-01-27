using System.Collections.Generic;
using Backups.Services;
using Backups.Tools;

namespace BackupsExtra.Cleaning
{
    public class Merge : IStrategy
    {
        public List<RestorePoint> Update(List<RestorePoint> newPoints, List<RestorePoint> lastPoints)
        {
            foreach (var point in lastPoints)
            {
                RestorePoint curPoint = MergeTwoPoints(newPoints[0], point);
                newPoints[0] = curPoint;
            }

            return newPoints;
        }

        private RestorePoint MergeTwoPoints(RestorePoint newPoint, RestorePoint lastPoint)
        {
            List<string> listOfLastObjects = lastPoint.GetPathsToZips();
            List<string> listOfNewObjects = newPoint.GetPathsToZips();
            var listOfObjects = new List<string>();
            foreach (string lastObject in listOfLastObjects)
            {
                bool isObjectHere = false;
                foreach (string newObject in listOfNewObjects)
                {
                    if (GetNameOfObject(lastObject) == GetNameOfObject(newObject))
                    {
                        isObjectHere = true;
                        listOfObjects.Add(newObject);
                    }
                }

                if (!isObjectHere)
                {
                    listOfObjects.Add(lastObject);
                }
            }

            return new RestorePoint(newPoint.GetTime(), listOfObjects);
        }

        private string GetNameOfObject(string nameOfFile)
        {
            string nameOfPoint = FileInfo.GetNameOfFile(nameOfFile);
            int index = nameOfPoint.LastIndexOf('_');
            return nameOfPoint.Substring(index + 1);
        }
    }
}