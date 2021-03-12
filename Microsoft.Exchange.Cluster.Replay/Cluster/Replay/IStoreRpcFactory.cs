using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200031F RID: 799
	internal interface IStoreRpcFactory
	{
		// Token: 0x060020D2 RID: 8402
		IStoreRpc Construct(string serverNameOrFqdn, string clientTypeId);

		// Token: 0x060020D3 RID: 8403
		IStoreRpc ConstructWithNoTimeout(string serverNameOrFqdn);

		// Token: 0x060020D4 RID: 8404
		IListMDBStatus ConstructListMDBStatus(string serverNameOrFqdn, string clientTypeId);

		// Token: 0x060020D5 RID: 8405
		IStoreMountDismount ConstructMountDismount(string serverNameOrFqdn);
	}
}
