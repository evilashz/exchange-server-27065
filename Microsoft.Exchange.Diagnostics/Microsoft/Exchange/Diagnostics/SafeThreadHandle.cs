using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000153 RID: 339
	internal sealed class SafeThreadHandle : DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x000241A9 File Offset: 0x000223A9
		internal SafeThreadHandle()
		{
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000241B1 File Offset: 0x000223B1
		internal SafeThreadHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000241BA File Offset: 0x000223BA
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeThreadHandle>(this);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000241C2 File Offset: 0x000223C2
		protected override bool ReleaseHandle()
		{
			return DiagnosticsNativeMethods.CloseHandle(this.handle);
		}
	}
}
