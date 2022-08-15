namespace Common.Model
{
	
	public class PowerRecord
	{
		
		public int Hour { get; set; }

		
		public string Date { get; set; }

	
		public int Load { get; set; }

		
		public string Region { get; set; }

		public string Timestamp { get; set; }

		public PowerRecord()
		{
		}

		public PowerRecord(int hour, string date, int load, string region, string timestamp)
		{
			Hour = hour;
			Date = date;
			Load = load;
			Region = region;
			Timestamp = timestamp;
		}
	}
}