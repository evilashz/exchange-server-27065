using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000DB RID: 219
	internal sealed class WerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00019AD4 File Offset: 0x00017CD4
		public WerSafeHandle() : this(IntPtr.Zero)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00019AE1 File Offset: 0x00017CE1
		public WerSafeHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00019AF1 File Offset: 0x00017CF1
		protected override bool ReleaseHandle()
		{
			DiagnosticsNativeMethods.WerReportCloseHandle(this.handle);
			return true;
		}
	}
}
