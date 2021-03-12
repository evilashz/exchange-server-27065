using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200013C RID: 316
	public struct InternalCounterEntryAligned
	{
		// Token: 0x04000626 RID: 1574
		public int SpinLock;

		// Token: 0x04000627 RID: 1575
		public int CounterNameHashCode;

		// Token: 0x04000628 RID: 1576
		public int CounterNameOffset;

		// Token: 0x04000629 RID: 1577
		public int LifetimeOffset;

		// Token: 0x0400062A RID: 1578
		public long Value;

		// Token: 0x0400062B RID: 1579
		public int NextCounterOffset;

		// Token: 0x0400062C RID: 1580
		public int Padding2;
	}
}
