using System;

namespace PbLab.DesignPatterns.Model
{
    internal class MassValueBuilder : IMassValueBuilder
    {
        private decimal _value;
        private MassUnit _unit;

        public void AddValue(string value)
        {
            if (value.Trim().StartsWith("-"))
            {
                throw new ArgumentOutOfRangeException("mass must be positive");
            }

            _value = decimal.Parse(value);
        }

        public void AddUnit(string unit)
        {
            _unit = (MassUnit)Enum.Parse(typeof(MassUnit), unit);
        }

        public MassValue Build()
        {
            return new MassValue(_value, _unit);
        }
    }
}