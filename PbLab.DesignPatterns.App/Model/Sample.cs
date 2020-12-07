using System;

namespace PbLab.DesignPatterns.Model
{
	public class Sample
	{
		public Sample()
		{

		}

		public Sample(DateTimeOffset timeStamp, MassValue mass)
		{
			TimeStamp = timeStamp;
			Mass = mass;
		}

		public DateTimeOffset TimeStamp { get; set; }

		public MassValue Mass { get; set; }
	}
}