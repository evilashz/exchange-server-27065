using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003EF RID: 1007
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParkedMeetingMessage : MessageItem
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x000BD78D File Offset: 0x000BB98D
		internal ParkedMeetingMessage(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000BD797 File Offset: 0x000BB997
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x000BD7AF File Offset: 0x000BB9AF
		public string ParkedCorrelationId
		{
			get
			{
				this.CheckDisposed("ParkedCorrelationId::get");
				return base.GetValueOrDefault<string>(ParkedMeetingMessageSchema.ParkedCorrelationId);
			}
			set
			{
				this.CheckDisposed("ParkedCorrelationId::set");
				this[ParkedMeetingMessageSchema.ParkedCorrelationId] = value;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000BD7C8 File Offset: 0x000BB9C8
		// (set) Token: 0x06002E11 RID: 11793 RVA: 0x000BD7E0 File Offset: 0x000BB9E0
		public byte[] CleanGlobalObjectId
		{
			get
			{
				this.CheckDisposed("CleanGlobalObjectId::get");
				return base.GetValueOrDefault<byte[]>(ParkedMeetingMessageSchema.CleanGlobalObjectId);
			}
			set
			{
				this.CheckDisposed("CleanGlobalObjectId::set");
				this[ParkedMeetingMessageSchema.CleanGlobalObjectId] = value;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000BD7F9 File Offset: 0x000BB9F9
		// (set) Token: 0x06002E13 RID: 11795 RVA: 0x000BD811 File Offset: 0x000BBA11
		public string OriginalMessageId
		{
			get
			{
				this.CheckDisposed("OriginalMessageId::get");
				return base.GetValueOrDefault<string>(ParkedMeetingMessageSchema.OriginalMessageId);
			}
			set
			{
				this.CheckDisposed("OriginalMessageId::set");
				this[ParkedMeetingMessageSchema.OriginalMessageId] = value;
			}
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000BD82A File Offset: 0x000BBA2A
		public new static ParkedMeetingMessage Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propertiesToReturn)
		{
			return ParkedMeetingMessage.Bind(session, storeId, (ICollection<PropertyDefinition>)propertiesToReturn);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000BD839 File Offset: 0x000BBA39
		public new static ParkedMeetingMessage Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propertiesToReturn)
		{
			return ItemBuilder.ItemBind<ParkedMeetingMessage>(session, storeId, ParkedMeetingMessageSchema.Instance, propertiesToReturn);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000BD848 File Offset: 0x000BBA48
		public static ParkedMeetingMessage Create(MailboxSession mailboxSession)
		{
			StoreObjectId storeObjectId = mailboxSession.GetDefaultFolderId(DefaultFolderType.ParkedMessages);
			if (storeObjectId == null)
			{
				storeObjectId = mailboxSession.CreateDefaultFolder(DefaultFolderType.ParkedMessages);
			}
			ParkedMeetingMessage parkedMeetingMessage = ItemBuilder.CreateNewItem<ParkedMeetingMessage>(mailboxSession, storeObjectId, ItemCreateInfo.ParkedMeetingMessageInfo);
			parkedMeetingMessage[StoreObjectSchema.ItemClass] = "IPM.Parked.MeetingMessage";
			return parkedMeetingMessage;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000BD888 File Offset: 0x000BBA88
		public static string GetCorrelationId(string seriesId, int seriesSequenceNumber)
		{
			return string.Format("{0}_{1}", seriesId, seriesSequenceNumber);
		}
	}
}
