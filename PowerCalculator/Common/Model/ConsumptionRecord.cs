using System;

namespace Common.Model
{
	
	public class ConsumptionRecord
	{
		
		public int Hour { get; set; }

		
		public DateTime Date { get; set; }

		
		public decimal Value { get; set; }

		
		public decimal AbsoluteValue { get; set; }

		public ConsumptionRecord()
		{
		}

		public ConsumptionRecord(int hour, DateTime date, decimal value, decimal absoluteValue)
		{
			Hour = hour;
			Date = date;
			Value = value;
			AbsoluteValue = absoluteValue;
		}
	}
}