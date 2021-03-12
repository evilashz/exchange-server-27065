using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000467 RID: 1127
	internal enum UserContextCreationError
	{
		// Token: 0x040015FF RID: 5631
		None,
		// Token: 0x04001600 RID: 5632
		UnableToResolveLogonIdentity,
		// Token: 0x04001601 RID: 5633
		UnableToAcquireOwaRWLock
	}
}
