using Common.Helper.Interfaces;
using System;

namespace Common.Helper
{
	public class MathHelper : IMathHelper
	{
		
		public decimal CalculateValuePerHour(int expeted, int actual)
		{
			return decimal.Divide(actual - expeted, actual) * 100;
		}

		
		public decimal CalculateAbsoluteValuePerHour(decimal value)
		{
			return Math.Abs(value);
		}

		
		public decimal CalculateSquareValue(decimal value)
		{
			return value * value;
		}

		public double CalculateRootOfValue(decimal value)
		{
			return Math.Sqrt(Convert.ToDouble(value));
		}

		
		public decimal CalculateDivisionValue(decimal a, decimal b)
		{
			return decimal.Divide(a, b);
		}
	}
}