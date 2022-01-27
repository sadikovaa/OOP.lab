using System.Collections.Generic;

namespace Banks.Conditions
{
    public class DepositCondition
    {
        private readonly List<ConditionForPercent> _conditionForPercents;

        public DepositCondition(List<ConditionForPercent> conditionForPercents)
        {
            _conditionForPercents = conditionForPercents;
        }

        public double GetPercent(double initialSum)
        {
            double percent = 0;
            foreach (var curr in _conditionForPercents)
            {
                if (initialSum < curr.GetLowerSumThreshold())
                {
                    percent = curr.Percent();
                }
            }

            return percent;
        }
    }
}