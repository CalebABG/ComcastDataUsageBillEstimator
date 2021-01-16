using System;
using System.Collections.Generic;

namespace Estimator
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var netflix = new StreamingService("Netflix", new()
            {
                [UsageQuality.Low] = new() { RatePerHourInGb = 0.3m },
                [UsageQuality.Med] = new() { RatePerHourInGb = 0.7m },
                [UsageQuality.High] = new() { RatePerHourInGb = 3.0m },
            });

            var youtubeTv = new StreamingService("YoutubeTV", new()
            {
                [UsageQuality.Low] = new() { RatePerHourInGb = 0.5m },
                [UsageQuality.Med] = new() { RatePerHourInGb = 1.5m },
                [UsageQuality.High] = new() { RatePerHourInGb = 3.0m },
            });

            var primeVideo = new StreamingService("Prime Video", new()
            {
                [UsageQuality.Low] = new() { RatePerHourInGb = 0.38m },
                [UsageQuality.Med] = new() { RatePerHourInGb = 1.4m },
                [UsageQuality.High] = new() { RatePerHourInGb = 6.84m },
            });

            var tv1 = new Device("TV 1", new()
            {
                new DataUsage("Streaming Youtube TV", UsageQuality.Med, 7.5m, youtubeTv)
            });

            var tv2 = new Device("TV 2", new()
            {
                new DataUsage("Streaming Youtube TV", UsageQuality.Med, 8.5m, youtubeTv)
            });

            var tv3 = new Device("TV 3", new()
            {
                new DataUsage("Streaming Youtube TV", UsageQuality.Med, 1.5m, youtubeTv)
            });

            var laptop1 = new Device("Laptop 1", dataInGbUsedPerDay: 6);
            var laptop2 = new Device("Laptop 2", dataInGbUsedPerDay: 5);
            var laptop3 = new Device("Laptop 3", dataInGbUsedPerDay: 5);

            var estimator = new Estimator.UsageEstimator(new List<Device>
            {
                tv1, tv2, tv3, laptop1, laptop2, laptop3
            });

            Console.WriteLine(estimator.GenerateReport());
        }
    }
}