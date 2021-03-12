using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Rpc.Common;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000127 RID: 295
	internal abstract class AmRpcServerBase : RpcServerBase
	{
		// Token: 0x060006BB RID: 1723
		public abstract RpcErrorExceptionInfo RpcsGetServerForDatabase(Guid guid, ref AmDbStatusInfo2 dbInfo);

		// Token: 0x060006BC RID: 1724
		public abstract RpcErrorExceptionInfo MountDatabase(Guid guid, int storeFlags, int amFlags, int mountDialoverride);

		// Token: 0x060006BD RID: 1725
		public abstract RpcErrorExceptionInfo DismountDatabase(Guid guid, int flags);

		// Token: 0x060006BE RID: 1726
		public abstract RpcErrorExceptionInfo MoveDatabaseEx(Guid guid, int flags, int dismountFlags, int mountDialOverride, string fromServer, string targetServer, [MarshalAs(UnmanagedType.U1)] bool tryOtherHealthyServers, int skipValidationChecks, int actionCode, string moveComment, ref AmDatabaseMoveResult databaseMoveResult);

		// Token: 0x060006BF RID: 1727
		public abstract RpcErrorExceptionInfo AttemptCopyLastLogsDirect(Guid guid, int mountDialOverride, int numRetries, int e00timeoutMs, int networkIOtimeoutMs, int networkConnecttimeoutMs, string sourceServer, int actionCode, int skipValidationChecks, [MarshalAs(UnmanagedType.U1)] bool mountPending, string uniqueOperationId, int subactionAttemptNumber, ref AmAcllReturnStatus acllStatus);

		// Token: 0x060006C0 RID: 1728
		public abstract RpcErrorExceptionInfo MountDatabaseDirect(Guid guid, AmMountArg mountArg);

		// Token: 0x060006C1 RID: 1729
		public abstract RpcErrorExceptionInfo DismountDatabaseDirect(Guid guid, AmDismountArg dismountArg);

		// Token: 0x060006C2 RID: 1730
		public abstract RpcErrorExceptionInfo IsRunning();

		// Token: 0x060006C3 RID: 1731
		public abstract RpcErrorExceptionInfo GetPrimaryActiveManager(ref AmPamInfo pamInfo);

		// Token: 0x060006C4 RID: 1732
		public abstract RpcErrorExceptionInfo ServerSwitchOver(string sourceServer);

		// Token: 0x060006C5 RID: 1733
		public abstract RpcErrorExceptionInfo GetActiveManagerRole(ref AmRole amRole, ref string errorMessage);

		// Token: 0x060006C6 RID: 1734
		public abstract RpcErrorExceptionInfo CheckThirdPartyListener(ref bool healthy, ref string errorMessage);

		// Token: 0x060006C7 RID: 1735
		public abstract RpcErrorExceptionInfo ReportSystemEvent(int eventCode, string reportingServer);

		// Token: 0x060006C8 RID: 1736
		public abstract RpcErrorExceptionInfo RemountDatabase(Guid guid, int mountFlags, int mountDialOverride, string fromServer);

		// Token: 0x060006C9 RID: 1737
		public abstract RpcErrorExceptionInfo ServerMoveAllDatabases(string sourceServer, string targetServer, int mountFlags, int dismountFlags, int mountDialOverride, int tryOtherHealthyServers, int skipValidationChecks, int actionCode, string moveComment, string componentName, ref List<AmDatabaseMoveResult> databaseMoveResults);

		// Token: 0x060006CA RID: 1738
		public abstract RpcErrorExceptionInfo RpcsGetAutomountConsensusState(ref int automountConsensusState);

		// Token: 0x060006CB RID: 1739
		public abstract RpcErrorExceptionInfo RpcsSetAutomountConsensusState(int automountConsensusState);

		// Token: 0x060006CC RID: 1740
		public abstract RpcErrorExceptionInfo AmRefreshConfiguration(int refreshFlags, int maxSecondsToWait);

		// Token: 0x060006CD RID: 1741
		public abstract RpcErrorExceptionInfo ReportServiceKill(string serviceName, string serverName, string timeStampStrInUtc);

		// Token: 0x060006CE RID: 1742
		public abstract RpcErrorExceptionInfo GetDeferredRecoveryEntries(ref List<AmDeferredRecoveryEntry> entries);

		// Token: 0x060006CF RID: 1743
		public abstract RpcErrorExceptionInfo GenericRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo);

		// Token: 0x060006D0 RID: 1744 RVA: 0x00005224 File Offset: 0x00004624
		public AmRpcServerBase()
		{
		}

		// Token: 0x0400099A RID: 2458
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IActiveManagerRpc_v3_0_s_ifspec;
	}
}
