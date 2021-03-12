using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000028 RID: 40
	internal sealed class ArrayTrieStatePool
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		public void FreeActiveState(ArrayTrieState state)
		{
			this.pool.Add(state);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public ArrayTrieState GetActiveState(ArrayTrieNode node, int initialCount, int nodeIndex)
		{
			if (this.pool.Count > 0)
			{
				ArrayTrieState arrayTrieState = this.pool[this.pool.Count - 1];
				this.pool.RemoveAt(this.pool.Count - 1);
				arrayTrieState.Reinitialize(node, initialCount, nodeIndex);
				return arrayTrieState;
			}
			return new ArrayTrieState(node, initialCount, nodeIndex);
		}

		// Token: 0x040000F7 RID: 247
		private List<ArrayTrieState> pool = new List<ArrayTrieState>(16);
	}
}
