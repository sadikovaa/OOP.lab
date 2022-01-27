namespace BackupsExtra.Logging
{
    public interface ITypeOfLogging
    {
        void RecordReport(string report, bool isNeedTimeCode);
    }
}