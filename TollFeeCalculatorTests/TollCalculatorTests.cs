using TollFeeCalculator;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class TollCalculatorTests
    {
        private TollCalculator _tollCalculator;
        public TollCalculatorTests()
        {
            _tollCalculator = new TollCalculator();
        }

        [TestMethod]
        public void GetDailyTollFee_FivePassesByCar_ReturnCorrectFee()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2024, 11, 7, 7, 15, 0),
                new DateTime(2024, 11, 7, 10, 15, 0),
                new DateTime(2024, 11, 7, 10, 10, 0),
                new DateTime(2024, 11, 7, 10, 45, 0),
                new DateTime(2024, 11, 7, 11, 30, 0)
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(34, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_FourPassesByCar_ReturnCorrectFee()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2024, 11, 7, 8, 15, 0),
                new DateTime(2024, 11, 7, 10, 10, 0),
                new DateTime(2024, 11, 7, 11, 15, 0),
                new DateTime(2024, 11, 7, 11, 30, 0)
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(29, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_NoDates_ReturnZero()
        {
            var car = new Car();

            DateTime[] dates = [];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(0, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_Motorbike_ReturnZero()
        {
            var motorbike = new Motorbike();

            DateTime[] dates = [
                new DateTime(2024, 11, 12, 7, 15, 0),
                new DateTime(2024, 11, 12, 15, 15, 0),
                new DateTime(2024, 11, 12, 19, 10, 0),
                ];

            var fee = _tollCalculator.GetDailyTollFee(motorbike, dates);

            Assert.AreEqual(0, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_TotalFeeMoreThanMax_ReturnMaxFee()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2024, 11, 12, 7, 15, 0),
                new DateTime(2024, 11, 12, 8, 20, 0),
                new DateTime(2024, 11, 12, 9, 30, 0),
                new DateTime(2024, 11, 12, 10, 40, 0),
                new DateTime(2024, 11, 12, 15, 15, 0),
                new DateTime(2024, 11, 12, 16, 50, 0),
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(60, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_AllPassesInOneHour_ReturnSingleFee()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2024, 11, 12, 7, 15, 0),
                new DateTime(2024, 11, 12, 7, 20, 0),
                new DateTime(2024, 11, 12, 7, 30, 0),
                new DateTime(2024, 11, 12, 7, 40, 0),
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(18, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_TollFreeDate_ReturnZero()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2013, 01, 01, 7, 15, 0),
                new DateTime(2013, 01, 01, 9, 30, 0)
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(0, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_TollFreeMonth_ReturnZero()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2024, 07, 01, 15, 25, 0),
                new DateTime(2024, 07, 01, 17, 15, 0)
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(0, fee);
        }

        [TestMethod]
        public void GetDailyTollFee_Saturday_ReturnZero()
        {
            var car = new Car();

            DateTime[] dates = [
                new DateTime(2024, 11, 9, 7, 15, 0),
                new DateTime(2024, 11, 9, 7, 20, 0),
                ];

            var fee = _tollCalculator.GetDailyTollFee(car, dates);

            Assert.AreEqual(0, fee);
        }
    }
}