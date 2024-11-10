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

            TollInfo tollInfo = GetTollInformation();

            var intervalStart = dates[0];
            var maxFee = 60;
            var totalFee = 0;
            var tempFee = 0;

            for (int i = 0; i < dates.Length; i++)
            {
                var nextFee = GetTollFee(dates[i], vehicle, tollInfo);

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

        private int GetTollFee(DateTime date, Vehicle vehicle, TollInfo tollInfo)
        {
            var vehicleHandler = new VehicleHandler();

            if (IsTollFreeDate(date) || vehicleHandler.IsTollFreeVehicle(vehicle)) return 0;

            var tollFee = tollInfo.TollFees.FirstOrDefault(tollFee =>
                date.TimeOfDay >= new TimeSpan(tollFee.StartHour, tollFee.StartMinute, 0) &&
                date.TimeOfDay <= new TimeSpan(tollFee.EndHour, tollFee.EndMinute, 0));

            return tollFee?.Cost ?? 0;
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
    }
}