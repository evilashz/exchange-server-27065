using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000021 RID: 33
	internal sealed class ActiveState
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000C163 File Offset: 0x0000A363
		public ActiveState(TrieNode node, int nodeIndex, int initialCount)
		{
			this.Initialize(node, nodeIndex, initialCount);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000C174 File Offset: 0x0000A374
		private bool Terminal
		{
			get
			{
				return this.isTerminal;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000C17C File Offset: 0x0000A37C
		private int NodeId
		{
			get
			{
				return this.nodeId;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000C184 File Offset: 0x0000A384
		private int TransitionCount
		{
			get
			{
				return this.transitionCount;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000C18C File Offset: 0x0000A38C
		private List<long> IDs
		{
			get
			{
				return this.terminalIDs;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C194 File Offset: 0x0000A394
		public static void TransitionStates(char ch, int position, BoundaryType boundaryType, RopeList<TrieNode> nodes, ActiveStatePool activeStatePool, List<ActiveState> currentStates, List<ActiveState> newStates, ref SearchResult result)
		{
			foreach (ActiveState activeState in currentStates)
			{
				if (activeState.Terminal && StringHelper.IsRightHandSideDelimiter(ch, boundaryType))
				{
					result.AddResult(activeState.IDs, (long)activeState.NodeId, position - activeState.TransitionCount, position - 1);
				}
				if (activeState.Transition(ch, nodes))
				{
					activeState.IncrementTransitionCount();
					newStates.Add(activeState);
				}
				else
				{
					activeStatePool.FreeActiveState(activeState);
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000C230 File Offset: 0x0000A430
		public void Reinitialize(TrieNode node, int nodeIndex, int initialCount)
		{
			this.Initialize(node, nodeIndex, initialCount);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000C23C File Offset: 0x0000A43C
		private bool Transition(char ch, RopeList<TrieNode> nodes)
		{
			if (nodes[this.nodeId].Transition(ch, this.nodePosition, nodes, ref this.nodeId, ref this.nodePosition))
			{
				this.isTerminal = nodes[this.nodeId].IsTerminal(this.nodePosition);
				this.terminalIDs = nodes[this.nodeId].TerminalIDs;
				return true;
			}
			return false;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		private void IncrementTransitionCount()
		{
			this.transitionCount++;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		private void Initialize(TrieNode node, int nodeIndex, int initialCount)
		{
			this.nodePosition = 0;
			this.transitionCount = initialCount;
			this.nodeId = nodeIndex;
			this.terminalIDs = node.TerminalIDs;
			this.isTerminal = node.IsTerminal(this.nodePosition);
		}

		// Token: 0x040000DB RID: 219
		private int nodePosition;

		// Token: 0x040000DC RID: 220
		private int nodeId;

		// Token: 0x040000DD RID: 221
		private int transitionCount;

		// Token: 0x040000DE RID: 222
		private bool isTerminal;

		// Token: 0x040000DF RID: 223
		private List<long> terminalIDs;
	}
}
