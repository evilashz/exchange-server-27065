using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000974 RID: 2420
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedItemProps
	{
		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x060059A2 RID: 22946 RVA: 0x00172D6C File Offset: 0x00170F6C
		// (set) Token: 0x060059A3 RID: 22947 RVA: 0x00172D74 File Offset: 0x00170F74
		public StoreObjectId EntryId { get; private set; }

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x060059A4 RID: 22948 RVA: 0x00172D7D File Offset: 0x00170F7D
		// (set) Token: 0x060059A5 RID: 22949 RVA: 0x00172D85 File Offset: 0x00170F85
		public StoreObjectId ParentId { get; private set; }

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x00172D8E File Offset: 0x00170F8E
		// (set) Token: 0x060059A7 RID: 22951 RVA: 0x00172D96 File Offset: 0x00170F96
		public Uri LinkedUri { get; private set; }

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x00172D9F File Offset: 0x00170F9F
		// (set) Token: 0x060059A9 RID: 22953 RVA: 0x00172DA7 File Offset: 0x00170FA7
		public ExDateTime? LastFullSyncTime { get; private set; }

		// Token: 0x060059AA RID: 22954 RVA: 0x00172DB0 File Offset: 0x00170FB0
		public LinkedItemProps(StoreObjectId entryId, StoreObjectId parentId)
		{
			if (entryId == null)
			{
				throw new ArgumentNullException("entryId");
			}
			if (parentId == null)
			{
				throw new ArgumentNullException("parentId");
			}
			this.EntryId = entryId;
			this.ParentId = parentId;
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x00172DE2 File Offset: 0x00170FE2
		public LinkedItemProps(StoreObjectId entryId, StoreObjectId parentId, Uri linkedUri) : this(entryId, parentId)
		{
			this.LinkedUri = linkedUri;
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x00172DF3 File Offset: 0x00170FF3
		public LinkedItemProps(StoreObjectId entryId, StoreObjectId parentId, Uri linkedUri, ExDateTime? lastFullSyncTime) : this(entryId, parentId, linkedUri)
		{
			this.LastFullSyncTime = lastFullSyncTime;
		}
	}
}
