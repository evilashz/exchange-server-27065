using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000022 RID: 34
	internal sealed class ActiveStatePool
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000C2F7 File Offset: 0x0000A4F7
		public void FreeActiveState(ActiveState state)
		{
			this.freeActiveStates.Add(state);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C308 File Offset: 0x0000A508
		public ActiveState GetActiveState(TrieNode node, int nodeIndex, int initialCount)
		{
			if (this.freeActiveStates.Count > 0)
			{
				ActiveState activeState = this.freeActiveStates[this.freeActiveStates.Count - 1];
				this.freeActiveStates.RemoveAt(this.freeActiveStates.Count - 1);
				activeState.Reinitialize(node, nodeIndex, initialCount);
				return activeState;
			}
			return new ActiveState(node, nodeIndex, initialCount);
		}

		// Token: 0x040000E0 RID: 224
		private List<ActiveState> freeActiveStates = new List<ActiveState>(16);
	}
}
