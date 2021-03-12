using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C81 RID: 3201
	internal sealed class SafeContextHandle : SafeSspiHandle
	{
		// Token: 0x060046CF RID: 18127 RVA: 0x000BE4D3 File Offset: 0x000BC6D3
		protected override bool ReleaseHandle()
		{
			return SspiNativeMethods.DeleteSecurityContext(ref this.SspiHandle) == SecurityStatus.OK;
		}
	}
}
