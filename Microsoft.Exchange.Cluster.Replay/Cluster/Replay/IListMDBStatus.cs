using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200031C RID: 796
	internal interface IListMDBStatus : IDisposable
	{
		// Token: 0x060020BF RID: 8383
		MdbStatus[] ListMdbStatus(Guid[] dbGuids);

		// Token: 0x060020C0 RID: 8384
		MdbStatus[] ListMdbStatus(Guid[] dbGuids, TimeSpan? timeout);

		// Token: 0x060020C1 RID: 8385
		MdbStatus[] ListMdbStatus(bool isBasicInformation);

		// Token: 0x060020C2 RID: 8386
		MdbStatus[] ListMdbStatus(bool isBasicInformation, TimeSpan? timeout);
	}
}
