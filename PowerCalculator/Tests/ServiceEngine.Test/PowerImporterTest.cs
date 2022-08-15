using Common.FileUpload.Interfaces;
using Common.Model;
using DatabaseAccess.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ServiceEngine.Test
{
	public class PowerImporterTest
	{
		private IFileDialog fileDialog;
		private ISqliteDataAccess sqliteDataAccess;

		private PowerImporter powerImporter;

		[SetUp]
		public void SetUp()
		{
			sqliteDataAccess = Substitute.For<ISqliteDataAccess>();
			fileDialog = Substitute.For<IFileDialog>();

			powerImporter = new PowerImporter(fileDialog, sqliteDataAccess);
		}

		[Test]
		public void CollectData_CallsTheMethod_ReturnsVoid()
		{
			string expetedPath = @"D:\folder";
			string actualPath = @"D:\folder2";

			powerImporter.CollectData(expetedPath, actualPath);

			fileDialog.
				Received().
				Load(expetedPath);

			fileDialog.
				Received().
				Load(actualPath);
		}

		[Test]
		public void ValidatePowerConsumtionTimePeriods_CallsTheMethod_ReturnsVoid()
		{
			powerImporter.ValidatePowerConsumtionTimePeriods(new List<PowerRecord>
			{
				new PowerRecord(1, "6-5-2021", 4500, "BG", "22-8-2021"),
				new PowerRecord(2, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(4, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "BG", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "CRG", "22-8-2021")
			});
		}

		[Test]
		[TestCase(1)]
		[TestCase(0)]
		[TestCase(-10)]
		[TestCase(20)]
		[TestCase(30)]
		[TestCase(null)]
		public void CheckTimePeriodsNumber_ValidateInputParameters_ReturnsException(int numberOfHoursByRegion)
		{
			Exception ex = Assert.Throws<Exception>(() => powerImporter.CheckTimePeriodsNumber(numberOfHoursByRegion));

			Assert.That(ex.Message == "Import files must have 23, 24 or 25 hours per region!");
		}

		[Test]
		[TestCase(23)]
		[TestCase(24)]
		[TestCase(25)]
		public void CheckTimePeriodsNumber_ValidateInputParameters_ReturnsVoid(int numberOfHoursByRegion)
		{
			powerImporter.CheckTimePeriodsNumber(numberOfHoursByRegion);
		}

		[Test]
		public void ValidateFilesCount_ValidateInputParameters_ReturnsException()
		{
			List<PowerRecord> expeted = new List<PowerRecord>()
			{
				new PowerRecord(),
				new PowerRecord()
			};

			List<PowerRecord> actual = new List<PowerRecord>()
			{
				new PowerRecord(),
				new PowerRecord(),
				new PowerRecord()
			};

			Exception ex = Assert.Throws<Exception>(() => powerImporter.ValidateFilesCount(expeted, actual));

			Assert.That(ex.Message == "Import files don't have equal data rows, please import valid files!");
		}

		[Test]
		public void ValidateFilesCount_ValidateInputParameters_ReturnsVoid()
		{
			List<PowerRecord> expeted = new List<PowerRecord>()
			{
				new PowerRecord(),
				new PowerRecord(),
				new PowerRecord()
			};

			List<PowerRecord> actual = new List<PowerRecord>()
			{
				new PowerRecord(),
				new PowerRecord(),
				new PowerRecord()
			};

			powerImporter.ValidateFilesCount(expeted, actual);
		}

		[Test]
		public void ValidateDataFromFiles_ValidateInputParameters_ReturnsException()
		{
			List<PowerRecord> expeted = new List<PowerRecord>()
			{
				new PowerRecord(1, "6-5-2021", 4500, "BG", "22-8-2021"),
				new PowerRecord(2, "6-5-2021", 4000, "VOJ", "22-8-2021")
			};

			List<PowerRecord> actual = new List<PowerRecord>()
			{
				new PowerRecord(2, "6-5-2021", 4500, "VOJ", "22-8-2021"),
				new PowerRecord(3, "6-5-2021", 4000, "VOJ", "22-8-2021")
			};

			Exception ex = Assert.Throws<Exception>(() => powerImporter.ValidateDataFromFiles(expeted, actual));

			Assert.That(ex.Message == "Files are inconsistent, please import valid files!");
		}

		[Test]
		public void ValidateDataFromFiles_ValidateInputParameters_ReturnsVoid()
		{
			List<PowerRecord> expeted = new List<PowerRecord>()
			{
				new PowerRecord(1, "6-5-2021", 4500, "VOJ", "22-8-2021"),
				new PowerRecord(2, "6-5-2021", 4000, "VOJ", "22-8-2021")
			};

			List<PowerRecord> actual = new List<PowerRecord>()
			{
				new PowerRecord(1, "6-5-2021", 4500, "VOJ", "22-8-2021"),
				new PowerRecord(2, "6-5-2021", 4000, "VOJ", "22-8-2021")
			};

			powerImporter.ValidateDataFromFiles(expeted, actual);
		}
	}
}