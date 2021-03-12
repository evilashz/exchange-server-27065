using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002BA RID: 698
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SeedStateGraph
	{
		// Token: 0x06001B38 RID: 6968 RVA: 0x0007554C File Offset: 0x0007374C
		static SeedStateGraph()
		{
			SeedStateGraph.m_stateGraph[0, 1] = true;
			SeedStateGraph.m_stateGraph[0, 4] = true;
			SeedStateGraph.m_stateGraph[0, 5] = true;
			SeedStateGraph.m_stateGraph[1, 2] = true;
			SeedStateGraph.m_stateGraph[1, 4] = true;
			SeedStateGraph.m_stateGraph[1, 5] = true;
			SeedStateGraph.m_stateGraph[2, 3] = true;
			SeedStateGraph.m_stateGraph[2, 4] = true;
			SeedStateGraph.m_stateGraph[2, 5] = true;
			SeedStateGraph.m_stateGraph[5, 5] = true;
			SeedStateGraph.m_stateGraph[3, 5] = true;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000755F4 File Offset: 0x000737F4
		public static bool IsTransitionPossible(SeederState fromState, SeederState toState)
		{
			return SeedStateGraph.m_stateGraph[(int)fromState, (int)toState];
		}

		// Token: 0x04000AF1 RID: 2801
		private static bool[,] m_stateGraph = new bool[6, 6];
	}
}
