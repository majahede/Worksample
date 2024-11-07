using System.Text.Json;
using TollFeeCalculator.Models;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        public int GetDailyTollFee(Vehicle vehicle, DateTime[] dates)
        {
            vehicle = vehicle ?? new Car();

            DateTime intervalStart = dates[0];
            int totalFee = 0;

            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(date, vehicle);
                int tempFee = GetTollFee(intervalStart, vehicle);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            TollInfo tollInfo = GetTollInformation();

            foreach (TollFee tollFee in tollInfo.TollFees)
            {
                var start = new TimeSpan(tollFee.StartHour, tollFee.StartMinute, 0);
                var end = new TimeSpan(tollFee.EndHour, tollFee.EndMinute, 0);

                if (date.TimeOfDay > start && date.TimeOfDay < end)
                {
                    return tollFee.Cost;
                }
            }

            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            TollInfo tollInfo = GetTollInformation();

            if (tollInfo.TollFreeDays.Contains(date.DayOfWeek.ToString()) ||
                tollInfo.TollFreeMonths.Contains(date.Month) ||
                tollInfo.TollFreeDates.Contains(date.Date))
            {
                return true;
            }

            return false;
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