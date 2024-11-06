namespace TollFeeCalculator.Models
{
    public class TollFee
    {
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int Cost { get; set; }
    }
}
