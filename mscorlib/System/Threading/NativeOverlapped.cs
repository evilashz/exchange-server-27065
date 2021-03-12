using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004D6 RID: 1238
	[ComVisible(true)]
	public struct NativeOverlapped
	{
		// Token: 0x040018E9 RID: 6377
		public IntPtr InternalLow;

		// Token: 0x040018EA RID: 6378
		public IntPtr InternalHigh;

		// Token: 0x040018EB RID: 6379
		public int OffsetLow;

		// Token: 0x040018EC RID: 6380
		public int OffsetHigh;

		// Token: 0x040018ED RID: 6381
		public IntPtr EventHandle;
	}
}
