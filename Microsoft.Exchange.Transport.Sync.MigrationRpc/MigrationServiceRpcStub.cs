using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.MigrationService;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationServiceRpcStub : IMigrationService
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000264D File Offset: 0x0000084D
		internal MigrationServiceRpcStub(string mailboxServer)
		{
			this.mailboxServer = mailboxServer;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026D4 File Offset: 0x000008D4
		public CreateSyncSubscriptionResult CreateSyncSubscription(AbstractCreateSyncSubscriptionArgs args)
		{
			MigrationServiceRpcMethodCode methodCode = MigrationServiceRpcMethodCode.CreateSyncSubscription;
			MdbefPropertyCollection mdbefPropertyCollection = args.Marshal();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			byte[] inputArgsBytes = mdbefPropertyCollection.GetBytes();
			CreateSyncSubscriptionResult result = null;
			MigrationRpcHelper.InvokeRpcOperation(delegate
			{
				using (IMigrationServiceRpc migrationServiceClient = this.GetMigrationServiceClient(this.mailboxServer))
				{
					int version = 2;
					byte[] array = migrationServiceClient.InvokeMigrationServiceEndPoint(version, inputArgsBytes);
					MdbefPropertyCollection args2 = MdbefPropertyCollection.Create(array, 0, array.Length);
					result = new CreateSyncSubscriptionResult(args2, methodCode);
				}
			});
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027B0 File Offset: 0x000009B0
		public UpdateSyncSubscriptionResult UpdateSyncSubscription(UpdateSyncSubscriptionArgs args)
		{
			MigrationServiceRpcMethodCode methodCode = MigrationServiceRpcMethodCode.UpdateSyncSubscription;
			MdbefPropertyCollection mdbefPropertyCollection = args.Marshal();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			byte[] inputArgsBytes = mdbefPropertyCollection.GetBytes();
			UpdateSyncSubscriptionResult result = null;
			MigrationRpcHelper.InvokeRpcOperation(delegate
			{
				using (IMigrationServiceRpc migrationServiceClient = this.GetMigrationServiceClient(this.mailboxServer))
				{
					byte[] array = migrationServiceClient.InvokeMigrationServiceEndPoint(1, inputArgsBytes);
					MdbefPropertyCollection args2 = MdbefPropertyCollection.Create(array, 0, array.Length);
					result = new UpdateSyncSubscriptionResult(args2, methodCode);
				}
			});
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000288C File Offset: 0x00000A8C
		public GetSyncSubscriptionStateResult GetSyncSubscriptionState(GetSyncSubscriptionStateArgs args)
		{
			MigrationServiceRpcMethodCode methodCode = MigrationServiceRpcMethodCode.GetSyncSubscriptionState;
			MdbefPropertyCollection mdbefPropertyCollection = args.Marshal();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			byte[] inputArgBytes = mdbefPropertyCollection.GetBytes();
			GetSyncSubscriptionStateResult result = null;
			MigrationRpcHelper.InvokeRpcOperation(delegate
			{
				using (IMigrationServiceRpc migrationServiceClient = this.GetMigrationServiceClient(this.mailboxServer))
				{
					byte[] array = migrationServiceClient.InvokeMigrationServiceEndPoint(1, inputArgBytes);
					MdbefPropertyCollection args2 = MdbefPropertyCollection.Create(array, 0, array.Length);
					result = new GetSyncSubscriptionStateResult(args2, methodCode);
				}
			});
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028F4 File Offset: 0x00000AF4
		private IMigrationServiceRpc GetMigrationServiceClient(string mailboxServer)
		{
			return new MigrationServiceRpcClient(this.mailboxServer);
		}

		// Token: 0x04000041 RID: 65
		internal const int CurrentVersion = 2;

		// Token: 0x04000042 RID: 66
		internal const int R4Version = 1;

		// Token: 0x04000043 RID: 67
		private string mailboxServer;
	}
}
