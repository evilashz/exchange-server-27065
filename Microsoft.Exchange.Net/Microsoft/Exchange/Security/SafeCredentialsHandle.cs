using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C80 RID: 3200
	internal class SafeCredentialsHandle : SafeSspiHandle
	{
		// Token: 0x060046CD RID: 18125 RVA: 0x000BE4BB File Offset: 0x000BC6BB
		protected override bool ReleaseHandle()
		{
			return SspiNativeMethods.FreeCredentialsHandle(ref this.SspiHandle) == SecurityStatus.OK;
		}
	}
}
