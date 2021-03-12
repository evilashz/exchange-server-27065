using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000C2 RID: 194
	internal sealed class ThreadSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x00014D13 File Offset: 0x00012F13
		public ThreadSafeHandle() : this(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00014D21 File Offset: 0x00012F21
		public ThreadSafeHandle(IntPtr handle) : this(handle, true)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00014D2B File Offset: 0x00012F2B
		public ThreadSafeHandle(IntPtr handle, bool ownHandle) : base(ownHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00014D3B File Offset: 0x00012F3B
		protected override bool ReleaseHandle()
		{
			return DiagnosticsNativeMethods.CloseHandle(this.handle);
		}
	}
}
