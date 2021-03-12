using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001FF RID: 511
	internal class NonIndexableItemPagingInfo
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003BA57 File Offset: 0x00039C57
		public NonIndexableItemPagingInfo(int pageSize, string pageReferenceItem)
		{
			this.PageSize = pageSize;
			this.PageItemReference = pageReferenceItem;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0003BA6D File Offset: 0x00039C6D
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0003BA75 File Offset: 0x00039C75
		public int PageSize { get; private set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0003BA7E File Offset: 0x00039C7E
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0003BA86 File Offset: 0x00039C86
		public string PageItemReference { get; private set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0003BA8F File Offset: 0x00039C8F
		public PageDirection PageDirection
		{
			get
			{
				return PageDirection.Next;
			}
		}
	}
}
