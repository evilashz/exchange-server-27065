using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200036C RID: 876
	internal interface IPrimaryServerInfoMap
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06002616 RID: 9750
		// (remove) Token: 0x06002617 RID: 9751
		event Action<PrimaryServerInfo> NotifyPrimaryServerStateChanged;

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002618 RID: 9752
		int Count { get; }

		// Token: 0x06002619 RID: 9753
		void Add(PrimaryServerInfo primaryServerInfo);

		// Token: 0x0600261A RID: 9754
		IEnumerable<PrimaryServerInfo> GetAll();

		// Token: 0x0600261B RID: 9755
		PrimaryServerInfo GetActive(string serverFqdn);

		// Token: 0x0600261C RID: 9756
		PrimaryServerInfo UpdateServerState(string serverFqdn, string state, ShadowRedundancyCompatibilityVersion version);

		// Token: 0x0600261D RID: 9757
		bool Remove(PrimaryServerInfo primaryServerInfo);

		// Token: 0x0600261E RID: 9758
		IEnumerable<PrimaryServerInfo> RemoveExpiredServers(DateTime now);
	}
}
