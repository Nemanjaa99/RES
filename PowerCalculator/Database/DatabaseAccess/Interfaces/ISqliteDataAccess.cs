using Common.Model;
using System.Collections.Generic;

namespace DatabaseAccess.Interfaces
{
	public interface ISqliteDataAccess
	{
		List<PowerRecord> LoadRecords(string table);

		List<string> LoadRegions(string table);

		void SaveRecords(List<PowerRecord> powerRecords, string table);
	}
}