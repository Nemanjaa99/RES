using System;
using System.Collections.Generic;

namespace Common.Model
{
	public class CalculatedPower
	{
		
		public DateTime Date { get; set; }


		public List<ConsumptionRecord> AverageRecords { get; set; }

		public CalculatedPower()
		{
		}

		public CalculatedPower(DateTime date, List<ConsumptionRecord> averageRecords)
		{
			Date = date;
			AverageRecords = averageRecords;
		}
	}
}