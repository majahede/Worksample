using System.Text.Json;
using TollFeeCalculator.Models;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        public int GetDailyTollFee(Vehicle vehicle, DateTime[] dates)
        {
            if (dates == null || dates.Length == 0) return 0;

            var intervalStart = dates[0];
            var maxFee = 60;
            var totalFee = 0;
            var tempFee = 0;

            for (int i = 0; i < dates.Length; i++)
            {

                var nextFee = GetTollFee(dates[i], vehicle);

                if (IsWithinHour(dates[i], intervalStart))
                {
                    tempFee = Math.Max(tempFee, nextFee);
                }
                else
                {
                    totalFee += tempFee;
                    intervalStart = dates[i];
                    tempFee = nextFee;
                }
            }

            totalFee += tempFee;

            return totalFee > maxFee ? maxFee : totalFee;
        }

        private int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            TollInfo tollInfo = GetTollInformation();

            foreach (TollFee tollFee in tollInfo.TollFees)
            {
                var start = new TimeSpan(tollFee.StartHour, tollFee.StartMinute, 0);
                var end = new TimeSpan(tollFee.EndHour, tollFee.EndMinute, 0);

                if (date.TimeOfDay >= start && date.TimeOfDay <= end)
                {
                    return tollFee.Cost;
                }
            }

            return 0;
        }

        private bool IsWithinHour(DateTime date, DateTime intervalStart)
        {
            var minutes = date.Subtract(intervalStart).TotalMinutes;
            return minutes <= 60;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            TollInfo tollInfo = GetTollInformation();

            return tollInfo.TollFreeDays.Contains(date.DayOfWeek.ToString()) ||
                   tollInfo.TollFreeMonths.Contains(date.Month) ||
                   tollInfo.TollFreeDates.Contains(date.Date);
        }

        private TollInfo GetTollInformation()
        {
            var path = "TollFeeInformation.json";
            var jsonString = File.ReadAllText(path);
            TollInfo tollInfo = JsonSerializer.Deserialize<TollInfo>(jsonString)!;

            return tollInfo;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            return Enum.IsDefined(typeof(TollFreeVehicles), vehicle.GetVehicleType());
        }

        private enum TollFreeVehicles
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}