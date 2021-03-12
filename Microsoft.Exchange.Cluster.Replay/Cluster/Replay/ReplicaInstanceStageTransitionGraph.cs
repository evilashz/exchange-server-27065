using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000146 RID: 326
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ReplicaInstanceStageTransitionGraph
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00036F7B File Offset: 0x0003517B
		static ReplicaInstanceStageTransitionGraph()
		{
			ReplicaInstanceStageTransitionGraph.m_graph[0, 1] = true;
			ReplicaInstanceStageTransitionGraph.m_graph[1, 2] = true;
			ReplicaInstanceStageTransitionGraph.m_graph[2, 3] = true;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00036FB0 File Offset: 0x000351B0
		public static bool IsTransitionPossible(ReplicaInstanceStage fromStage, ReplicaInstanceStage toStage)
		{
			return ReplicaInstanceStageTransitionGraph.m_graph[(int)fromStage, (int)toStage];
		}

		// Token: 0x04000555 RID: 1365
		private static bool[,] m_graph = new bool[5, 5];
	}
}
