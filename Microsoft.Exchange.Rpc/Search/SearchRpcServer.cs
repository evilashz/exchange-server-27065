using System;

namespace Microsoft.Exchange.Rpc.Search
{
	// Token: 0x020003EC RID: 1004
	internal abstract class SearchRpcServer : RpcServerBase
	{
		// Token: 0x06001132 RID: 4402
		public abstract void RecordDocumentProcessing(Guid mdbGuid, Guid flowInstance, Guid correlationId, long docId);

		// Token: 0x06001133 RID: 4403
		public abstract void RecordDocumentFailure(Guid mdbGuid, Guid correlationId, long docId, string errorMessage);

		// Token: 0x06001134 RID: 4404
		public abstract void UpdateIndexSystems();

		// Token: 0x06001135 RID: 4405
		public abstract void ResumeIndexing(Guid databaseGuid);

		// Token: 0x06001136 RID: 4406
		public abstract void RebuildIndexSystem(Guid databaseGuid);

		// Token: 0x06001137 RID: 4407 RVA: 0x00056654 File Offset: 0x00055A54
		public SearchRpcServer()
		{
		}

		// Token: 0x04001011 RID: 4113
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISearchServiceRpc_v4_0_s_ifspec;
	}
}
