using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{
    public class VehicleHandler
    {
        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            vehicle = vehicle ?? new Car();
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
