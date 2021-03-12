using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UnifiedPolicyNotification;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E8E RID: 3726
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcClientWrapper
	{
		// Token: 0x060081AE RID: 33198 RVA: 0x00236FB4 File Offset: 0x002351B4
		private RpcClientWrapper()
		{
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x00236FEC File Offset: 0x002351EC
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

		// Token: 0x060081B0 RID: 33200 RVA: 0x0023704C File Offset: 0x0023524C
		public static SyncNotificationResult NotifySyncChanges(string identity, string targetServerFqdn, Guid tenantId, string syncSvcUrl, bool fullSync, bool syncNow, SyncChangeInfo[] syncChangeInfos)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentNullException("tenantId");
			}
			if (string.IsNullOrEmpty(syncSvcUrl))
			{
				throw new ArgumentNullException("syncSvcUrl");
			}
			RpcClientWrapper.InitializeIfNeeded();
			NotificationType type = NotificationType.Sync;
			if (!RpcClientWrapper.instance.initialized)
			{
				return new SyncNotificationResult(new SyncAgentTransientException("RPC client not initialized"));
			}
			SyncNotificationResult result;
			try
			{
				using (UnifiedPolicyNotificationRpcClient unifiedPolicyNotificationRpcClient = new UnifiedPolicyNotificationRpcClient(targetServerFqdn ?? RpcClientWrapper.instance.localServer.Fqdn))
				{
					SyncWorkItem syncWorkItem = new SyncWorkItem(identity, syncNow, new TenantContext(tenantId, null), syncChangeInfos, syncSvcUrl, fullSync, Workload.Exchange, false);
					byte[] data = unifiedPolicyNotificationRpcClient.Notify(1, (int)type, syncWorkItem.Serialize());
					NotificationRpcOutParameters notificationRpcOutParameters = new NotificationRpcOutParameters(data);
					result = notificationRpcOutParameters.Result;
				}
			}
			catch (RpcException ex)
			{
				result = new SyncNotificationResult(new SyncAgentTransientException(ex.Message, ex));
			}
			return result;
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x00237154 File Offset: 0x00235354
		public static SyncNotificationResult NotifyStatusChanges(string identity, string targetServerFqdn, Guid tenantId, string statusUpdateSvcUrl, bool syncNow, IList<UnifiedPolicyStatus> statusUpdates)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentNullException("tenantId");
			}
			if (string.IsNullOrEmpty(statusUpdateSvcUrl))
			{
				throw new ArgumentNullException("statusUpdateSvcUrl");
			}
			if (statusUpdates == null || statusUpdates.Count == 0)
			{
				throw new ArgumentNullException("statusUpdates");
			}
			RpcClientWrapper.InitializeIfNeeded();
			NotificationType type = NotificationType.ApplicationStatus;
			if (!RpcClientWrapper.instance.initialized)
			{
				return new SyncNotificationResult(new SyncAgentTransientException("RPC client not initialized"));
			}
			SyncNotificationResult result;
			try
			{
				using (UnifiedPolicyNotificationRpcClient unifiedPolicyNotificationRpcClient = new UnifiedPolicyNotificationRpcClient(targetServerFqdn ?? RpcClientWrapper.instance.localServer.Fqdn))
				{
					SyncStatusUpdateWorkitem syncStatusUpdateWorkitem = new SyncStatusUpdateWorkitem(identity, syncNow, new TenantContext(tenantId, null), statusUpdates, statusUpdateSvcUrl, 0);
					byte[] data = unifiedPolicyNotificationRpcClient.Notify(1, (int)type, syncStatusUpdateWorkitem.Serialize());
					NotificationRpcOutParameters notificationRpcOutParameters = new NotificationRpcOutParameters(data);
					result = notificationRpcOutParameters.Result;
				}
			}
			catch (RpcException ex)
			{
				result = new SyncNotificationResult(new SyncAgentTransientException(ex.Message, ex));
			}
			return result;
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x00237284 File Offset: 0x00235484
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

		// Token: 0x04005713 RID: 22291
		private const int CurrentAPIVersion = 1;

		// Token: 0x04005714 RID: 22292
		private static readonly RpcClientWrapper instance = new RpcClientWrapper();

		// Token: 0x04005715 RID: 22293
		private readonly object initializeLockObject = new object();

		// Token: 0x04005716 RID: 22294
		private readonly ITopologyConfigurationSession rootOrgConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 44, "rootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\UnifiedPolicy\\RpcClientWrapper.cs");

		// Token: 0x04005717 RID: 22295
		private bool initialized;

		// Token: 0x04005718 RID: 22296
		private Server localServer;
	}
}
