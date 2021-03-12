using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004FF RID: 1279
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class Timeout
	{
		// Token: 0x04001973 RID: 6515
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x04001974 RID: 6516
		[__DynamicallyInvokable]
		public const int Infinite = -1;

		// Token: 0x04001975 RID: 6517
		internal const uint UnsignedInfinite = 4294967295U;
	}
}
