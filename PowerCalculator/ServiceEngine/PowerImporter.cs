using Common.FileUpload.Interfaces;
using Common.Model;
using DatabaseAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace ServiceEngine
{
	public class PowerImporter
	{
		private IFileDialog fileDialog;
		private ISqliteDataAccess sqliteDataAccess;

		public PowerImporter(IFileDialog fileDialog, ISqliteDataAccess sqliteDataAccess)
		{
			this.fileDialog = fileDialog;
			this.sqliteDataAccess = sqliteDataAccess;
		}

		
		public void CollectData(string expetedPowerPath, string actualPowerPath)
		{
			List<PowerRecord> expetedPowerImporters = fileDialog.Load(expetedPowerPath);
			List<PowerRecord> actualPowerImporters = fileDialog.Load(actualPowerPath);

			ValidatePowerConsumtionTimePeriods(expetedPowerImporters);
			ValidatePowerConsumtionTimePeriods(actualPowerImporters);

			ValidateFilesCount(expetedPowerImporters, actualPowerImporters);
			ValidateDataFromFiles(expetedPowerImporters, actualPowerImporters);

			
			sqliteDataAccess.SaveRecords(expetedPowerImporters, "ExpetedConsumption");
			sqliteDataAccess.SaveRecords(actualPowerImporters, "ActualConsumption");
		}

		
		public void ValidatePowerConsumtionTimePeriods(List<PowerRecord> powerRecords)
		{
			int counter = 1;
			string currentRegion = powerRecords[0].Region;

			for (int i = 1; i < powerRecords.Count; i++)
			{
				if (powerRecords[i].Region != currentRegion)
				{
					CheckTimePeriodsNumber(counter);

					counter = 1;
					currentRegion = powerRecords[i].Region;
				}
				else
				{
					counter++;
				}
			}
		}

		
		public void CheckTimePeriodsNumber(int numberOfHoursByRegion)
		{
			if (numberOfHoursByRegion < 23 || numberOfHoursByRegion > 25)
			{
				throw new Exception("Import files must have 23, 24 or 25 hours per region!");
			}
		}

		
		public void ValidateFilesCount(List<PowerRecord> expeted, List<PowerRecord> actual)
		{
			if (expeted.Count != actual.Count)
			{
				throw new Exception("Import files don't have equal data rows, please import valid files!");
			}
		}

		
		public void ValidateDataFromFiles(List<PowerRecord> expeted, List<PowerRecord> actual)
		{
			for (int i = 0; i < expeted.Count; i++)
			{
				if (expeted[i].Hour != actual[i].Hour || expeted[i].Region != actual[i].Region)
				{
					throw new Exception("Files are inconsistent, please import valid files!");
				}
			}
		}
	}
}