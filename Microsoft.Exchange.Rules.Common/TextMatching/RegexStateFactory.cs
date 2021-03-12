using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000049 RID: 73
	internal sealed class RegexStateFactory
	{
		// Token: 0x060001FD RID: 509 RVA: 0x000090E0 File Offset: 0x000072E0
		public RegexState CreateRegexState()
		{
			return new RegexState(this.stateid++);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009103 File Offset: 0x00007303
		public void Destory()
		{
			this.stateid--;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009114 File Offset: 0x00007314
		public RegexState CreateRegexState(List<RegexTreeNode> nodes)
		{
			return new RegexState(this.stateid++, nodes);
		}

		// Token: 0x040000E4 RID: 228
		private int stateid;
	}
}
