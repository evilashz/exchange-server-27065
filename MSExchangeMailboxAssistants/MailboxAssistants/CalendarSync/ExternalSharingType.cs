using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Sharing;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C4 RID: 196
	internal abstract class ExternalSharingType : SynchronizableFolderType
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00039E3A File Offset: 0x0003803A
		protected override Guid ProviderGuid
		{
			get
			{
				return SharingBindingManager.ExternalSharingProviderGuid;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00039E41 File Offset: 0x00038041
		protected override bool MatchesFolder(Folder folder)
		{
			if (!folder.IsExchangeCrossOrgShareFolder)
			{
				SynchronizableFolderType.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: ExternalSharingType.MatchesFolder: folder {1} from user {2} isn't a cross org sharing folder.", TraceContext.Get(), folder.DisplayName, ((MailboxSession)folder.Session).DisplayName);
				return false;
			}
			return true;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00039E7F File Offset: 0x0003807F
		protected override bool MatchesExtendedFolderFlags(int extendedFolderFlags)
		{
			return CalendarFolder.IsCrossOrgShareFolder(extendedFolderFlags);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00039E88 File Offset: 0x00038088
		protected override bool HasSubscriptionInternal(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			bool result;
			using (SharingSubscriptionManager sharingSubscriptionManager = new SharingSubscriptionManager(mailboxSession))
			{
				result = (sharingSubscriptionManager.GetByLocalFolderId(folderId) != null);
			}
			return result;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00039EC8 File Offset: 0x000380C8
		public override bool Synchronize(MailboxSession mailboxSession, FolderRow folderRow, Deadline deadline, CalendarSyncPerformanceCountersInstance counters, CalendarSyncFolderOperationLogEntry folderOpLogEntry)
		{
			bool result = true;
			SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ExternalSharingCalendarType.Synchronize will try to sync folder {1} with id {2} for mailbox {3}.", new object[]
			{
				TraceContext.Get(),
				folderRow.DisplayName,
				folderRow.FolderId,
				mailboxSession.DisplayName
			});
			folderOpLogEntry.FolderUrl = folderRow.DisplayName;
			try
			{
				result = SharingEngine.SyncFolder(mailboxSession, folderRow.FolderId, deadline);
			}
			catch (SharingSynchronizationException ex)
			{
				SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ExternalSharingCalendarType.Synchronize SharingEngine threw an exception for for folder {1} with id {2} for mailbox {3}: {4}", new object[]
				{
					TraceContext.Get(),
					folderRow.DisplayName,
					folderRow.FolderId,
					mailboxSession.DisplayName,
					ex
				});
				folderOpLogEntry.AddExceptionToLog(ex);
			}
			catch (OrganizationNotFederatedException ex2)
			{
				SynchronizableFolderType.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: ExternalSharingCalendarType.Synchronize SharingEngine couldn't sync folder {1} with id {2} for mailbox {3} because the org isn't federated.", new object[]
				{
					TraceContext.Get(),
					folderRow.DisplayName,
					folderRow.FolderId,
					mailboxSession.DisplayName
				});
				folderOpLogEntry.AddExceptionToLog(ex2);
			}
			return result;
		}
	}
}
