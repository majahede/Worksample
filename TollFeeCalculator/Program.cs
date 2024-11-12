using TollFeeCalculator;
using TollFeeCalculator.Vehicles;

TollCalculator tollCalculator = new TollCalculator();
Car car = new Car();

DateTime[] dates = [
    new DateTime(2024, 11, 7, 7, 15, 0), 
    new DateTime(2024, 11, 7, 10, 15, 0), 
    new DateTime(2024, 11, 7, 10, 10, 0), 
    new DateTime(2024, 11, 7, 10, 45, 0),
    new DateTime(2024, 11, 7, 11, 30, 0)
    ];

var fee = tollCalculator.GetDailyTollFee(car, dates);
Console.WriteLine(fee);