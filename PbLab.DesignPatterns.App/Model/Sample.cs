using System;
using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.Model
{
	public class Sample
	{
		public Sample()
        {
            Id = IdGenerator.Instance.Next();
        }

		public Sample(DateTimeOffset timeStamp, MassValue mass) : this()
		{
			TimeStamp = timeStamp;
			Mass = mass;
		}

        public int Id { get; }

		public DateTimeOffset TimeStamp { get; set; }

		public MassValue Mass { get; set; }


        public SampleSnapshot Snahpshot()
        {
			return new SampleSnapshot();
        }

        public void Restore(SampleSnapshot snapshot)
        {

        }
	}

    public class SampleSnapshot
    {
        public DateTime TimeStamp { get; }

        public DateTime OryginalyCreated { get; }
    }
}