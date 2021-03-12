using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x0200004A RID: 74
	internal sealed class RegexStateSet
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00009140 File Offset: 0x00007340
		public RegexStateSet()
		{
			this.list = new List<RegexState>();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009278 File Offset: 0x00007478
		public IEnumerable<RegexState> UnmarkedStates()
		{
			for (int counter = 0; counter < this.list.Count; counter++)
			{
				if (!this.list[counter].Marked)
				{
					yield return this.list[counter];
				}
			}
			yield break;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009295 File Offset: 0x00007495
		public void Add(RegexState state)
		{
			this.list.Add(state);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000092A4 File Offset: 0x000074A4
		public RegexState MatchingState(RegexState state)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].Equals(state))
				{
					return this.list[i];
				}
			}
			return null;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000092EC File Offset: 0x000074EC
		public bool Contains(RegexState state)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].Equals(state))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000E5 RID: 229
		private List<RegexState> list;
	}
}
