using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.AttachmentCommands;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Attachments : StorageEntitySet<Attachments, IAttachment, IStoreSession>, IAttachments, IEntitySet<IAttachment>
	{
		// Token: 0x06000105 RID: 261 RVA: 0x000045CE File Offset: 0x000027CE
		protected internal Attachments(IStorageEntitySetScope<IStoreSession> parentScope, IEntityReference<IItem> parentItem, AttachmentDataProvider attachmentDataProvider, IEntityCommandFactory<Attachments, IAttachment> commandFactory = null) : base(parentScope, "Attachments", commandFactory ?? EntityCommandFactory<Attachments, IAttachment, CreateAttachment, DeleteAttachment, FindAttachments, ReadAttachment, UpdateAttachment>.Instance)
		{
			this.ParentItem = parentItem;
			this.AttachmentDataProvider = attachmentDataProvider;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000045F5 File Offset: 0x000027F5
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000045FD File Offset: 0x000027FD
		public IEntityReference<IItem> ParentItem { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004606 File Offset: 0x00002806
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000460E File Offset: 0x0000280E
		public AttachmentDataProvider AttachmentDataProvider { get; private set; }
	}
}
