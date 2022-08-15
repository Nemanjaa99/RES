using System;
using System.IO;

namespace Common.Logging
{
	
	public static class Logger
	{
		
		public static void Log(Operation operation, string msg)
		{
			
			string path = "../../../../Logs/log.txt";

			
			using (StreamWriter streamWriter = new StreamWriter(path, true))
			{
				
				string modifiedMessage = $"Type: {operation}, Time: {DateTime.Now}, Message: {msg}";
				streamWriter.WriteLine(modifiedMessage);
				streamWriter.Close();
			}
		}
	}

	public enum Operation
	{
		Info = 0,
		Warning = 1,
		Error = 2
	}
}