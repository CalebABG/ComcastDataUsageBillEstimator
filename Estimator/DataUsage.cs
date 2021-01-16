using System;

namespace Estimator
{
    public class DataUsage
    {
        public DataUsage(string _for, UsageQuality quality = UsageQuality.Low,
            decimal hoursUsedPerDay = 0, StreamingService? streamingService = null)
        {
            For = _for ?? throw new ArgumentNullException(nameof(_for));
            Quality = quality;
            HoursUsedPerDay = hoursUsedPerDay;
            StreamingService = streamingService;
        }

        public string For { get; }
        public UsageQuality Quality { get; }
        public decimal HoursUsedPerDay { get; }
        public StreamingService? StreamingService { get; }
    }
}