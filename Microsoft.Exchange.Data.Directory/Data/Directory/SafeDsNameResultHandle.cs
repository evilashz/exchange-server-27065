using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000183 RID: 387
	internal class SafeDsNameResultHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x0600107B RID: 4219 RVA: 0x0004F967 File Offset: 0x0004DB67
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.DsFreeNameResult(this.handle);
			return true;
		}
	}
}
