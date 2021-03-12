using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	internal abstract class MultiMailboxSearchResultItem : MultiMailboxSearchBase
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0000A0FC File Offset: 0x000094FC
		protected MultiMailboxSearchResultItem(int version) : base(version)
		{
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000A0E4 File Offset: 0x000094E4
		protected MultiMailboxSearchResultItem() : base(MultiMailboxSearchBase.CurrentVersion)
		{
		}
	}
}
