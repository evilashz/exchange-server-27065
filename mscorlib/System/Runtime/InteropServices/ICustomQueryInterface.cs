using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093A RID: 2362
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public interface ICustomQueryInterface
	{
		// Token: 0x0600610B RID: 24843
		[SecurityCritical]
		CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv);
	}
}
