namespace Banks.Conditions
{
    public class ConditionForPercent
    {
        private double _lowerSumThreshold;
        private double _percent;

        public ConditionForPercent(double lowerSumThreshold, double percent)
        {
            _lowerSumThreshold = lowerSumThreshold;
            _percent = percent;
        }

        public double GetLowerSumThreshold()
        {
            return _lowerSumThreshold;
        }

        public double Percent()
        {
            return _percent;
        }
    }
}