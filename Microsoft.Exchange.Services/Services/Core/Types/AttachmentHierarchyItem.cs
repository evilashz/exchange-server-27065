using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006BC RID: 1724
	internal sealed class AttachmentHierarchyItem : IDisposable
	{
		// Token: 0x0600350E RID: 13582 RVA: 0x000BF6C8 File Offset: 0x000BD8C8
		public AttachmentHierarchyItem(Attachment attachment, AttachmentCollection ownerCollection)
		{
			this.attachment = attachment;
			this.ownerCollection = ownerCollection;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000BF6DE File Offset: 0x000BD8DE
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				if (this.xsoItem != null)
				{
					this.xsoItem.Dispose();
				}
				if (this.attachment != null)
				{
					this.attachment.Dispose();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000BF718 File Offset: 0x000BD918
		public void Delete()
		{
			if (!this.isDeleted)
			{
				try
				{
					if (this.ownerCollection.IsReadOnly)
					{
						throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorDeleteItemsFailed);
					}
					this.ownerCollection.Remove(this.Attachment.Id);
					this.isDeleted = true;
				}
				finally
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000BF784 File Offset: 0x000BD984
		public void Save()
		{
			this.XsoItem.Save(SaveMode.NoConflictResolution);
			this.Attachment.Save();
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000BF79E File Offset: 0x000BD99E
		public Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000BF7A6 File Offset: 0x000BD9A6
		public bool IsItemAttachment
		{
			get
			{
				return this.Attachment is ItemAttachment;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000BF7B8 File Offset: 0x000BD9B8
		public Item XsoItem
		{
			get
			{
				if (this.xsoItem == null)
				{
					ItemAttachment itemAttachment = this.Attachment as ItemAttachment;
					if (itemAttachment == null)
					{
						throw new InvalidStoreIdException(CoreResources.IDs.ErrorInvalidIdNotAnItemAttachmentId);
					}
					this.xsoItem = itemAttachment.GetItem();
				}
				return this.xsoItem;
			}
		}

		// Token: 0x04001DC7 RID: 7623
		private bool isDeleted;

		// Token: 0x04001DC8 RID: 7624
		private bool isDisposed;

		// Token: 0x04001DC9 RID: 7625
		private Attachment attachment;

		// Token: 0x04001DCA RID: 7626
		private AttachmentCollection ownerCollection;

		// Token: 0x04001DCB RID: 7627
		private Item xsoItem;
	}
}
