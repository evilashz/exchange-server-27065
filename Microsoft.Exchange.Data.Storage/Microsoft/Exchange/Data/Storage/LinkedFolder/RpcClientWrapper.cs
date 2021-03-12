using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.JobQueue;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000997 RID: 2455
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcClientWrapper
	{
		// Token: 0x06005A8B RID: 23179 RVA: 0x00178D47 File Offset: 0x00176F47
		private RpcClientWrapper()
		{
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x00178D7C File Offset: 0x00176F7C
		private static void InitializeIfNeeded()
		{
			if (!RpcClientWrapper.instance.initialized)
			{
				lock (RpcClientWrapper.instance.initializeLockObject)
				{
					if (!RpcClientWrapper.instance.initialized)
					{
						RpcClientWrapper.instance.Initialize();
					}
				}
			}
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x00178DDC File Offset: 0x00176FDC
		public static EnqueueResult EnqueueTeamMailboxSyncRequest(string targetServer, Guid mailboxGuid, QueueType queueType, OrganizationId orgId, string clientString, string domainController, SyncOption syncOption = SyncOption.Default)
		{
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentNullException("mailboxGuid");
			}
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			if (queueType != QueueType.TeamMailboxDocumentSync && queueType != QueueType.TeamMailboxMembershipSync && queueType != QueueType.TeamMailboxMaintenanceSync)
			{
				throw new ArgumentException("queueType");
			}
			RpcClientWrapper.InitializeIfNeeded();
			if (!RpcClientWrapper.instance.initialized)
			{
				return new EnqueueResult(EnqueueResultType.ClientInitError, ClientStrings.RpcClientInitError);
			}
			EnqueueResult result;
			try
			{
				using (JobQueueRpcClient jobQueueRpcClient = new JobQueueRpcClient(targetServer ?? RpcClientWrapper.instance.localServer.Fqdn))
				{
					TeamMailboxSyncRpcInParameters teamMailboxSyncRpcInParameters = new TeamMailboxSyncRpcInParameters(mailboxGuid, orgId, clientString, syncOption, domainController);
					byte[] data = jobQueueRpcClient.EnqueueRequest(1, (int)queueType, teamMailboxSyncRpcInParameters.Serialize());
					EnqueueRequestRpcOutParameters enqueueRequestRpcOutParameters = new EnqueueRequestRpcOutParameters(data);
					result = enqueueRequestRpcOutParameters.Result;
				}
			}
			catch (RpcException ex)
			{
				result = new EnqueueResult(EnqueueResultType.RpcError, ClientStrings.RpcClientRequestError(ex.Message));
			}
			return result;
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x00178EF0 File Offset: 0x001770F0
		private void Initialize()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				this.localServer = this.rootOrgConfigSession.FindLocalServer();
			}, 3);
			if (adoperationResult.Succeeded)
			{
				this.initialized = true;
			}
		}

		// Token: 0x040031FA RID: 12794
		private const int CurrentAPIVersion = 1;

		// Token: 0x040031FB RID: 12795
		private static readonly RpcClientWrapper instance = new RpcClientWrapper();

		// Token: 0x040031FC RID: 12796
		private readonly object initializeLockObject = new object();

		// Token: 0x040031FD RID: 12797
		private readonly ITopologyConfigurationSession rootOrgConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 41, "rootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\LinkedFolder\\RpcClientWrapper.cs");

		// Token: 0x040031FE RID: 12798
		private bool initialized;

		// Token: 0x040031FF RID: 12799
		private Server localServer;
	}
}
