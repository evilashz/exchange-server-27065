using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ReplayRpcVersionControl
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002C5E File Offset: 0x00000E5E
		public static bool IsGetCopyStatusEx2RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.GetCopyStatusEx2SupportVersion);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C6B File Offset: 0x00000E6B
		public static bool IsSuspendRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.SuspendRpcSupportVersion);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C78 File Offset: 0x00000E78
		public static bool IsSeedRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.SeedRpcSupportVersion);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C85 File Offset: 0x00000E85
		public static bool IsSeedRpcSafeDeleteSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.SeedRpcSafeDeleteSupportVersion);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C92 File Offset: 0x00000E92
		public static bool IsSeedRpcV5Supported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.SeedRpcV5SupportVersion);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C9F File Offset: 0x00000E9F
		public static bool IsRunConfigUpdaterRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.RunConfigUpdaterRpcSupportVersion);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002CAC File Offset: 0x00000EAC
		public static bool IsActivationRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.ActivationRpcSupportVersion);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public static bool IsNotifyChangedReplayConfigurationRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.NotifyChangedReplayConfigurationSupportVersion);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CC6 File Offset: 0x00000EC6
		public static bool IsRequestSuspend3RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.RequestSuspend3SupportVersion);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CD3 File Offset: 0x00000ED3
		public static bool IsSeedFromPassiveSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.SeedFromPassiveSupportVersion);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static bool IsGetCopyStatusEx4RpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CED File Offset: 0x00000EED
		public static bool IsGetCopyStatusBasicRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CFA File Offset: 0x00000EFA
		public static bool IsDisableReplayLagRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D07 File Offset: 0x00000F07
		public static bool IsDisableReplayLagRpcV2Supported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.DisableReplayLagRpcV2SupportVersion);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D14 File Offset: 0x00000F14
		public static bool IsGetCopyStatusWithHealthStateRpcSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.IsGetCopyStatusWithHealthStateRpcSupportVersion);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D21 File Offset: 0x00000F21
		public static bool IsServerLocatorServiceSupported(ServerVersion serverVersion)
		{
			return ReplayRpcVersionControl.IsVersionGreater(serverVersion, ReplayRpcVersionControl.IsServerLocatorServiceSupportVersion);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D2E File Offset: 0x00000F2E
		public static bool IsVersionGreater(ServerVersion a, ServerVersion b)
		{
			return ServerVersion.Compare(a, b) >= 0;
		}

		// Token: 0x0400001D RID: 29
		public static readonly ServerVersion SuspendRpcSupportVersion = new ServerVersion(14, 0, 145, 0);

		// Token: 0x0400001E RID: 30
		public static readonly ServerVersion SeedRpcSupportVersion = new ServerVersion(14, 0, 267, 0);

		// Token: 0x0400001F RID: 31
		public static readonly ServerVersion SeedRpcSafeDeleteSupportVersion = new ServerVersion(15, 0, 585, 0);

		// Token: 0x04000020 RID: 32
		public static readonly ServerVersion SeedRpcV5SupportVersion = new ServerVersion(15, 0, 645, 0);

		// Token: 0x04000021 RID: 33
		public static readonly ServerVersion DisableReplayLagRpcV2SupportVersion = new ServerVersion(15, 0, 691, 0);

		// Token: 0x04000022 RID: 34
		public static readonly ServerVersion GetCopyStatusEx2SupportVersion = new ServerVersion(14, 0, 288, 0);

		// Token: 0x04000023 RID: 35
		public static readonly ServerVersion RunConfigUpdaterRpcSupportVersion = new ServerVersion(14, 0, 324, 0);

		// Token: 0x04000024 RID: 36
		public static readonly ServerVersion ActivationRpcSupportVersion = new ServerVersion(14, 0, 408, 0);

		// Token: 0x04000025 RID: 37
		public static readonly ServerVersion GetCopyStatusEx3SupportVersion = new ServerVersion(14, 0, 455, 0);

		// Token: 0x04000026 RID: 38
		public static readonly ServerVersion NotifyChangedReplayConfigurationSupportVersion = new ServerVersion(14, 0, 572, 0);

		// Token: 0x04000027 RID: 39
		public static readonly ServerVersion RequestSuspend3SupportVersion = new ServerVersion(14, 0, 572, 0);

		// Token: 0x04000028 RID: 40
		public static readonly ServerVersion SeedFromPassiveSupportVersion = new ServerVersion(14, 0, 572, 0);

		// Token: 0x04000029 RID: 41
		public static readonly ServerVersion GetCopyStatusEx4RpcSupportVersion = new ServerVersion(15, 0, 202, 0);

		// Token: 0x0400002A RID: 42
		public static readonly ServerVersion IsGetCopyStatusWithHealthStateRpcSupportVersion = new ServerVersion(15, 0, 339, 0);

		// Token: 0x0400002B RID: 43
		public static readonly ServerVersion IsServerLocatorServiceSupportVersion = new ServerVersion(15, 0, 413, 0);
	}
}
