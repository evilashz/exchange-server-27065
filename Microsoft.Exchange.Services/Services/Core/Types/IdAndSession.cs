using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200077F RID: 1919
	internal class IdAndSession
	{
		// Token: 0x0600394A RID: 14666 RVA: 0x000CB036 File Offset: 0x000C9236
		internal IdAndSession()
		{
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000CB049 File Offset: 0x000C9249
		public IdAndSession(StoreId storeId, StoreSession session)
		{
			this.id = storeId;
			this.session = session;
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000CB06A File Offset: 0x000C926A
		public IdAndSession(StoreId storeId, StoreSession session, IList<AttachmentId> attachmentIds) : this(storeId, session)
		{
			this.attachmentIds.AddRange(attachmentIds);
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000CB080 File Offset: 0x000C9280
		public IdAndSession(StoreId storeId, StoreId parentFolderId, StoreSession session) : this(storeId, session)
		{
			this.parentFolderId = parentFolderId;
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000CB091 File Offset: 0x000C9291
		public IdAndSession(StoreId storeId, StoreId parentFolderId, StoreSession session, IList<AttachmentId> attachmentIds) : this(storeId, session, attachmentIds)
		{
			this.parentFolderId = parentFolderId;
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000CB0A4 File Offset: 0x000C92A4
		public static IdAndSession CreateFromItem(Item item)
		{
			IdAndSession result = null;
			if (item.Session is MailboxSession)
			{
				result = new IdAndSession(item.Id, item.Session);
			}
			else if (item.Session is PublicFolderSession)
			{
				result = new IdAndSession(item.Id, item.ParentId, item.Session);
			}
			return result;
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000CB0FA File Offset: 0x000C92FA
		public static IdAndSession CreateFromFolder(Folder folder)
		{
			return new IdAndSession(folder.Id, folder.Session);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000CB110 File Offset: 0x000C9310
		public IdAndSession Clone()
		{
			return new IdAndSession(this.Id, this.Session, this.AttachmentIds)
			{
				parentFolderId = this.parentFolderId
			};
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000CB142 File Offset: 0x000C9342
		public Item GetRootXsoItem(PropertyDefinition[] propertyDefinitions)
		{
			return ServiceCommandBase.GetXsoItem(this.Session, this.Id, propertyDefinitions);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000CB156 File Offset: 0x000C9356
		public AggregateOperationResult DeleteRootXsoItem(StoreObjectId parentStoreObjectId, DeleteItemFlags deleteItemFlags)
		{
			return this.DeleteRootXsoItem(parentStoreObjectId, deleteItemFlags, false);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000CB164 File Offset: 0x000C9364
		public AggregateOperationResult DeleteRootXsoItem(StoreObjectId parentStoreObjectId, DeleteItemFlags deleteItemFlags, bool returnNewItemIds)
		{
			MailboxSession mailboxSession = this.Session as MailboxSession;
			if (mailboxSession != null && mailboxSession.IsDefaultFolderType(parentStoreObjectId) == DefaultFolderType.JunkEmail)
			{
				deleteItemFlags |= DeleteItemFlags.SuppressReadReceipt;
			}
			CallContext callContext = CallContext.Current;
			if (callContext != null && (callContext.WorkloadType == WorkloadType.Owa || callContext.WorkloadType == WorkloadType.OwaVoice) && mailboxSession != null && mailboxSession.LogonType == LogonType.Delegated && (deleteItemFlags & DeleteItemFlags.MoveToDeletedItems) == DeleteItemFlags.MoveToDeletedItems && this.GetAsStoreObjectId().ObjectType == StoreObjectType.CalendarItem)
			{
				MailboxSession mailboxIdentityMailboxSession = CallContext.Current.SessionCache.GetMailboxIdentityMailboxSession();
				StoreObjectId defaultFolderId = mailboxIdentityMailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
				return mailboxSession.Move(mailboxIdentityMailboxSession, defaultFolderId, new DeleteItemFlags?(deleteItemFlags), new StoreId[]
				{
					this.Id
				});
			}
			return this.Session.Delete(deleteItemFlags, returnNewItemIds, new StoreId[]
			{
				this.Id
			});
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000CB22D File Offset: 0x000C942D
		public StoreId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x000CB235 File Offset: 0x000C9435
		public StoreId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000CB23D File Offset: 0x000C943D
		public StoreSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06003958 RID: 14680 RVA: 0x000CB245 File Offset: 0x000C9445
		public List<AttachmentId> AttachmentIds
		{
			get
			{
				return this.attachmentIds;
			}
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000CB24D File Offset: 0x000C944D
		public ConcatenatedIdAndChangeKey GetConcatenatedId()
		{
			return IdConverter.GetConcatenatedId(this.Id, this, this.AttachmentIds);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000CB261 File Offset: 0x000C9461
		public StoreObjectId GetAsStoreObjectId()
		{
			return IdConverter.GetAsStoreObjectId(this.id);
		}

		// Token: 0x04001FF1 RID: 8177
		private StoreId id;

		// Token: 0x04001FF2 RID: 8178
		private StoreId parentFolderId;

		// Token: 0x04001FF3 RID: 8179
		private StoreSession session;

		// Token: 0x04001FF4 RID: 8180
		private List<AttachmentId> attachmentIds = new List<AttachmentId>();
	}
}
