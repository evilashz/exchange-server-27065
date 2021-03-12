using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
	// Token: 0x0200063F RID: 1599
	[ComVisible(false)]
	public static class InternalApplicationIdentityHelper
	{
		// Token: 0x06004DF5 RID: 19957 RVA: 0x001178D0 File Offset: 0x00115AD0
		[SecurityCritical]
		public static object GetInternalAppId(ApplicationIdentity id)
		{
			return id.Identity;
		}
	}
}
