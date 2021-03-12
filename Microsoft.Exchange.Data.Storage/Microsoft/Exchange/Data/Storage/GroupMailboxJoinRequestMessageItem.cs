using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200086A RID: 2154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupMailboxJoinRequestMessageItem : MessageItem
	{
		// Token: 0x06005116 RID: 20758 RVA: 0x00151B73 File Offset: 0x0014FD73
		internal GroupMailboxJoinRequestMessageItem(ICoreItem coreItem) : base(coreItem, false)
		{
			if (base.IsNew)
			{
				this[InternalSchema.ItemClass] = "IPM.GroupMailbox.JoinRequest";
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x00151B95 File Offset: 0x0014FD95
		// (set) Token: 0x06005118 RID: 20760 RVA: 0x00151BAD File Offset: 0x0014FDAD
		public string GroupSmtpAddress
		{
			get
			{
				this.CheckDisposed("GroupSmtpAddress::get");
				return base.GetValueOrDefault<string>(GroupMailboxJoinRequestMessageSchema.GroupSmtpAddress);
			}
			set
			{
				this.CheckDisposed("GroupSmtpAddress::set");
				this[GroupMailboxJoinRequestMessageSchema.GroupSmtpAddress] = value;
			}
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00151BC6 File Offset: 0x0014FDC6
		public new static GroupMailboxJoinRequestMessageItem Bind(StoreSession session, StoreId messageId)
		{
			return GroupMailboxJoinRequestMessageItem.Bind(session, messageId, null);
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x00151BD0 File Offset: 0x0014FDD0
		public static GroupMailboxJoinRequestMessageItem Create(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			return ItemBuilder.CreateNewItem<GroupMailboxJoinRequestMessageItem>(mailboxSession, folderId, ItemCreateInfo.GroupMailboxJoinRequestMessageInfo);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00151BE0 File Offset: 0x0014FDE0
		public new static GroupMailboxJoinRequestMessageItem Bind(StoreSession session, StoreId messageId, ICollection<PropertyDefinition> propsToReturn)
		{
			if (!(session is MailboxSession))
			{
				throw new ArgumentException("session");
			}
			return ItemBuilder.ItemBind<GroupMailboxJoinRequestMessageItem>(session, messageId, GroupMailboxJoinRequestMessageSchema.Instance, propsToReturn);
		}
	}
}
