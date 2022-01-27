using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Services;
using Backups.TypeOfStorage;
using BackupsExtra.Cleaning;
using BackupsExtra.ExtraFileManager;
using BackupsExtra.Service;

namespace BackupsExtra.Tools
{
    public static class FileSavingState
    {
        public static void SaveState(BackupsExtraService backups, string path)
        {
            File.Create(path);
            File.AppendAllText(path, backups.GetDirectory());
            File.AppendAllText(path, string.Join(" ", backups.GetJobObjects().ToArray()));
            List<RestorePoint> restorePoints = backups.GetRestorePoints();
            foreach (var zip in restorePoints)
            {
                File.AppendAllText(path, CreateRestorePointString(zip));
            }
        }

        public static BackupsExtraService UpdateState(string pathToFileOfState, IStorage typeOfStorage, IExtraFileManager fileSystem, IAlgorithmForCleaning clear, bool needTime)
        {
            string[] lines = File.ReadAllLines(pathToFileOfState);
            var jobObjects = lines[1].Split(" ").ToList();
            var restorePoints = new List<RestorePoint>();
            for (int i = 2; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(" ");
                var time = DateTime.Parse(words[0]);
                var paths = new List<string>();
                for (int j = 1; j < words.Length; j++)
                {
                    paths.Add(words[j]);
                }

                restorePoints.Add(new RestorePoint(time, paths));
            }

            var backups = new BackupsExtraService(typeOfStorage, lines[0], fileSystem, clear, needTime);
            foreach (var jobObject in jobObjects)
            {
                backups.AddObject(jobObject);
            }

            backups.SetRestorePoints(restorePoints);
            return backups;
        }

        private static string CreateRestorePointString(RestorePoint restorePoint)
        {
            return restorePoint.GetTime() + string.Join(" ", restorePoint.GetPathsToZips().ToArray());
        }
    }
}