using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000E RID: 14
	internal interface ITrendline
	{
		// Token: 0x06000042 RID: 66
		bool WasAbove(long high);

		// Token: 0x06000043 RID: 67
		bool WasBelow(long low);

		// Token: 0x06000044 RID: 68
		bool HasCrossedBelowAfterLastCrossingAbove(long high, long low);

		// Token: 0x06000045 RID: 69
		bool HasCrossedAboveAfterLastCrossingBelow(long low, long high);

		// Token: 0x06000046 RID: 70
		bool StillAboveLowAfterCrossingHigh(long high, long low);

		// Token: 0x06000047 RID: 71
		bool StillBelowHighAfterCrossingLow(long low, long high);

		// Token: 0x06000048 RID: 72
		long GetMax();

		// Token: 0x06000049 RID: 73
		long GetMin();

		// Token: 0x0600004A RID: 74
		long GetAverage();
	}
}
