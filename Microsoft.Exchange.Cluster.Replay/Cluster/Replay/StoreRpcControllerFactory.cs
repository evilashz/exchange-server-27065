using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000320 RID: 800
	internal class StoreRpcControllerFactory : IStoreRpcFactory
	{
		// Token: 0x060020D6 RID: 8406 RVA: 0x000972D5 File Offset: 0x000954D5
		public IStoreRpc Construct(string serverNameOrFqdn, string clientTypeId)
		{
			return new StoreRpcController(serverNameOrFqdn, clientTypeId);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000972DE File Offset: 0x000954DE
		public IStoreRpc ConstructWithNoTimeout(string serverNameOrFqdn)
		{
			return new StoreRpcControllerNoTimeout(serverNameOrFqdn);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000972E6 File Offset: 0x000954E6
		public IListMDBStatus ConstructListMDBStatus(string serverNameOrFqdn, string clientTypeId)
		{
			return new StoreRpcController(serverNameOrFqdn, clientTypeId);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000972EF File Offset: 0x000954EF
		public IStoreMountDismount ConstructMountDismount(string serverNameOrFqdn)
		{
			return new StoreRpcController(serverNameOrFqdn, null);
		}
	}
}
