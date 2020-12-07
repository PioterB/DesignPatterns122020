using System;

namespace PbLab.DesignPatterns.Model
{
	public class MassValue	
	{
		public MassValue()
		{

		}

		public MassValue(decimal value, MassUnit unit)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("mass must be positive");
			}

			Value = value;
			Unit = unit;
		}

		public decimal Value { get; set; }
		public MassUnit Unit { get; set; }

		public override string ToString()
		{
			return Value.ToString() + " " + Unit;
		}

        public static MassValue Parse(string mass)
        {
            var massParts = mass.Split((' '));
            var value = decimal.Parse(massParts[0]);
            var unit = (MassUnit)Enum.Parse(typeof(MassUnit), massParts[1]);

			return new MassValue(value, unit);
		}
	}
}