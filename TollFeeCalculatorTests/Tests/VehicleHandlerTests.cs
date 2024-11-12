using TollFeeCalculator.Vehicles;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class VehicleHandlerTests
    {
        private VehicleHandler _vehicleHandler;
        public VehicleHandlerTests()
        {
            _vehicleHandler = new VehicleHandler();
        }

        [TestMethod]
        public void IsTollFreeVehicle_Car_ReturnFalse()
        {
            var vehicle = new Car();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsFalse(isTollFree);
        }

        [TestMethod]
        public void IsTollFreeVehicle_Motorbike_ReturnTrue()
        {
            var vehicle = new Motorbike();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsTrue(isTollFree);
        }

        [TestMethod]
        public void IsTollFreeVehicle_Tractor_ReturnTrue()
        {
            var vehicle = new Tractor();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsTrue(isTollFree);
        }

        [TestMethod]
        public void IsTollFreeVehicle_Emergency_ReturnTrue()
        {
            var vehicle = new Emergency();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsTrue(isTollFree);
        }

        [TestMethod]
        public void IsTollFreeVehicle_Foreign_ReturnTrue()
        {
            var vehicle = new Foreign();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsTrue(isTollFree);
        }

        [TestMethod]
        public void IsTollFreeVehicle_Military_ReturnTrue()
        {
            var vehicle = new Military();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsTrue(isTollFree);
        }

        [TestMethod]
        public void IsTollFreeVehicle_Diplomat_ReturnTrue()
        {
            var vehicle = new Diplomat();

            var isTollFree = _vehicleHandler.IsTollFreeVehicle(vehicle);

            Assert.IsTrue(isTollFree);
        }
    }
}
