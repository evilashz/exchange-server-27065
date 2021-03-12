using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200013D RID: 317
	public struct InternalCounterEntryMisaligned
	{
		// Token: 0x0400062D RID: 1581
		public int SpinLock;

		// Token: 0x0400062E RID: 1582
		public int CounterNameHashCode;

		// Token: 0x0400062F RID: 1583
		public int CounterNameOffset;

		// Token: 0x04000630 RID: 1584
		public int LifetimeOffset;

		// Token: 0x04000631 RID: 1585
		public int Value_lo;

		// Token: 0x04000632 RID: 1586
		public int Value_hi;

		// Token: 0x04000633 RID: 1587
		public int NextCounterOffset;

		// Token: 0x04000634 RID: 1588
		public int Padding2;
	}
}
