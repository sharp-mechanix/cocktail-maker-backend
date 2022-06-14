using System;
using System.Linq;

namespace CocktailMaker.Common.Math
{
	/// <summary>
    ///		Static class to handle fractions
    /// </summary>
	public static class Fraction
	{
		/// <summary>
        ///     Converts fraction with unit string to <see cref="UnitValuePair" />
        /// </summary>
		public static UnitValuePair ConvertFractionToUnitValuePair(string fractionWithUnit)
        {
			var trimmed = fractionWithUnit.Trim();
			var parts = trimmed.Split(new char[] { ' ', '/' });

			if (parts.Length == 2 && parts[0].Contains('-'))
            {
				var dashed = parts[0].Split('-');

				return new UnitValuePair
				{
					Unit = parts[1],
				    Value = Convert.ToDouble(dashed[0])
				};
            }

			if (parts.Length == 2 && !parts[0].Contains('-'))
            {
				try
				{
					return new UnitValuePair
					{
						Unit = parts[1],
						Value = Convert.ToDouble(parts[0])
					};
				}
				catch (Exception)
				{
					return new UnitValuePair
					{
						Unit = "nothing",
						Value = 0
					};
				}
			}

			var result = new UnitValuePair
			{
				Unit = parts[parts.Length - 1],
			};

			parts = parts.SkipLast(1).ToArray();

			if (parts.Length == 1)
            {
				result.Value = Convert.ToDouble(parts[0]);

				return result;
            }

			var fractionPart = parts.TakeLast(2).ToArray();
			parts = parts.SkipLast(2).ToArray();

			result.Value = Convert.ToInt32(fractionPart[0]) / Convert.ToDouble(fractionPart[1]);

			if (parts.Length == 1)
            {
				result.Value += Convert.ToInt32(parts[0]);
            }

			return result;
        }
	}
}

