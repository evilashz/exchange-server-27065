using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000180 RID: 384
	internal class SafeDnsHostNameHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x0004F923 File Offset: 0x0004DB23
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.NetApiBufferFree(this.handle);
			return true;
		}
	}
}
