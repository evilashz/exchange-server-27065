using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x020001C9 RID: 457
	internal abstract class ReplayRpcServerBase : RpcServerBase
	{
		// Token: 0x060009B6 RID: 2486
		public abstract RpcErrorExceptionInfo RequestSuspend(Guid guid, string suspendComment);

		// Token: 0x060009B7 RID: 2487
		public abstract RpcErrorExceptionInfo RequestResume(Guid guid);

		// Token: 0x060009B8 RID: 2488
		public abstract RpcErrorExceptionInfo GetCopyStatus(RpcGetDatabaseCopyStatusFlags collectionFlags, Guid[] dbGuids, ref RpcDatabaseCopyStatus[] dbStatuses);

		// Token: 0x060009B9 RID: 2489
		public abstract RpcErrorExceptionInfo GetCopyStatus2(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, out int nCopyStatusesReturned, out byte[] dbStatuses);

		// Token: 0x060009BA RID: 2490
		public abstract RpcErrorExceptionInfo GetCopyStatusBasic(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, ref RpcDatabaseCopyStatusBasic[] dbStatuses);

		// Token: 0x060009BB RID: 2491
		public abstract RpcErrorExceptionInfo RunConfigurationUpdater();

		// Token: 0x060009BC RID: 2492
		public abstract RpcErrorExceptionInfo GetDagNetworkConfig(ref byte[] dagNetConfig);

		// Token: 0x060009BD RID: 2493
		public abstract RpcErrorExceptionInfo SetDagNetwork(byte[] networkChange);

		// Token: 0x060009BE RID: 2494
		public abstract RpcErrorExceptionInfo SetDagNetworkConfig(byte[] networkConfigChange);

		// Token: 0x060009BF RID: 2495
		public abstract RpcErrorExceptionInfo RemoveDagNetwork(byte[] deleteRequest);

		// Token: 0x060009C0 RID: 2496
		public abstract RpcErrorExceptionInfo CancelDbSeed(Guid dbGuid);

		// Token: 0x060009C1 RID: 2497
		public abstract RpcErrorExceptionInfo EndDbSeed(Guid dbGuid);

		// Token: 0x060009C2 RID: 2498
		public abstract RpcErrorExceptionInfo RpcsGetDatabaseSeedStatus(Guid dbGuid, ref RpcSeederStatus pSeederStatus);

		// Token: 0x060009C3 RID: 2499
		public abstract RpcErrorExceptionInfo RpcsPrepareDatabaseSeedAndBegin(RpcSeederArgs seederArgs, ref RpcSeederStatus seederStatus);

		// Token: 0x060009C4 RID: 2500
		public abstract RpcErrorExceptionInfo RequestSuspend2(Guid guid, string suspendComment, uint flags);

		// Token: 0x060009C5 RID: 2501
		public abstract RpcErrorExceptionInfo RequestSuspend3(Guid guid, string suspendComment, uint flags, uint actionInitiator);

		// Token: 0x060009C6 RID: 2502
		public abstract RpcErrorExceptionInfo RequestResume2(Guid guid, uint flags);

		// Token: 0x060009C7 RID: 2503
		public abstract RpcErrorExceptionInfo RpcsDisableReplayLag2(Guid dbGuid, string disableReason, ActionInitiatorType actionInitiator);

		// Token: 0x060009C8 RID: 2504
		public abstract RpcErrorExceptionInfo RpcsEnableReplayLag2(Guid dbGuid, ActionInitiatorType actionInitiator);

		// Token: 0x060009C9 RID: 2505
		public abstract RpcErrorExceptionInfo RpcsNotifyChangedReplayConfiguration(Guid guid, [MarshalAs(UnmanagedType.U1)] bool waitForCompletion, [MarshalAs(UnmanagedType.U1)] bool exitAfterEnqueueing, [MarshalAs(UnmanagedType.U1)] bool isHighPriority, int changeHint);

		// Token: 0x060009CA RID: 2506
		public abstract RpcErrorExceptionInfo RpcsInstallFailoverClustering(ref string verboseLog);

		// Token: 0x060009CB RID: 2507
		public abstract RpcErrorExceptionInfo RpcsCreateCluster(string clusterName, string firstNodeName, string[] ipAddresses, uint[] rgNetMasks, ref string verboseLog);

		// Token: 0x060009CC RID: 2508
		public abstract RpcErrorExceptionInfo RpcsDestroyCluster(string clusterName, ref string verboseLog);

		// Token: 0x060009CD RID: 2509
		public abstract RpcErrorExceptionInfo RpcsAddNodeToCluster(string newNode, ref string verboseLog);

		// Token: 0x060009CE RID: 2510
		public abstract RpcErrorExceptionInfo RpcsEvictNodeFromCluster(string convictedNode, ref string verboseLog);

		// Token: 0x060009CF RID: 2511
		public abstract RpcErrorExceptionInfo RpcsForceCleanupNode(ref string verboseLog);

		// Token: 0x060009D0 RID: 2512
		public abstract RpcErrorExceptionInfo GetCopyStatusWithHealthState(RpcGetDatabaseCopyStatusFlags2 collectionFlags2, Guid[] dbGuids, ref RpcCopyStatusContainer container);

		// Token: 0x060009D1 RID: 2513 RVA: 0x000198E4 File Offset: 0x00018CE4
		public ReplayRpcServerBase()
		{
		}

		// Token: 0x04000B71 RID: 2929
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IReplayRpc_v3_0_s_ifspec;

		// Token: 0x04000B72 RID: 2930
		public byte[] m_sdBaseSystemBinaryForm;
	}
}
