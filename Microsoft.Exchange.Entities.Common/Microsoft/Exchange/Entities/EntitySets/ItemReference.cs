using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x0200003B RID: 59
	internal abstract class ItemReference<TEntity> : StorageEntitySetScope<IStoreSession>, IItemReference<TEntity>, IEntityReference<TEntity> where TEntity : class, IItem
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00004A33 File Offset: 0x00002C33
		protected ItemReference(IStorageEntitySetScope<IStoreSession> scope, string itemKey) : base(scope)
		{
			this.ItemKey = itemKey;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004A43 File Offset: 0x00002C43
		protected ItemReference(IStorageEntitySetScope<IStoreSession> scope, StoreId itemStoreId, IStoreSession session) : base(scope)
		{
			this.ItemKey = base.IdConverter.ToStringId(itemStoreId, session);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004A5F File Offset: 0x00002C5F
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004A67 File Offset: 0x00002C67
		public string ItemKey { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004A70 File Offset: 0x00002C70
		public IAttachments Attachments
		{
			get
			{
				return new Attachments(this, this, this.GetAttachmentDataProvider(), null);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004A80 File Offset: 0x00002C80
		public StoreId GetContainingFolderId()
		{
			if (this.containingFolderId == null)
			{
				StoreObjectId objectId = base.IdConverter.ToStoreObjectId(this.ItemKey);
				this.containingFolderId = base.StoreSession.GetParentFolderId(objectId);
			}
			return this.containingFolderId;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004ABF File Offset: 0x00002CBF
		string IEntityReference<!0>.GetKey()
		{
			return this.ItemKey;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004AC8 File Offset: 0x00002CC8
		TEntity IEntityReference<!0>.Read(CommandContext context)
		{
			return default(TEntity);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004ADE File Offset: 0x00002CDE
		protected virtual AttachmentDataProvider GetAttachmentDataProvider()
		{
			return new AttachmentDataProvider(this, base.IdConverter.ToStoreObjectId(this.ItemKey));
		}

		// Token: 0x04000055 RID: 85
		private StoreId containingFolderId;
	}
}
