using TollFeeCalculator;
using TollFeeCalculator.Vehicles;

TollCalculator tollCalculator = new TollCalculator();
Car car = new Car();

DateTime[] a = [
    new DateTime(2024, 11, 7, 7, 15, 0), 
    new DateTime(2024, 11, 7, 7, 15, 0), 
    new DateTime(2024, 11, 7, 10, 17, 0), 
    new DateTime(2024, 11, 7, 10, 45, 0),
    new DateTime(2024, 11, 7, 10, 45, 0)
    ];

var fee = tollCalculator.GetDailyTollFee(car, a);
Console.WriteLine(fee);