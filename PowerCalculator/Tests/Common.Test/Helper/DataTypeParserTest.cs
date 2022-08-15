using Common.Helper;
using NUnit.Framework;
using System;

namespace Common.Test.Helper
{
	public class DataTypeParserTest
	{
		private DataTypeParser dataTypeParser;

		[SetUp]
		public void SetUp()
		{
			dataTypeParser = new DataTypeParser();
		}

		[Test]
		public void ConvertIntFromString_CheckMethodCalculations_ReturnsRightNumber()
		{
			int expeted = 18;

			int actual = dataTypeParser.ConvertIntFromString("18");

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void ConvertIntFromString_CheckMethodCalculations_ReturnsException()
		{
			Exception ex = Assert.Throws<Exception>(() => dataTypeParser.ConvertIntFromString("18al32../"));

			Assert.That(ex.Message == "Failed to convert int type from string type");
		}

		[Test]
		public void ConvertDateTimeFromString_CheckMethodCalculations_ReturnsRightDateTime()
		{
			DateTime expeted = new DateTime(2021, 8, 21);

			DateTime actual = dataTypeParser.ConvertDateTimeFromString("21-Aug-2021");

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void ConvertDateTimeFromString_CheckMethodCalculations_ReturnsException()
		{
			Exception ex = Assert.Throws<Exception>(() => dataTypeParser.ConvertDateTimeFromString("21-s\\Aug-2021."));

			Assert.That(ex.Message == "Failed to convert date time type from string type");
		}

		[Test]
		public void GetDateFromFileName_CheckMethodCalculations_ReturnsRightDateTime()
		{
			string expeted = new DateTime(2020, 5, 7).ToString();

			string actual = dataTypeParser.GetDateFromFileName(@"D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv");

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void GetDateFromFileName_CheckMethodCalculations_ReturnsWrongDateTime()
		{
			string expeted = new DateTime(2021, 10, 7).ToString();

			string actual = dataTypeParser.GetDateFromFileName(@"D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv");

			Assert.AreNotEqual(expeted, actual);
		}

		[Test]
		public void GetFileNameFromPath_CheckMethodCalculations_ReturnsRightFileName()
		{
			string expeted = "ostv_2020_05_07";

			string actual = dataTypeParser.GetFileNameFromPath(@"D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv");

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void GetFileNameFromPath_CheckMethodCalculations_ReturnsWrongFileName()
		{
			string expeted = "real_2021_05_07";

			string actual = dataTypeParser.GetFileNameFromPath(@"D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv");

			Assert.AreNotEqual(expeted, actual);
		}
	}
}