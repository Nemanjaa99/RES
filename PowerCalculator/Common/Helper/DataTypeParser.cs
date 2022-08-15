using Common.Helper.Interfaces;
using System;

namespace Common.Helper
{
	public class DataTypeParser : IDataTypeParser
	{
		
		public int ConvertIntFromString(string data)
		{
			try
			{
				return int.Parse(data);
			}
			catch
			{
				throw new Exception("Failed to convert int type from string type");
			}
		}

		
		public DateTime ConvertDateTimeFromString(string data)
		{
			try
			{
				return DateTime.Parse(data);
			}
			catch
			{
				throw new Exception("Failed to convert date time type from string type");
			}
		}

		public string GetDateFromFileName(string path)
		{
		
			string fileName = GetFileNameFromPath(path);

			
			string[] part = fileName.Split('_');

			return new DateTime(ConvertIntFromString(part[1]), ConvertIntFromString(part[2]), ConvertIntFromString(part[3])).ToString();
		}

		
		public string GetFileNameFromPath(string path)
		{
			
			string[] pathParts = path.Split('\\');

			
			string[] fileParts = pathParts[pathParts.Length - 1].Split('.');

			
			return fileParts[0];
		}
	}
}