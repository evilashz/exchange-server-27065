using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.ConsumerSharing;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C0 RID: 192
	internal class ConsumerSharingCalendarType : SynchronizableFolderType
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00039AB4 File Offset: 0x00037CB4
		protected override Guid ProviderGuid
		{
			get
			{
				return SharingProvider.SharingProviderConsumer.Guid;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00039AC0 File Offset: 0x00037CC0
		public override string FolderTypeName
		{
			get
			{
				return "ConsumerSharingCalendar";
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00039AC7 File Offset: 0x00037CC7
		public override PropertyDefinition CounterProperty
		{
			get
			{
				return MailboxSchema.ConsumerSharingCalendarSubscriptionCount;
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00039ACE File Offset: 0x00037CCE
		protected override bool MatchesContainerClass(string containerClass)
		{
			return ObjectClass.IsCalendarFolder(containerClass);
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00039AD6 File Offset: 0x00037CD6
		public override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.CalendarFolder;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00039AD9 File Offset: 0x00037CD9
		protected override bool MatchesFolder(Folder folder)
		{
			if (!folder.IsExchangeConsumerShareFolder)
			{
				SynchronizableFolderType.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: ConsumerSharingType.MatchesFolder: folder {1} from user {2} isn't a consumer sharing folder.", TraceContext.Get(), folder.DisplayName, ((MailboxSession)folder.Session).DisplayName);
				return false;
			}
			return true;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00039B17 File Offset: 0x00037D17
		protected override bool MatchesExtendedFolderFlags(int extendedFolderFlags)
		{
			return CalendarFolder.IsConsumerShareFolder(extendedFolderFlags);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00039B20 File Offset: 0x00037D20
		protected override bool HasSubscriptionInternal(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			ConsumerCalendarSynchronizer consumerCalendarSynchronizer = new ConsumerCalendarSynchronizer(mailboxSession, XSOFactory.Default, SynchronizableFolderType.Tracer);
			return consumerCalendarSynchronizer.TryGetSubscription(folderId) != null;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00039B68 File Offset: 0x00037D68
		public override bool Synchronize(MailboxSession mailboxSession, FolderRow folderRow, Deadline deadline, CalendarSyncPerformanceCountersInstance counters, CalendarSyncFolderOperationLogEntry folderOpLogEntry)
		{
			ConsumerCalendarSynchronizer consumerCalendarSynchronizer = new ConsumerCalendarSynchronizer(mailboxSession, XSOFactory.Default, SynchronizableFolderType.Tracer);
			consumerCalendarSynchronizer.LogError += delegate(object s, string e)
			{
				folderOpLogEntry.AddErrorToLog(e, string.Empty);
			};
			bool result;
			try
			{
				switch (consumerCalendarSynchronizer.Synchronize(folderRow.FolderId, deadline))
				{
				case SyncResult.DeadlineReached:
					return false;
				}
				result = true;
			}
			catch (LocalizedException ex)
			{
				folderOpLogEntry.AddExceptionToLog(ex);
				result = true;
			}
			return result;
		}
	}
}
