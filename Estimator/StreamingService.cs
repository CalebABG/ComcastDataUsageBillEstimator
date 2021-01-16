using System;
using System.Collections.Generic;
using static Estimator.Utilities;

namespace Estimator
{
    public class StreamingService
    {
        public StreamingService(string name, Dictionary<UsageQuality, UsageRating>? usageRatings = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UsageRatings = usageRatings;
        }

        public string Name { get; }
        public Dictionary<UsageQuality, UsageRating>? UsageRatings { get; }

        public bool AddUsageRating(UsageQuality quality, UsageRating rating)
        {
            if (UsageRatings is null) return false;
            return AddToDictionary(quality, rating, UsageRatings);
        }

        public bool RemoveUsageRating(UsageQuality quality)
        {
            if (UsageRatings is null) return false;
            return RemoveFromDictionary(quality, UsageRatings);
        }
    }
}