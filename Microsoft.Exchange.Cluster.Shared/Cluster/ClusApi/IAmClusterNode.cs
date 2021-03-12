using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000027 RID: 39
	internal interface IAmClusterNode : IDisposable
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000179 RID: 377
		AmClusterNodeHandle Handle { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600017A RID: 378
		AmServerName Name { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600017B RID: 379
		AmNodeState State { get; }

		// Token: 0x0600017C RID: 380
		IEnumerable<AmClusterNetInterface> EnumerateNetInterfaces();

		// Token: 0x0600017D RID: 381
		long GetHungNodesMask(out int currentGumId);

		// Token: 0x0600017E RID: 382
		AmNodeState GetState(bool isThrowIfUnknown);

		// Token: 0x0600017F RID: 383
		bool IsNetworkVisible(string networkName);
	}
}
