using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000044 RID: 68
	internal class TrieInfo
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public TrieInfo(long id, Trie trie)
		{
			this.id = id;
			this.trie = trie;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000EDBE File Offset: 0x0000CFBE
		public Trie Trie
		{
			get
			{
				return this.trie;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000EDC6 File Offset: 0x0000CFC6
		public long ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000167 RID: 359
		private readonly long id;

		// Token: 0x04000168 RID: 360
		private Trie trie;
	}
}
