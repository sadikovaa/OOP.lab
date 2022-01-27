using System;

namespace Server.Service.Tools
{
    public class Change
    {
        public DateTime Time { get; set; }
        public TypeOfChanges TypeOfChanges { get; set; }
        public string AdditionalInfo { get; set; }

        public Change(TypeOfChanges typeOfChanges)
        {
            Time = DateTime.Now;
            TypeOfChanges = typeOfChanges;
        }
        
        public Change(TypeOfChanges typeOfChanges, string additionalInfo)
        {
            Time = DateTime.Now;
            TypeOfChanges = typeOfChanges;
            AdditionalInfo = additionalInfo;
        }
    }
}