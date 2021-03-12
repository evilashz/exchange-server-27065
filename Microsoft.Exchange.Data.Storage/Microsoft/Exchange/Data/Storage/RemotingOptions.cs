using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200026D RID: 621
	[Flags]
	internal enum RemotingOptions
	{
		// Token: 0x04001265 RID: 4709
		LocalConnectionsOnly = 0,
		// Token: 0x04001266 RID: 4710
		AllowCrossSite = 1,
		// Token: 0x04001267 RID: 4711
		AllowCrossPremise = 2,
		// Token: 0x04001268 RID: 4712
		AllowHybridAccess = 4
	}
}
