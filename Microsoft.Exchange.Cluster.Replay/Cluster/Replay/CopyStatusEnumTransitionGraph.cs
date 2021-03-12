using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000147 RID: 327
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CopyStatusEnumTransitionGraph
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x00036FC0 File Offset: 0x000351C0
		static CopyStatusEnumTransitionGraph()
		{
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Unknown, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Unknown, CopyStatusEnum.FailedAndSuspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Unknown, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Initializing, CopyStatusEnum.Resynchronizing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Initializing, CopyStatusEnum.Failed);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Initializing, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Resynchronizing, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Resynchronizing, CopyStatusEnum.DisconnectedAndResynchronizing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Resynchronizing, CopyStatusEnum.Healthy);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Resynchronizing, CopyStatusEnum.Failed);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Resynchronizing, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndResynchronizing, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndResynchronizing, CopyStatusEnum.DisconnectedAndHealthy);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndResynchronizing, CopyStatusEnum.Resynchronizing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndResynchronizing, CopyStatusEnum.Failed);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndResynchronizing, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndHealthy, CopyStatusEnum.DisconnectedAndResynchronizing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndHealthy, CopyStatusEnum.Healthy);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndHealthy, CopyStatusEnum.Failed);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndHealthy, CopyStatusEnum.SeedingSource);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.DisconnectedAndHealthy, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Healthy, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Healthy, CopyStatusEnum.DisconnectedAndHealthy);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Healthy, CopyStatusEnum.Failed);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Healthy, CopyStatusEnum.SeedingSource);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Healthy, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Failed, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Failed, CopyStatusEnum.Resynchronizing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Failed, CopyStatusEnum.DisconnectedAndResynchronizing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Failed, CopyStatusEnum.Healthy);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Failed, CopyStatusEnum.FailedAndSuspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.FailedAndSuspended, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.FailedAndSuspended, CopyStatusEnum.Seeding);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Suspended, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Suspended, CopyStatusEnum.Seeding);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Suspended, CopyStatusEnum.FailedAndSuspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Seeding, CopyStatusEnum.FailedAndSuspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.Seeding, CopyStatusEnum.Suspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.SeedingSource, CopyStatusEnum.Healthy);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.SeedingSource, CopyStatusEnum.Failed);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.SeedingSource, CopyStatusEnum.FailedAndSuspended);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.SeedingSource, CopyStatusEnum.Initializing);
			CopyStatusEnumTransitionGraph.AddTransition(CopyStatusEnum.SeedingSource, CopyStatusEnum.DisconnectedAndHealthy);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00037113 File Offset: 0x00035313
		public static bool IsTransitionPossible(CopyStatusEnum fromState, CopyStatusEnum toState)
		{
			return CopyStatusEnumTransitionGraph.m_graph[(int)fromState, (int)toState];
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00037121 File Offset: 0x00035321
		private static void AddTransition(CopyStatusEnum fromState, CopyStatusEnum toState)
		{
			CopyStatusEnumTransitionGraph.m_graph[(int)fromState, (int)toState] = true;
		}

		// Token: 0x04000556 RID: 1366
		private static bool[,] m_graph = new bool[16, 16];
	}
}
