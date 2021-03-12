using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000045 RID: 69
	internal sealed class RegexCharSet
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00007D61 File Offset: 0x00005F61
		public RegexCharSet()
		{
			this.dict = new Dictionary<RegexCharacterClass, List<RegexTreeNode>>();
		}

		// Token: 0x1700007B RID: 123
		public List<RegexTreeNode> this[RegexCharacterClass cl]
		{
			get
			{
				return this.dict[cl];
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007D84 File Offset: 0x00005F84
		public void Add(RegexCharacterClass cl, RegexTreeNode node)
		{
			List<RegexTreeNode> list;
			if (!this.dict.ContainsKey(cl))
			{
				list = new List<RegexTreeNode>();
				this.dict.Add(cl, list);
			}
			else
			{
				list = this.dict[cl];
			}
			list.Add(node);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007F54 File Offset: 0x00006154
		public IEnumerable<RegexCharacterClass> Chars()
		{
			foreach (RegexCharacterClass cl in this.dict.Keys)
			{
				yield return cl;
			}
			yield break;
		}

		// Token: 0x040000D6 RID: 214
		private Dictionary<RegexCharacterClass, List<RegexTreeNode>> dict;
	}
}
