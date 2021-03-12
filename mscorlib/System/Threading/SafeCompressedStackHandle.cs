using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004C3 RID: 1219
	[SecurityCritical]
	internal class SafeCompressedStackHandle : SafeHandle
	{
		// Token: 0x06003A8B RID: 14987 RVA: 0x000DDECD File Offset: 0x000DC0CD
		public SafeCompressedStackHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000DDEDB File Offset: 0x000DC0DB
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000DDEED File Offset: 0x000DC0ED
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			CompressedStack.DestroyDelayedCompressedStack(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
