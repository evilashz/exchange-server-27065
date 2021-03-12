using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000184 RID: 388
	internal class SafeDsRolePrimaryDomainInfoLevelHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x0004F97D File Offset: 0x0004DB7D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.DsRoleFreeMemory(this.handle);
			return true;
		}
	}
}
