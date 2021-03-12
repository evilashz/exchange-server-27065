using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000182 RID: 386
	internal sealed class SafeDsGetDcContextHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x0004F951 File Offset: 0x0004DB51
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.DsGetDcClose(this.handle);
			return true;
		}
	}
}
