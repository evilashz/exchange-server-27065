using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000185 RID: 389
	internal class SafeDsSiteNameHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x0004F993 File Offset: 0x0004DB93
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.NetApiBufferFree(this.handle);
			return true;
		}
	}
}
