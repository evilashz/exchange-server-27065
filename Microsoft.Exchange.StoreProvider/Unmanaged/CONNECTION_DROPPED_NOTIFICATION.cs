using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A8 RID: 680
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct CONNECTION_DROPPED_NOTIFICATION
	{
		// Token: 0x04001178 RID: 4472
		internal IntPtr lpszServerDN;

		// Token: 0x04001179 RID: 4473
		internal IntPtr lpszUserDN;

		// Token: 0x0400117A RID: 4474
		internal int dwTickDeath;
	}
}
