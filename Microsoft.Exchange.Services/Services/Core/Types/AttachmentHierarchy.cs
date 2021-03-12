using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006BB RID: 1723
	internal sealed class AttachmentHierarchy : IDisposable
	{
		// Token: 0x06003503 RID: 13571 RVA: 0x000BF3E4 File Offset: 0x000BD5E4
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				for (int i = this.Count - 1; i >= 0; i--)
				{
					this.items[i].Dispose();
				}
				if (!this.rootItemFromExternal && this.rootItem != null)
				{
					this.rootItem.Dispose();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000BF43F File Offset: 0x000BD63F
		private static Attachment FindAttachmentInAttachmentCollection(AttachmentCollection attachments, AttachmentId attachmentId)
		{
			if (attachments.Contains(attachmentId))
			{
				return attachments.Open(attachmentId);
			}
			throw new InvalidStoreIdException(CoreResources.IDs.ErrorInvalidAttachmentId);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000BF461 File Offset: 0x000BD661
		public AttachmentHierarchy(Item rootItem)
		{
			this.rootItem = rootItem;
			this.rootItemFromExternal = true;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000BF484 File Offset: 0x000BD684
		public AttachmentHierarchy(IdAndSession idAndSession, bool openAsReadWrite, bool clientSupportsIrm)
		{
			bool flag = false;
			try
			{
				bool flag2 = IrmUtils.IsIrmEnabled(clientSupportsIrm, idAndSession.Session);
				this.rootItem = idAndSession.GetRootXsoItem(null);
				StoreSession session = idAndSession.Session;
				if (openAsReadWrite)
				{
					this.rootItem.OpenAsReadWrite();
				}
				Item xsoItem = this.rootItem;
				if (flag2)
				{
					RightsManagedMessageItem rightsManagedMessageItem = xsoItem as RightsManagedMessageItem;
					if (rightsManagedMessageItem != null)
					{
						IrmUtils.DecryptForGetAttachment(session, rightsManagedMessageItem);
					}
				}
				for (int i = 0; i < idAndSession.AttachmentIds.Count; i++)
				{
					AttachmentCollection effectiveAttachmentCollection = Util.GetEffectiveAttachmentCollection(xsoItem, false);
					Attachment attachment = AttachmentHierarchy.FindAttachmentInAttachmentCollection(effectiveAttachmentCollection, idAndSession.AttachmentIds[i]);
					AttachmentHierarchyItem attachmentHierarchyItem = new AttachmentHierarchyItem(attachment, effectiveAttachmentCollection);
					this.items.Add(attachmentHierarchyItem);
					if (i < idAndSession.AttachmentIds.Count - 1)
					{
						if (!(attachment is ItemAttachment))
						{
							throw new InvalidStoreIdException(CoreResources.IDs.ErrorInvalidAttachmentId);
						}
						xsoItem = attachmentHierarchyItem.XsoItem;
					}
					if (flag2)
					{
						RightsManagedMessageItem rightsManagedMessageItem = xsoItem as RightsManagedMessageItem;
						if (rightsManagedMessageItem != null)
						{
							IrmUtils.DecryptForGetAttachment(session, rightsManagedMessageItem);
						}
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000BF5B4 File Offset: 0x000BD7B4
		public void DeleteLast()
		{
			AttachmentHierarchyItem attachmentHierarchyItem = this[this.Count - 1];
			attachmentHierarchyItem.Delete();
			this.items.Remove(attachmentHierarchyItem);
			attachmentHierarchyItem.Dispose();
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000BF5EC File Offset: 0x000BD7EC
		public void SaveAll()
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.items[i].Save();
				this.items[i].Dispose();
			}
			this.rootItem.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000BF63C File Offset: 0x000BD83C
		public Item LastAsXsoItem
		{
			get
			{
				if (this.Count == 0)
				{
					return this.rootItem;
				}
				Item xsoItem = this[this.Count - 1].XsoItem;
				RightsManagedMessageItem rightsManagedMessageItem = xsoItem as RightsManagedMessageItem;
				if (rightsManagedMessageItem != null)
				{
					IrmUtils.DecryptForGetAttachment(this.rootItem.Session, rightsManagedMessageItem);
					return rightsManagedMessageItem;
				}
				return xsoItem;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000BF68A File Offset: 0x000BD88A
		public AttachmentHierarchyItem Last
		{
			get
			{
				if (this.Count > 0)
				{
					return this[this.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x17000C30 RID: 3120
		public AttachmentHierarchyItem this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000BF6B3 File Offset: 0x000BD8B3
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000BF6C0 File Offset: 0x000BD8C0
		public Item RootItem
		{
			get
			{
				return this.rootItem;
			}
		}

		// Token: 0x04001DC3 RID: 7619
		private readonly bool rootItemFromExternal;

		// Token: 0x04001DC4 RID: 7620
		private bool isDisposed;

		// Token: 0x04001DC5 RID: 7621
		private Item rootItem;

		// Token: 0x04001DC6 RID: 7622
		private List<AttachmentHierarchyItem> items = new List<AttachmentHierarchyItem>();
	}
}
