using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	internal sealed class PagingInfo : MultiMailboxSearchBase
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x00009D7C File Offset: 0x0000917C
		internal PagingInfo(int version, int pageSize, PagingDirection direction, long referenceDocumentId, PagingReferenceItem pagingReferenceItem) : base(version)
		{
			this.pageSize = pageSize;
			this.direction = direction;
			this.referenceDocumentId = referenceDocumentId;
			this.pagingReferenceItem = pagingReferenceItem;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00009D44 File Offset: 0x00009144
		internal PagingInfo(int pageSize, PagingDirection direction, long referenceDocumentId, PagingReferenceItem pagingReferenceItem) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.pageSize = pageSize;
			this.direction = direction;
			this.referenceDocumentId = referenceDocumentId;
			this.pagingReferenceItem = pagingReferenceItem;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00009DB0 File Offset: 0x000091B0
		internal int PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00009DC4 File Offset: 0x000091C4
		internal PagingDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00009DD8 File Offset: 0x000091D8
		internal long ReferenceDocumentId
		{
			get
			{
				return this.referenceDocumentId;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00009DEC File Offset: 0x000091EC
		internal PagingReferenceItem ReferenceItem
		{
			get
			{
				return this.pagingReferenceItem;
			}
		}

		// Token: 0x04000B08 RID: 2824
		private readonly int pageSize;

		// Token: 0x04000B09 RID: 2825
		private readonly PagingDirection direction;

		// Token: 0x04000B0A RID: 2826
		private readonly long referenceDocumentId;

		// Token: 0x04000B0B RID: 2827
		private readonly PagingReferenceItem pagingReferenceItem;
	}
}
