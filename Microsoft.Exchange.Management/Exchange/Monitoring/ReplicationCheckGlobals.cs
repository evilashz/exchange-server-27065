using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200054D RID: 1357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ReplicationCheckGlobals
	{
		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000C40C1 File Offset: 0x000C22C1
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x000C40E1 File Offset: 0x000C22E1
		internal static bool RunningInMonitoringContext
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["RunningInMonitoringContext"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["RunningInMonitoringContext"] = value;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x000C40F8 File Offset: 0x000C22F8
		// (set) Token: 0x06003053 RID: 12371 RVA: 0x000C4118 File Offset: 0x000C2318
		internal static ServerConfig ServerConfiguration
		{
			get
			{
				return (ServerConfig)(ReplicationCheckGlobals.s_fields["ServerConfiguration"] ?? ServerConfig.Unknown);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["ServerConfiguration"] = value;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000C412F File Offset: 0x000C232F
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x000C414F File Offset: 0x000C234F
		internal static bool UsingReplayRpc
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["UsingReplayRpc"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["UsingReplayRpc"] = value;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000C4166 File Offset: 0x000C2366
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x000C4186 File Offset: 0x000C2386
		internal static bool ReplayServiceCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["ReplayServiceCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["ReplayServiceCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x000C419D File Offset: 0x000C239D
		// (set) Token: 0x06003059 RID: 12377 RVA: 0x000C41BD File Offset: 0x000C23BD
		internal static bool ActiveManagerCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["ActiveManagerCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["ActiveManagerCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000C41D4 File Offset: 0x000C23D4
		// (set) Token: 0x0600305B RID: 12379 RVA: 0x000C41F4 File Offset: 0x000C23F4
		internal static bool ThirdPartyReplCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["ThirdPartyReplCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["ThirdPartyReplCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000C420B File Offset: 0x000C240B
		// (set) Token: 0x0600305D RID: 12381 RVA: 0x000C422B File Offset: 0x000C242B
		internal static bool TasksRpcListenerCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["TasksRpcListenerCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["TasksRpcListenerCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000C4242 File Offset: 0x000C2442
		// (set) Token: 0x0600305F RID: 12383 RVA: 0x000C4262 File Offset: 0x000C2462
		internal static bool TcpListenerCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["TcpListenerCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["TcpListenerCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000C4279 File Offset: 0x000C2479
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x000C4299 File Offset: 0x000C2499
		internal static bool DatabaseRedundancyCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["DatabaseRedundancyCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["DatabaseRedundancyCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000C42B0 File Offset: 0x000C24B0
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x000C42D0 File Offset: 0x000C24D0
		internal static bool DatabaseAvailabilityCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["DatabaseAvailabilityCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["DatabaseAvailabilityCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000C42E7 File Offset: 0x000C24E7
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x000C4307 File Offset: 0x000C2507
		internal static bool ServerLocatorServiceCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["ServerLocatorServiceCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["ServerLocatorServiceCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000C431E File Offset: 0x000C251E
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x000C433E File Offset: 0x000C253E
		internal static bool MonitoringServiceCheckHasRun
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["MonitoringServiceCheckHasRun"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["MonitoringServiceCheckHasRun"] = value;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000C4355 File Offset: 0x000C2555
		// (set) Token: 0x06003069 RID: 12393 RVA: 0x000C4375 File Offset: 0x000C2575
		internal static bool IsReplayServiceDown
		{
			get
			{
				return (bool)(ReplicationCheckGlobals.s_fields["IsReplayServiceDown"] ?? false);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["IsReplayServiceDown"] = value;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000C438C File Offset: 0x000C258C
		// (set) Token: 0x0600306B RID: 12395 RVA: 0x000C43AC File Offset: 0x000C25AC
		internal static AmRole ActiveManagerRole
		{
			get
			{
				return (AmRole)(ReplicationCheckGlobals.s_fields["ActiveManagerRole"] ?? AmRole.Unknown);
			}
			set
			{
				ReplicationCheckGlobals.s_fields["ActiveManagerRole"] = value;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000C43C3 File Offset: 0x000C25C3
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x000C43D9 File Offset: 0x000C25D9
		internal static IADServer Server
		{
			get
			{
				return (IADServer)ReplicationCheckGlobals.s_fields["Server"];
			}
			set
			{
				ReplicationCheckGlobals.s_fields["Server"] = value;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000C43EB File Offset: 0x000C25EB
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x000C4401 File Offset: 0x000C2601
		internal static Dictionary<Guid, RpcDatabaseCopyStatus2> CopyStatusResults
		{
			get
			{
				return (Dictionary<Guid, RpcDatabaseCopyStatus2>)ReplicationCheckGlobals.s_fields["CopyStatusResults"];
			}
			set
			{
				ReplicationCheckGlobals.s_fields["CopyStatusResults"] = value;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000C4413 File Offset: 0x000C2613
		// (set) Token: 0x06003071 RID: 12401 RVA: 0x000C4429 File Offset: 0x000C2629
		internal static Task.TaskVerboseLoggingDelegate WriteVerboseDelegate
		{
			get
			{
				return (Task.TaskVerboseLoggingDelegate)ReplicationCheckGlobals.s_fields["WriteVerboseDelegate"];
			}
			set
			{
				ReplicationCheckGlobals.s_fields["WriteVerboseDelegate"] = value;
			}
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000C443B File Offset: 0x000C263B
		internal static void ResetState()
		{
			ReplicationCheckGlobals.s_fields.Clear();
		}

		// Token: 0x04002271 RID: 8817
		private const int NumberOfFields = 16;

		// Token: 0x04002272 RID: 8818
		private static HybridDictionary s_fields = new HybridDictionary(16, true);
	}
}
