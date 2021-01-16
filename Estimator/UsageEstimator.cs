using System;
using System.Collections.Generic;
using System.Text;

namespace Estimator
{
    public class UsageEstimator
    {
        public UsageEstimator(List<Device> devices)
        {
            Devices = devices ?? throw new ArgumentNullException(nameof(devices));
        }

        public List<Device> Devices { get; set; }

        public decimal OverageCostPerMonthInDollars { get; } = 10;
        public decimal OverageThresholdPerMonthInGb { get; } = 50;
        public decimal OverageChargeCapPerMonthInDollars { get; } = 100;
        public decimal DataAllottedPerMonthInGb { get; } = 1200;

        public decimal CalculateTotalDataUsageInGbPerMonth()
        {
            var today = DateTime.Now;
            // var daysPerMonth = 30;
            var daysPerMonth = DateTime.DaysInMonth(today.Year, today.Month);

            var totalDeviceDataUsage = default(decimal);

            foreach (var device in Devices)
                totalDeviceDataUsage += device.CalculateUsageInGbPerDay();

            return daysPerMonth * totalDeviceDataUsage;
        }

        public string GenerateReport()
        {
            var builder = new StringBuilder(500);

            var totalDataUsageInGbForMonth = CalculateTotalDataUsageInGbPerMonth();

            var dataOverages = totalDataUsageInGbForMonth - DataAllottedPerMonthInGb;

            builder.AppendLine($"Total Data Usage: {totalDataUsageInGbForMonth} (GB)");

            if (dataOverages <= 0)
            {
                builder.AppendLine("No Data Overage Charges!");
            }
            else
            {
                builder.AppendLine($"Data Overages: {dataOverages} (GB)");

                var overageCount = Math.Floor(dataOverages / OverageThresholdPerMonthInGb);
                builder.AppendLine($"Overage Count: {overageCount}x");

                var totalOverageCharge = overageCount * OverageCostPerMonthInDollars;
                var actualOverageCharge = totalOverageCharge > OverageChargeCapPerMonthInDollars
                    ? OverageChargeCapPerMonthInDollars
                    : totalOverageCharge;

                builder.AppendLine($"Data Overage Charges: Total: (${totalOverageCharge})");
                builder.Append(
                    $"Data Overage Charges Breakdown:\n  - Actual Overage Charge: ${actualOverageCharge} = ");
                builder.Append(
                    $"(Overage Cost = ${OverageCostPerMonthInDollars}) x (# of {OverageThresholdPerMonthInGb} GB used = {overageCount} [Overage Data: {dataOverages}GB])");
            }

            return builder.ToString();
        }
    }
}