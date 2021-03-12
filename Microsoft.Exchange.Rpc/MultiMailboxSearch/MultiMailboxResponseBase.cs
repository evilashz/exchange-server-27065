using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	internal abstract class MultiMailboxResponseBase : MultiMailboxSearchBase
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0000A2D0 File Offset: 0x000096D0
		internal MultiMailboxResponseBase(int version) : base(version)
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000A2B8 File Offset: 0x000096B8
		internal MultiMailboxResponseBase() : base(MultiMailboxSearchBase.CurrentVersion)
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0000A2E4 File Offset: 0x000096E4
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0000D420 File Offset: 0x0000C820
		internal MultiMailboxSearchResultItem[] Results
		{
			get
			{
				return this.results;
			}
			set
			{
				this.results = value;
				if (value != null)
				{
					this.count = (long)value.Length;
				}
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000A2F8 File Offset: 0x000096F8
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0000A30C File Offset: 0x0000970C
		internal long Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x04000B1C RID: 2844
		private MultiMailboxSearchResultItem[] results;

		// Token: 0x04000B1D RID: 2845
		private long count;
	}
}
