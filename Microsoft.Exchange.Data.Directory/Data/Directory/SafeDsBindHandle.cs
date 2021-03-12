using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000181 RID: 385
	internal sealed class SafeDsBindHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x0004F93A File Offset: 0x0004DB3A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.DsUnBind(ref this.handle);
			return true;
		}
	}
}
