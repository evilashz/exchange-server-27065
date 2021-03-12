using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E2A RID: 3626
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncOperation : IReadOnlyPropertyBag
	{
		// Token: 0x06007D73 RID: 32115 RVA: 0x00229A0D File Offset: 0x00227C0D
		internal SyncOperation()
		{
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x06007D74 RID: 32116 RVA: 0x00229A15 File Offset: 0x00227C15
		// (set) Token: 0x06007D75 RID: 32117 RVA: 0x00229A36 File Offset: 0x00227C36
		public int?[] ChangeTrackingInformation
		{
			get
			{
				if (this.manifestEntry.IsDelayedServerOperation)
				{
					return this.WholeItemChangedChangeTrackingInformation;
				}
				return this.manifestEntry.ChangeTrackingInformation;
			}
			set
			{
				this.manifestEntry.ChangeTrackingInformation = value;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x06007D76 RID: 32118 RVA: 0x00229A44 File Offset: 0x00227C44
		public ChangeType ChangeType
		{
			get
			{
				return this.manifestEntry.ChangeType;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06007D77 RID: 32119 RVA: 0x00229A51 File Offset: 0x00227C51
		public bool IsRead
		{
			get
			{
				return this.manifestEntry.IsRead;
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06007D78 RID: 32120 RVA: 0x00229A5E File Offset: 0x00227C5E
		public ISyncItemId Id
		{
			get
			{
				return this.manifestEntry.Id;
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06007D79 RID: 32121 RVA: 0x00229A6B File Offset: 0x00227C6B
		public string MessageClass
		{
			get
			{
				return this.manifestEntry.MessageClass;
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06007D7A RID: 32122 RVA: 0x00229A78 File Offset: 0x00227C78
		public ConversationId ConversationId
		{
			get
			{
				return this.manifestEntry.ConversationId;
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06007D7B RID: 32123 RVA: 0x00229A85 File Offset: 0x00227C85
		public bool FirstMessageInConversation
		{
			get
			{
				return this.manifestEntry.FirstMessageInConversation;
			}
		}

		// Token: 0x1700218D RID: 8589
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.manifestEntry[propertyDefinition];
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06007D7D RID: 32125 RVA: 0x00229AA0 File Offset: 0x00227CA0
		private int?[] WholeItemChangedChangeTrackingInformation
		{
			get
			{
				if (this.wholeItemChangedChangeTrackingInformation == null && this.manifestEntry.ChangeTrackingInformation != null)
				{
					this.wholeItemChangedChangeTrackingInformation = new int?[this.manifestEntry.ChangeTrackingInformation.Length];
					for (int i = 0; i < this.manifestEntry.ChangeTrackingInformation.Length; i++)
					{
						this.wholeItemChangedChangeTrackingInformation[i] = (this.manifestEntry.ChangeTrackingInformation[i] ^ 1);
					}
				}
				return this.wholeItemChangedChangeTrackingInformation;
			}
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x00229B48 File Offset: 0x00227D48
		public void EnsureServerManifestWatermark()
		{
			if (this.manifestEntry.Watermark == null && this.manifestEntry.ChangeType != ChangeType.Delete)
			{
				ISyncItem syncItem = this.item;
				if (syncItem == null)
				{
					syncItem = this.folderSync.GetItem(this.manifestEntry.Id, new PropertyDefinition[0]);
				}
				this.manifestEntry.Watermark = syncItem.Watermark;
				if (this.cacheItem)
				{
					this.item = syncItem;
				}
			}
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x00229BB8 File Offset: 0x00227DB8
		public ISyncItem GetItem(params PropertyDefinition[] prefetchProperties)
		{
			if (this.rejected)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotOpenRejectedItem);
			}
			if (this.item != null)
			{
				return this.item;
			}
			ISyncItem result = this.folderSync.GetItem(this.manifestEntry, prefetchProperties);
			if (this.cacheItem)
			{
				this.item = result;
			}
			return result;
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x00229C0F File Offset: 0x00227E0F
		public void Reject()
		{
			if (this.rejected)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotRejectSameOperationTwice);
			}
			this.rejected = true;
			this.folderSync.RejectServerOperation(this.manifestEntry);
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x00229C41 File Offset: 0x00227E41
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			return this.manifestEntry.GetProperties(propertyDefinitions);
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x00229C4F File Offset: 0x00227E4F
		internal void Bind(ISyncItem item, ServerManifestEntry manifestEntry)
		{
			this.cacheItem = true;
			this.manifestEntry = manifestEntry;
			this.item = item;
			this.folderSync = null;
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x00229C6D File Offset: 0x00227E6D
		internal void Bind(FolderSync folderSync, ServerManifestEntry manifestEntry, bool cacheItem)
		{
			this.cacheItem = cacheItem;
			this.manifestEntry = manifestEntry;
			this.item = null;
			this.folderSync = folderSync;
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x00229C8B File Offset: 0x00227E8B
		internal void DisposeCachedItem()
		{
			if (this.item != null)
			{
				this.item.Dispose();
				this.item = null;
			}
		}

		// Token: 0x0400558B RID: 21899
		private bool cacheItem;

		// Token: 0x0400558C RID: 21900
		private FolderSync folderSync;

		// Token: 0x0400558D RID: 21901
		private ISyncItem item;

		// Token: 0x0400558E RID: 21902
		private ServerManifestEntry manifestEntry;

		// Token: 0x0400558F RID: 21903
		private bool rejected;

		// Token: 0x04005590 RID: 21904
		private int?[] wholeItemChangedChangeTrackingInformation;
	}
}
