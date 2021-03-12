using System;
using System.Net;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.MigrationService;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationNotificationRpcStub : IMigrationNotification
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000214A File Offset: 0x0000034A
		internal MigrationNotificationRpcStub(string mailboxServer) : this(mailboxServer, MigrationNotificationRpcStub.LocalSystemCredential)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002158 File Offset: 0x00000358
		internal MigrationNotificationRpcStub(string mailboxServer, NetworkCredential credential)
		{
			this.mailboxServer = mailboxServer;
			this.credential = credential;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021F0 File Offset: 0x000003F0
		public UpdateMigrationRequestResult UpdateMigrationRequest(UpdateMigrationRequestArgs args)
		{
			MigrationServiceRpcMethodCode methodCode = MigrationServiceRpcMethodCode.SubscriptionStatusChanged;
			MdbefPropertyCollection mdbefPropertyCollection = args.Marshal();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			byte[] inputArgBytes = mdbefPropertyCollection.GetBytes();
			UpdateMigrationRequestResult result = null;
			MigrationRpcHelper.InvokeRpcOperation(delegate
			{
				using (IMigrationNotificationRpc migrationNotificationClient = this.GetMigrationNotificationClient(this.mailboxServer, this.credential))
				{
					byte[] array = migrationNotificationClient.UpdateMigrationRequest(1, inputArgBytes);
					MdbefPropertyCollection args2 = MdbefPropertyCollection.Create(array, 0, array.Length);
					result = new UpdateMigrationRequestResult(args2, methodCode);
				}
			});
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CC File Offset: 0x000004CC
		public RegisterMigrationBatchResult RegisterMigrationBatch(RegisterMigrationBatchArgs args)
		{
			MigrationServiceRpcMethodCode methodCode = MigrationServiceRpcMethodCode.RegisterMigrationBatch;
			MdbefPropertyCollection mdbefPropertyCollection = args.Marshal();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			byte[] inputArgBytes = mdbefPropertyCollection.GetBytes();
			RegisterMigrationBatchResult result = null;
			MigrationRpcHelper.InvokeRpcOperation(delegate
			{
				using (IMigrationNotificationRpc migrationNotificationClient = this.GetMigrationNotificationClient(this.mailboxServer, null))
				{
					byte[] array = migrationNotificationClient.UpdateMigrationRequest(1, inputArgBytes);
					MdbefPropertyCollection args2 = MdbefPropertyCollection.Create(array, 0, array.Length);
					result = new RegisterMigrationBatchResult(args2, methodCode);
				}
			});
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002334 File Offset: 0x00000534
		private IMigrationNotificationRpc GetMigrationNotificationClient(string mailboxServer, NetworkCredential credential)
		{
			return new MigrationNotificationRpcClient(this.mailboxServer, credential);
		}

		// Token: 0x04000007 RID: 7
		private const int CurrentVersion = 1;

		// Token: 0x04000008 RID: 8
		private static readonly NetworkCredential LocalSystemCredential = new NetworkCredential(Environment.MachineName + "$", string.Empty, string.Empty);

		// Token: 0x04000009 RID: 9
		private string mailboxServer;

		// Token: 0x0400000A RID: 10
		private NetworkCredential credential;
	}
}
