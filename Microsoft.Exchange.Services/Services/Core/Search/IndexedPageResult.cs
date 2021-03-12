using System;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200026C RID: 620
	internal class IndexedPageResult : BasePageResult
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0004E11C File Offset: 0x0004C31C
		public IndexedPageResult(BaseQueryView view, int indexedOffset) : base(view)
		{
			this.indexedOffset = indexedOffset;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0004E12C File Offset: 0x0004C32C
		public int IndexedOffset
		{
			get
			{
				return this.indexedOffset;
			}
		}

		// Token: 0x04000C08 RID: 3080
		private int indexedOffset;
	}
}
