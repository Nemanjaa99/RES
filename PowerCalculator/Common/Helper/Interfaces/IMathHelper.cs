namespace Common.Helper.Interfaces
{
	public interface IMathHelper
	{
		decimal CalculateAbsoluteValuePerHour(decimal value);

		decimal CalculateDivisionValue(decimal a, decimal b);

		double CalculateRootOfValue(decimal value);

		decimal CalculateSquareValue(decimal value);

		decimal CalculateValuePerHour(int expeted, int actual);
	}
}