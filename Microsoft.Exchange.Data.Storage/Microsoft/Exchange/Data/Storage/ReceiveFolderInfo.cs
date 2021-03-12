using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ReceiveFolderInfo
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000136E0 File Offset: 0x000118E0
		public ReceiveFolderInfo(byte[] entryId, string messageClass, ExDateTime lastModification)
		{
			this = default(ReceiveFolderInfo);
			this.FolderId = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Folder);
			this.MessageClass = messageClass;
			this.LastModification = lastModification;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00013704 File Offset: 0x00011904
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0001370C File Offset: 0x0001190C
		public StoreObjectId FolderId { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00013715 File Offset: 0x00011915
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0001371D File Offset: 0x0001191D
		public string MessageClass { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00013726 File Offset: 0x00011926
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0001372E File Offset: 0x0001192E
		public ExDateTime LastModification { get; private set; }
	}
}
