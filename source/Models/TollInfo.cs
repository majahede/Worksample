namespace TollFeeCalculator.Models
{
    public class TollInfo
    {
        public List<string> TollFreeDays { get; set; }
        public List<int> TollFreeMonths { get; set; }
        public List<DateTime> TollFreeDates { get; set; }
    }
}
