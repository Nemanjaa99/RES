using Common.Model;
using Dapper;
using DatabaseAccess.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DatabaseAccess
{
	public class SqliteDataAccess : ISqliteDataAccess
	{
	
		public List<PowerRecord> LoadRecords(string table)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				var output = cnn.Query<PowerRecord>($"select * from {table}", new DynamicParameters());
				return output.ToList();
			}
		}

		
		public List<string> LoadRegions(string table)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				var output = cnn.Query<string>($"select distinct Region from {table}", new DynamicParameters());
				return output.ToList();
			}
		}

		
		public void SaveRecords(List<PowerRecord> powerRecords, string table)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				foreach (var powerRecord in powerRecords)
				{
					cnn.Execute($"insert into {table} (Hour, Date, Load, Region, Timestamp) values (@Hour, @Date, @Load, @Region, @Timestamp)", powerRecord);
				}
			}
		}

		
		private string LoadConnectionString(string id = "Default")
		{
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}
	}
}