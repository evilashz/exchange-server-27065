using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000132 RID: 306
	internal sealed class InstantMessageCategoryVersion
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x00045A51 File Offset: 0x00043C51
		public InstantMessageCategoryVersion(int categoryIndex, int categoryVersion)
		{
			this.categoryIndex = categoryIndex;
			this.categoryVersion = categoryVersion;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00045A67 File Offset: 0x00043C67
		public int CategoryIndex
		{
			get
			{
				return this.categoryIndex;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00045A6F File Offset: 0x00043C6F
		public int CategoryVersion
		{
			get
			{
				return this.categoryVersion;
			}
		}

		// Token: 0x04000784 RID: 1924
		private int categoryIndex;

		// Token: 0x04000785 RID: 1925
		private int categoryVersion;
	}
}
