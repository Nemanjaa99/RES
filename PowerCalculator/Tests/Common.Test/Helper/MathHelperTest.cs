using Common.Helper;
using NUnit.Framework;

namespace Common.Test.Helper
{
	public class MathHelperTest
	{
		private MathHelper mathHelper;

		[SetUp]
		public void SetUp()
		{
			mathHelper = new MathHelper();
		}

		[Test]
		public void CalculateValuePerHour_CheckMethodCalculations_ReturnsRightNumber()
		{
			decimal expeted = -100;

			decimal actual = mathHelper.CalculateValuePerHour(10, 5);

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void CalculateValuePerHour_CheckMethodCalculations_ReturnsWrongNumber()
		{
			decimal expeted = -50;

			decimal actual = mathHelper.CalculateValuePerHour(10, 5);

			Assert.AreNotEqual(expeted, actual);
		}

		[Test]
		public void CalculateAbsoluteValuePerHour_CheckMethodCalculations_ReturnsRightNumber()
		{
			decimal expeted = 10;

			decimal actual = mathHelper.CalculateAbsoluteValuePerHour(-10);

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void CalculateAbsoluteValuePerHour_CheckMethodCalculations_ReturnsWrongNumber()
		{
			decimal expeted = -10;

			decimal actual = mathHelper.CalculateAbsoluteValuePerHour(10);

			Assert.AreNotEqual(expeted, actual);
		}

		[Test]
		public void CalculateSquareValue_CheckMethodCalculations_ReturnsRightNumber()
		{
			decimal expeted = 25;

			decimal actual = mathHelper.CalculateSquareValue(5);

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void CalculateSquareValue_CheckMethodCalculations_ReturnsWrongNumber()
		{
			decimal expeted = 10;

			decimal actual = mathHelper.CalculateSquareValue(5);

			Assert.AreNotEqual(expeted, actual);
		}

		[Test]
		public void CalculateRootOfValue_CheckMethodCalculations_ReturnsRightNumber()
		{
			double expeted = 4;

			double actual = mathHelper.CalculateRootOfValue(16);

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void CalculateRootOfValue_CheckMethodCalculations_ReturnsWrongNumber()
		{
			double expeted = 3;

			double actual = mathHelper.CalculateRootOfValue(16);

			Assert.AreNotEqual(expeted, actual);
		}

		[Test]
		public void CalculateDivisionValue_CheckMethodCalculations_ReturnsRightNumber()
		{
			decimal expeted = 4;

			decimal actual = mathHelper.CalculateDivisionValue(8, 2);

			Assert.AreEqual(expeted, actual);
		}

		[Test]
		public void CalculateDivisionValue_CheckMethodCalculations_ReturnsWrongNumber()
		{
			decimal expeted = 3;

			decimal actual = mathHelper.CalculateDivisionValue(8, 2);

			Assert.AreNotEqual(expeted, actual);
		}
	}
}