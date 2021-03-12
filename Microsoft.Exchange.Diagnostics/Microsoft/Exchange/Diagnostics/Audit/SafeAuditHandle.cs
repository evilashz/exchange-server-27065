using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200018F RID: 399
	internal sealed class SafeAuditHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x000299A0 File Offset: 0x00027BA0
		private SafeAuditHandle() : base(true)
		{
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000299A9 File Offset: 0x00027BA9
		protected override bool ReleaseHandle()
		{
			return NativeMethods.AuthzUnregisterSecurityEventSource(0U, ref this.handle);
		}
	}
}
