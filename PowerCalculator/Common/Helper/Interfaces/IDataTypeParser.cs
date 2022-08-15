using System;

namespace Common.Helper.Interfaces
{
	public interface IDataTypeParser
	{
		DateTime ConvertDateTimeFromString(string data);

		int ConvertIntFromString(string data);

		string GetDateFromFileName(string path);

		string GetFileNameFromPath(string path);
	}
}