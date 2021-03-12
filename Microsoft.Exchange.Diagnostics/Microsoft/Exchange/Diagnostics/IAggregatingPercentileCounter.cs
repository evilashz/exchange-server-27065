using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000DF RID: 223
	public interface IAggregatingPercentileCounter : IPercentileCounter
	{
		// Token: 0x06000659 RID: 1625
		void IncrementValue(ref long value, long increment);
	}
}
