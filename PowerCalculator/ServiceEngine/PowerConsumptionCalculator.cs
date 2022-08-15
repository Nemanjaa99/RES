using Common.Helper.Interfaces;
using Common.Model;
using DatabaseAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceEngine
{
	public class PowerConsumptionCalculator
	{
		private ISqliteDataAccess sqliteDataAccess;
		private IDataTypeParser dataTypeParser;
		private IMathHelper mathHelper;

		public PowerConsumptionCalculator(ISqliteDataAccess sqliteDataAccess, IDataTypeParser dataTypeParser, IMathHelper mathHelper)
		{
			this.sqliteDataAccess = sqliteDataAccess;
			this.dataTypeParser = dataTypeParser;
			this.mathHelper = mathHelper;
		}

		
		public OutputModel CalculatePower(DateTime from, DateTime to, string region)
		{
			List<PowerRecord> expetedPowerImporters = sqliteDataAccess.LoadRecords("ExpetedConsumption");
			List<PowerRecord> actualPowerImporters = sqliteDataAccess.LoadRecords("ActualConsumption");

			expetedPowerImporters = CheckRegionRange(from, to, region, expetedPowerImporters);
			actualPowerImporters = CheckRegionRange(from, to, region, actualPowerImporters);

			List<CalculatedPower> calculatedPowers = CalculateAbsoluteValues(expetedPowerImporters, actualPowerImporters);

			decimal calculatedMeanDeviation = CalculateAbsoluteMeanDeviation(calculatedPowers);
			double calculatedSquareDeviation = CalculateSquareDeviation(calculatedPowers);

			return new OutputModel(calculatedPowers, calculatedMeanDeviation, calculatedSquareDeviation);
		}

		
		public List<PowerRecord> CheckRegionRange(DateTime from, DateTime to, string region, List<PowerRecord> powerRecords)
		{
			List<PowerRecord> output = new List<PowerRecord>();

			foreach (var powerRecord in powerRecords)
			{
				DateTime date = dataTypeParser.ConvertDateTimeFromString(powerRecord.Date);

				if (date >= from && date <= to && powerRecord.Region == region)
				{
					output.Add(powerRecord);
				}
			}

			return output;
		}

		
		public List<CalculatedPower> CalculateAbsoluteValues(List<PowerRecord> expeted, List<PowerRecord> actual)
		{
			List<CalculatedPower> output = new List<CalculatedPower>();

			for (int i = 0; i < expeted.Count; i++)
			{
				DateTime date = dataTypeParser.ConvertDateTimeFromString(expeted[i].Date);
				decimal averageValue = mathHelper.CalculateValuePerHour(expeted[i].Load, actual[i].Load);
				decimal averageAbsoluteValue = mathHelper.CalculateAbsoluteValuePerHour(averageValue);

				CalculatedPower item = output.FirstOrDefault(x => x.Date == date);
				if (item != null)
				{
					item.AverageRecords.Add(new ConsumptionRecord(expeted[i].Hour, date, averageValue, averageAbsoluteValue));
				}
				else
				{
					output.Add(new CalculatedPower(date, new List<ConsumptionRecord>()
					{
						new ConsumptionRecord(expeted[i].Hour, date, averageValue, averageAbsoluteValue)
					}));
				}
			}

			return output;
		}

	
		public decimal CalculateAbsoluteMeanDeviation(List<CalculatedPower> absoluteValues)
		{
			decimal sum = 0;
			int counter = 0;

			foreach (var absoluteValue in absoluteValues)
			{
				foreach (var averageRecord in absoluteValue.AverageRecords)
				{
					sum += averageRecord.AbsoluteValue;
					counter++;
				}
			}

		
			return mathHelper.CalculateDivisionValue(sum, counter);
		}

		
		public double CalculateSquareDeviation(List<CalculatedPower> absoluteValues)
		{
			decimal sum = 0;
			int counter = 0;

			foreach (var absoluteValue in absoluteValues)
			{
				foreach (var averageRecord in absoluteValue.AverageRecords)
				{
					sum += averageRecord.AbsoluteValue;
					counter++;
				}
			}

		
			sum = mathHelper.CalculateDivisionValue(sum, counter);

			
			sum = mathHelper.CalculateSquareValue(sum);

			
			return mathHelper.CalculateRootOfValue(sum);
		}
	}
}