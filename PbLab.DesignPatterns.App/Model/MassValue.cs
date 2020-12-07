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

			var builder = new MassValueBuilder();
			builder.AddValue(massParts[0]);
			builder.AddUnit(massParts[1]);

            return builder.Build();
		}
	}
}