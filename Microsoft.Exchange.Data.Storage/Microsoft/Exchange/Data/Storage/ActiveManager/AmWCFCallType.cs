using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000313 RID: 787
	internal enum AmWCFCallType
	{
		// Token: 0x040014E1 RID: 5345
		LocalServer,
		// Token: 0x040014E2 RID: 5346
		RemoteServerSameDomainSameSite,
		// Token: 0x040014E3 RID: 5347
		RemoteServerSameDomainDifferentSite,
		// Token: 0x040014E4 RID: 5348
		RemoteServerDifferentDomain,
		// Token: 0x040014E5 RID: 5349
		Unknown
	}
}
