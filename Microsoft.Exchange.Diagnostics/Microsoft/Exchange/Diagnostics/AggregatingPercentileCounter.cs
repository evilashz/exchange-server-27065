using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public sealed class AggregatingPercentileCounter : PercentileCounter, IAggregatingPercentileCounter, IPercentileCounter
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0001A45F File Offset: 0x0001865F
		public AggregatingPercentileCounter(long valueGranularity, long valueMaximum) : base(TimeSpan.MaxValue, TimeSpan.MaxValue, valueGranularity, valueMaximum, null)
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001A474 File Offset: 0x00018674
		public void IncrementValue(ref long value, long increment)
		{
			lock (this.syncObject)
			{
				if (value + increment < 0L)
				{
					throw new ArgumentOutOfRangeException("increment", "Increment would make value negative");
				}
				if (value > 0L)
				{
					base.RemoveValue(value);
				}
				value += increment;
				base.AddValue(value);
			}
		}
	}
}
