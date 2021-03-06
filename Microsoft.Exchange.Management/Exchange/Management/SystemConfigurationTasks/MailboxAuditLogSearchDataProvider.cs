using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200008A RID: 138
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxAuditLogSearchDataProvider : AuditLogSearchDataProviderBase
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00012CCC File Offset: 0x00010ECC
		public MailboxAuditLogSearchDataProvider(MailboxSession mailboxSession) : base(mailboxSession, MailboxAuditLogSearchDataProvider.MailboxAuditLogSearchSetting)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00012CDA File Offset: 0x00010EDA
		internal override AuditLogSearchItemBase GetItemFromStore(VersionedId messageId)
		{
			return new MailboxAuditLogSearchItem(base.MailboxSession, messageId);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012CE8 File Offset: 0x00010EE8
		protected override void SaveObjectToStore(AuditLogSearchBase searchBase)
		{
			MailboxAuditLogSearch mailboxAuditLogSearch = (MailboxAuditLogSearch)searchBase;
			using (MailboxAuditLogSearchItem mailboxAuditLogSearchItem = new MailboxAuditLogSearchItem(base.MailboxSession, base.Folder))
			{
				Guid guid = ((AuditLogSearchId)mailboxAuditLogSearch.Identity).Guid;
				mailboxAuditLogSearchItem.Identity = guid;
				mailboxAuditLogSearchItem.Name = mailboxAuditLogSearch.Name;
				mailboxAuditLogSearchItem.StartDate = new ExDateTime(ExTimeZone.UtcTimeZone, mailboxAuditLogSearch.StartDateUtc.Value);
				mailboxAuditLogSearchItem.EndDate = new ExDateTime(ExTimeZone.UtcTimeZone, mailboxAuditLogSearch.EndDateUtc.Value);
				mailboxAuditLogSearchItem.StatusMailRecipients = mailboxAuditLogSearch.StatusMailRecipients;
				mailboxAuditLogSearchItem.CreatedBy = mailboxAuditLogSearch.CreatedBy;
				mailboxAuditLogSearchItem.CreatedByEx = mailboxAuditLogSearch.CreatedByEx;
				mailboxAuditLogSearchItem.MailboxIds = mailboxAuditLogSearch.Mailboxes;
				mailboxAuditLogSearchItem.LogonTypeStrings = mailboxAuditLogSearch.LogonTypes;
				mailboxAuditLogSearchItem.Operations = mailboxAuditLogSearch.Operations;
				mailboxAuditLogSearchItem.ShowDetails = mailboxAuditLogSearch.ShowDetails;
				mailboxAuditLogSearchItem.ExternalAccess = mailboxAuditLogSearch.ExternalAccess;
				mailboxAuditLogSearchItem.Save(SaveMode.ResolveConflicts);
				AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
				{
					QueueType = AuditQueueType.AsyncMailboxSearch,
					EventType = QueueEventType.Queue,
					CorrelationId = guid.ToString(),
					OrganizationId = base.MailboxSession.OrganizationId,
					QueueLength = base.Folder.ItemCount + 1
				};
				auditQueuesOpticsLogData.Log();
			}
		}

		// Token: 0x04000239 RID: 569
		private static readonly AuditLogSearchDataProviderBase.SearchSetting MailboxAuditLogSearchSetting = new AuditLogSearchDataProviderBase.SearchSetting
		{
			FolderName = "MailboxAuditLogSearch",
			CachedFolderIds = new Dictionary<Guid, StoreObjectId>(),
			MessageQueryFilter = new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(StoreObjectSchema.CreationTime),
				new ExistsFilter(AuditLogSearchItemSchema.Identity),
				new ExistsFilter(AuditLogSearchItemSchema.StartDate),
				new ExistsFilter(AuditLogSearchItemSchema.EndDate),
				new ExistsFilter(AuditLogSearchItemSchema.StatusMailRecipients),
				new ExistsFilter(AuditLogSearchItemSchema.CreatedBy),
				new ExistsFilter(AuditLogSearchItemSchema.CreatedByEx),
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.AuditLogSearch.Mailbox", MatchOptions.FullString, MatchFlags.IgnoreCase)
			})
		};
	}
}
