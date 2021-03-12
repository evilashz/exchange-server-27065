using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000200 RID: 512
	internal sealed class MailboxData
	{
		// Token: 0x060013A8 RID: 5032 RVA: 0x00072FDE File Offset: 0x000711DE
		public MailboxData(Guid mailboxGuid)
		{
			this.mailboxGuid = mailboxGuid;
			this.InboxUnreadCount = -1L;
			this.InboxItemCount = -1L;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00072FFD File Offset: 0x000711FD
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00073005 File Offset: 0x00071205
		public byte[] InboxFolderId
		{
			get
			{
				return this.inboxFolderId;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0007300D File Offset: 0x0007120D
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x00073015 File Offset: 0x00071215
		public long InboxUnreadCount { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0007301E File Offset: 0x0007121E
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00073026 File Offset: 0x00071226
		public long InboxItemCount { get; set; }

		// Token: 0x060013AF RID: 5039 RVA: 0x00073030 File Offset: 0x00071230
		public bool LoadData(IMailboxSession itemStore, IXSOFactory factory)
		{
			if (this.inboxFolderId == null)
			{
				StoreObjectId defaultFolderId = itemStore.GetDefaultFolderId(DefaultFolderType.Inbox);
				if (defaultFolderId != null)
				{
					this.inboxFolderId = defaultFolderId.ProviderLevelItemId;
					using (IFolder folder = factory.BindToFolder(itemStore, defaultFolderId))
					{
						this.InboxItemCount = (long)folder.ItemCount;
						this.InboxUnreadCount = (long)folder.GetValueOrDefault<int>(FolderSchema.UnreadCount, 1);
						if (this.InboxUnreadCount == 0L)
						{
							this.InboxUnreadCount = 1L;
							ExTraceGlobals.PushNotificationAssistantTracer.TraceError<Guid, object>((long)this.GetHashCode(), "MailboxData.LoadData('{0}'): {1} - The UnreadCount coming from the folder is = 0", itemStore.MailboxGuid, TraceContext.Get());
						}
						return true;
					}
				}
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToResolveInboxFolderId, itemStore.MdbGuid.ToString(), new object[]
				{
					itemStore.MdbGuid,
					itemStore.MailboxGuid
				});
				ExTraceGlobals.PushNotificationAssistantTracer.TraceWarning((long)this.GetHashCode(), "MailboxData.LoadData: {0} - Load Data: Inbox folder is null", new object[]
				{
					TraceContext.Get()
				});
				return false;
			}
			return true;
		}

		// Token: 0x04000C01 RID: 3073
		private readonly Guid mailboxGuid;

		// Token: 0x04000C02 RID: 3074
		private byte[] inboxFolderId;
	}
}
