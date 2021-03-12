using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F2 RID: 2034
	[ComVisible(true)]
	public interface ISponsor
	{
		// Token: 0x0600580A RID: 22538
		[SecurityCritical]
		TimeSpan Renewal(ILease lease);
	}
}
