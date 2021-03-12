using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000328 RID: 808
	[ComVisible(true)]
	public interface IIdentityPermissionFactory
	{
		// Token: 0x0600294D RID: 10573
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
