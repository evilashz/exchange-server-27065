using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AdminAuditLogSearchDataProvider : AuditLogSearchDataProviderBase
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00012F0D File Offset: 0x0001110D
		public AdminAuditLogSearchDataProvider(MailboxSession mailboxSession) : base(mailboxSession, AdminAuditLogSearchDataProvider.AdminAuditLogSearchSetting)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012F1B File Offset: 0x0001111B
		internal override AuditLogSearchItemBase GetItemFromStore(VersionedId messageId)
		{
			return new AdminAuditLogSearchItem(base.MailboxSession, messageId);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00012F2C File Offset: 0x0001112C
		protected override void SaveObjectToStore(AuditLogSearchBase searchBase)
		{
			AdminAuditLogSearch adminAuditLogSearch = (AdminAuditLogSearch)searchBase;
			using (AdminAuditLogSearchItem adminAuditLogSearchItem = new AdminAuditLogSearchItem(base.MailboxSession, base.Folder))
			{
				Guid guid = ((AuditLogSearchId)adminAuditLogSearch.Identity).Guid;
				adminAuditLogSearchItem.Identity = guid;
				adminAuditLogSearchItem.Name = adminAuditLogSearch.Name;
				adminAuditLogSearchItem.StartDate = new ExDateTime(ExTimeZone.UtcTimeZone, adminAuditLogSearch.StartDateUtc.Value);
				adminAuditLogSearchItem.EndDate = new ExDateTime(ExTimeZone.UtcTimeZone, adminAuditLogSearch.EndDateUtc.Value);
				adminAuditLogSearchItem.StatusMailRecipients = adminAuditLogSearch.StatusMailRecipients;
				adminAuditLogSearchItem.CreatedBy = adminAuditLogSearch.CreatedBy;
				adminAuditLogSearchItem.CreatedByEx = adminAuditLogSearch.CreatedByEx;
				adminAuditLogSearchItem.Cmdlets = adminAuditLogSearch.Cmdlets;
				adminAuditLogSearchItem.Parameters = adminAuditLogSearch.Parameters;
				adminAuditLogSearchItem.ObjectIds = adminAuditLogSearch.ObjectIds;
				adminAuditLogSearchItem.ExternalAccess = adminAuditLogSearch.ExternalAccess;
				adminAuditLogSearchItem.RawUserIds = adminAuditLogSearch.UserIds;
				adminAuditLogSearchItem.ResolvedUsers = adminAuditLogSearch.ResolvedUsers;
				adminAuditLogSearchItem.RedactDatacenterAdmins = adminAuditLogSearch.RedactDatacenterAdmins;
				adminAuditLogSearchItem.Save(SaveMode.ResolveConflicts);
				AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
				{
					QueueType = AuditQueueType.AsyncAdminSearch,
					EventType = QueueEventType.Queue,
					CorrelationId = guid.ToString(),
					OrganizationId = base.MailboxSession.OrganizationId,
					QueueLength = base.Folder.ItemCount + 1
				};
				auditQueuesOpticsLogData.Log();
			}
		}

		// Token: 0x0400023A RID: 570
		private static readonly AuditLogSearchDataProviderBase.SearchSetting AdminAuditLogSearchSetting = new AuditLogSearchDataProviderBase.SearchSetting
		{
			FolderName = "AdminAuditLogSearch",
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
				new TextFilter(StoreObjectSchema.ItemClass, "IPM.AuditLogSearch.Admin", MatchOptions.FullString, MatchFlags.IgnoreCase)
			})
		};
	}
}
