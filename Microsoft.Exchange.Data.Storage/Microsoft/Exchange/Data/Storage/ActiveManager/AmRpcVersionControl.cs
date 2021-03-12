using System;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000316 RID: 790
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AmRpcVersionControl
	{
		// Token: 0x0600236C RID: 9068 RVA: 0x00090A42 File Offset: 0x0008EC42
		public static bool IsRemountRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.RemountRpcSupportedVersion);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00090A4F File Offset: 0x0008EC4F
		public static bool IsReportSystemEventRpcSupported(ServerVersion serverVersion, AmSystemEventCode eventCode)
		{
			if (eventCode == AmSystemEventCode.StoreServiceStarted || eventCode == AmSystemEventCode.StoreServiceStopped)
			{
				return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.ReportSystemEventRpcSupportedVersion);
			}
			return eventCode == AmSystemEventCode.StoreServiceUnexpectedlyStopped && ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.UnexpectedStoreStopEventCodeSupportedVersion);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00090A76 File Offset: 0x0008EC76
		public static bool IsSwitchOverSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.SwitchOverSupportVersion);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00090A83 File Offset: 0x0008EC83
		public static bool IsGetAmRoleRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.GetAMRoleSupportVersion);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00090A90 File Offset: 0x0008EC90
		public static bool IsAttemptCopyLastLogsDirect2RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.AttemptCopyLastLogsDirect2SupportVersion);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00090A9D File Offset: 0x0008EC9D
		public static bool IsServerMoveAllDatabasesRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.ServerMoveAllDatabasesRpcSupportVersion);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00090AAA File Offset: 0x0008ECAA
		public static bool IsMoveDatabaseEx2RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.MoveDatabaseEx2RpcSupportVersion);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00090AB7 File Offset: 0x0008ECB7
		public static bool IsMoveDatabaseEx3RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.MoveDatabaseEx3RpcSupportVersion);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00090AC4 File Offset: 0x0008ECC4
		public static bool IsCheckThirdPartyListenerSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.ThirdPartyReplListenerSupportedVersion);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x00090AD1 File Offset: 0x0008ECD1
		public static bool IsAmRefreshConfigurationSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.AmRefreshConfigurationRpcSupportVersion);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00090ADE File Offset: 0x0008ECDE
		public static bool IsAttemptCopyLastLogsDirect3RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.AttemptCopyLastLogsDirect3SupportVersion);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00090AEB File Offset: 0x0008ECEB
		public static bool IsMountWithAmFlagsRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.MountWithAmFlagsSupportedVersion);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00090AF8 File Offset: 0x0008ECF8
		public static bool IsMoveWithCatalogFailureReasonCodeSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.MoveWithCatalogFailureReasonCodeVersion);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00090B05 File Offset: 0x0008ED05
		public static bool IsReportServiceKillRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.ReportServiceKillSupportedVersion);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00090B12 File Offset: 0x0008ED12
		public static bool IsGetDeferredRecoveryEntriesRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.GetDeferredRecoveryEntriesRpcSupportedSupportedVersion);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00090B1F File Offset: 0x0008ED1F
		public static bool IsServerMoveAllDatabases3RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.AmServerMoveAllDatabases3RpcSupportedVersion);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00090B2C File Offset: 0x0008ED2C
		public static bool IsServerMoveBackDatabasesRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, AmRpcVersionControl.AmServerMoveBackDatabasesRpcSupportedVersion);
		}

		// Token: 0x04001500 RID: 5376
		public static readonly ServerVersion SwitchOverSupportVersion = new ServerVersion(14, 0, 244, 0);

		// Token: 0x04001501 RID: 5377
		public static readonly ServerVersion GetAMRoleSupportVersion = new ServerVersion(14, 0, 288, 0);

		// Token: 0x04001502 RID: 5378
		public static readonly ServerVersion ReportSystemEventRpcSupportedVersion = new ServerVersion(14, 0, 455, 0);

		// Token: 0x04001503 RID: 5379
		public static readonly ServerVersion RemountRpcSupportedVersion = new ServerVersion(14, 0, 464, 0);

		// Token: 0x04001504 RID: 5380
		public static readonly ServerVersion UnexpectedStoreStopEventCodeSupportedVersion = new ServerVersion(14, 0, 522, 0);

		// Token: 0x04001505 RID: 5381
		public static readonly ServerVersion AttemptCopyLastLogsDirect2SupportVersion = new ServerVersion(14, 0, 572, 0);

		// Token: 0x04001506 RID: 5382
		public static readonly ServerVersion ThirdPartyReplListenerSupportedVersion = new ServerVersion(14, 0, 572, 0);

		// Token: 0x04001507 RID: 5383
		public static readonly ServerVersion ServerMoveAllDatabasesRpcSupportVersion = new ServerVersion(14, 0, 579, 0);

		// Token: 0x04001508 RID: 5384
		public static readonly ServerVersion MoveDatabaseEx2RpcSupportVersion = new ServerVersion(14, 0, 579, 0);

		// Token: 0x04001509 RID: 5385
		public static readonly ServerVersion AmRefreshConfigurationRpcSupportVersion = new ServerVersion(14, 0, 601, 0);

		// Token: 0x0400150A RID: 5386
		public static readonly ServerVersion AttemptCopyLastLogsDirect3SupportVersion = new ServerVersion(14, 0, 636, 0);

		// Token: 0x0400150B RID: 5387
		public static readonly ServerVersion MoveDatabaseEx3RpcSupportVersion = new ServerVersion(14, 1, 115, 0);

		// Token: 0x0400150C RID: 5388
		public static readonly ServerVersion MountWithAmFlagsSupportedVersion = new ServerVersion(14, 1, 135, 0);

		// Token: 0x0400150D RID: 5389
		public static readonly ServerVersion MoveWithCatalogFailureReasonCodeVersion = new ServerVersion(14, 2, 5038, 0);

		// Token: 0x0400150E RID: 5390
		public static readonly ServerVersion ReportServiceKillSupportedVersion = new ServerVersion(14, 2, 5114, 0);

		// Token: 0x0400150F RID: 5391
		public static readonly ServerVersion GetDeferredRecoveryEntriesRpcSupportedSupportedVersion = new ServerVersion(14, 2, 5114, 0);

		// Token: 0x04001510 RID: 5392
		public static readonly ServerVersion AmServerMoveAllDatabases3RpcSupportedVersion = new ServerVersion(15, 0, 337, 0);

		// Token: 0x04001511 RID: 5393
		public static readonly ServerVersion AmServerMoveBackDatabasesRpcSupportedVersion = new ServerVersion(15, 0, 997, 0);
	}
}
