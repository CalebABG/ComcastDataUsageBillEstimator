using System;
using System.Collections.Generic;
using System.Linq;
using static Estimator.Utilities;

namespace Estimator
{
    public class Device
    {
        public Device(string name, List<DataUsage>? dataUsages = null, decimal dataInGbUsedPerDay = 0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataUsages = dataUsages?.ToDictionary(u => u.For, u => u);
            DataInGbUsedPerDay = dataInGbUsedPerDay;
        }

        public string Name { get; }
        public decimal DataInGbUsedPerDay { get; }
        public Dictionary<string, DataUsage>? DataUsages { get; }

        public bool AddDataUsage(DataUsage dataUsage)
        {
            if (DataUsages is null) return false;
            return AddToDictionary(dataUsage.For, dataUsage, DataUsages);
        }

        public bool RemoveDataUsage(string _for)
        {
            if (DataUsages is null) return false;
            return RemoveFromDictionary(_for, DataUsages);
        }

        public decimal CalculateUsageInGbPerDay()
        {
            var total = default(decimal);

            if (DataUsages is not null)
            {
                foreach (var dataUsage in DataUsages.Values)
                {
                    if (dataUsage.StreamingService?.UsageRatings is null) 
                        throw new NullReferenceException($"Either {nameof(dataUsage.StreamingService)} or " +
                                                         $"{nameof(dataUsage.StreamingService.UsageRatings)} is null");

                    var rating = dataUsage.StreamingService.UsageRatings[dataUsage.Quality];

                    total += rating.RatePerHourInGb * dataUsage.HoursUsedPerDay;
                }
            }

            return total + DataInGbUsedPerDay;
        }
    }
}