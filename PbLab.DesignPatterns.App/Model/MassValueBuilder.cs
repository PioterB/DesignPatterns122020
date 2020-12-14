using System;
using PbLab.DesignPatterns.Tools;
using PbLab.DesignPatterns.Validation;

namespace PbLab.DesignPatterns.Model
{
    internal class MassValueBuilder : IMassValueBuilder
    {
        private readonly ChainLink<string> _valueValidation =
            new ChainLink<string>(
                new EmptyString(),
                new ChainLink<string>(
                    new IsNegative(),
                    new ChainLink<string>(
                        new NotNumber())));


        private decimal _value;
        private MassUnit _unit;

        public void AddValue(string value)
        {
            if (_valueValidation.Handle(value))
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