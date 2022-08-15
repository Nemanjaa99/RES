using Common.Helper.Interfaces;
using Common.Model;
using DatabaseAccess.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ServiceEngine.Test
{
	public class PowerConsumptionCalculatorTest
	{
		private ISqliteDataAccess sqliteDataAccess;
		private IDataTypeParser dataTypeParser;
		private IMathHelper mathHelper;

		private PowerConsumptionCalculator powerConsumptionCalculator;

		[SetUp]
		public void SetUp()
		{
			sqliteDataAccess = Substitute.For<ISqliteDataAccess>();
			dataTypeParser = Substitute.For<IDataTypeParser>();
			mathHelper = Substitute.For<IMathHelper>();

			powerConsumptionCalculator = new PowerConsumptionCalculator(sqliteDataAccess, dataTypeParser, mathHelper);
		}

		[Test]
		public void CalculatePower_CallsTheMethod_ReturnsOutputModel()
		{
			OutputModel actual = powerConsumptionCalculator.CalculatePower(new DateTime(2021, 5, 5), new DateTime(2021, 5, 10),
				"VOJ");
		}

		[Test]
		public void CheckRegionRange_CallsTheMethod_ReturnsList()
		{
			List<PowerRecord> powerRecords = new List<PowerRecord>()
			{
				new PowerRecord(1, "6-5-2021", 4000, "VOJ", "22-8-2021")
			};

			List<PowerRecord> actual = powerConsumptionCalculator.CheckRegionRange(new DateTime(1, 1, 1), new DateTime(1, 1, 1),
				"VOJ", powerRecords);

			Assert.AreEqual(actual.Count, 1);
		}

		[Test]
		public void CalculateAbsoluteValues_CallsTheMethod_ReturnsList()
		{
			List<PowerRecord> expeted = new List<PowerRecord>()
			{
				new PowerRecord(1, "6-5-2021", 4000, "VOJ", "22-8-2021")
			};

			List<PowerRecord> actual = new List<PowerRecord>()
			{
				new PowerRecord(1, "6-5-2021", 4500, "VOJ", "22-8-2021")
			};

			List<CalculatedPower> output = powerConsumptionCalculator.CalculateAbsoluteValues(expeted, actual);

			Assert.AreEqual(output.Count, 1);
		}

		[Test]
		public void CalculateAbsoluteMeanDeviation_CallsTheMethod_ReturnsVoid()
		{
			List<CalculatedPower> absoluteValues = new List<CalculatedPower>()
			{
				new CalculatedPower(new DateTime(2021, 5, 6), new List<ConsumptionRecord>()
				{
					new ConsumptionRecord(1, new DateTime(2021, 5, 6), 50.55m, 50.55m),
					new ConsumptionRecord(2, new DateTime(2021, 5, 6), 100, 100)
				})
			};

			decimal output = powerConsumptionCalculator.CalculateAbsoluteMeanDeviation(absoluteValues);

			mathHelper.
				Received().
				CalculateDivisionValue(150.55m, 2);
		}

		[Test]
		public void CalculateSquareDeviation_CallsTheMethod_ReturnsVoid()
		{
			List<CalculatedPower> absoluteValues = new List<CalculatedPower>()
			{
				new CalculatedPower(new DateTime(2021, 5, 6), new List<ConsumptionRecord>()
				{
					new ConsumptionRecord(1, new DateTime(2021, 5, 6), 50.55m, 50.55m),
					new ConsumptionRecord(2, new DateTime(2021, 5, 6), 100, 100)
				})
			};

			double output = powerConsumptionCalculator.CalculateSquareDeviation(absoluteValues);

			mathHelper.
				Received().
				CalculateDivisionValue(150.55m, 2);

			mathHelper.
				Received().
				CalculateSquareValue(0);

			mathHelper.
				Received().
				CalculateRootOfValue(0);
		}
	}
}