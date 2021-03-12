using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000DE RID: 222
	public interface IPercentileCounter
	{
		// Token: 0x06000656 RID: 1622
		void AddValue(long value);

		// Token: 0x06000657 RID: 1623
		long PercentileQuery(double percentage);

		// Token: 0x06000658 RID: 1624
		long PercentileQuery(double percentage, out long samples);
	}
}
