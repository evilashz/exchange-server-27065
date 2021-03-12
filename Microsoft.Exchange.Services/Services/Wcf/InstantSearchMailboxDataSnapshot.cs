using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009D2 RID: 2514
	internal class InstantSearchMailboxDataSnapshot
	{
		// Token: 0x06004729 RID: 18217 RVA: 0x000FE324 File Offset: 0x000FC524
		public InstantSearchMailboxDataSnapshot(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
			bool flag = false;
			try
			{
				MailboxSession obj;
				Monitor.Enter(obj = this.mailboxSession, ref flag);
				ExTimeZone exTimeZone = TimeZoneHelper.GetUserTimeZone(this.mailboxSession);
				if (exTimeZone == null)
				{
					exTimeZone = ExTimeZone.UtcTimeZone;
				}
				this.TimeZone = exTimeZone;
				this.MailboxGuid = this.mailboxSession.MailboxGuid;
				this.RootFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
				this.SentItemsFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
				StoreObjectId auditLogsFolderId = null;
				StoreObjectId adminAuditLogsFolderId = null;
				this.mailboxSession.BypassAuditsFolderAccessChecking(delegate
				{
					auditLogsFolderId = this.mailboxSession.GetAuditsFolderId();
					adminAuditLogsFolderId = this.mailboxSession.GetAdminAuditLogsFolderId();
				});
				this.folderExclusionList = new StoreObjectId[]
				{
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.JunkEmail),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecipientCache),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.CalendarLogging),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDiscoveryHolds),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsPurges),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsVersions),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.UserActivity),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsMigratedMessages),
					this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Clutter),
					auditLogsFolderId,
					adminAuditLogsFolderId
				};
			}
			finally
			{
				if (flag)
				{
					MailboxSession obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x000FE4F0 File Offset: 0x000FC6F0
		internal ComparisonFilter[] ExcludedFoldersQueryFragment
		{
			get
			{
				List<ComparisonFilter> list = new List<ComparisonFilter>(this.folderExclusionList.Length);
				foreach (StoreObjectId storeObjectId in this.folderExclusionList)
				{
					if (storeObjectId != null)
					{
						list.Add(new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.ParentItemId, storeObjectId));
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x040028D5 RID: 10453
		internal readonly Guid MailboxGuid;

		// Token: 0x040028D6 RID: 10454
		internal readonly ExTimeZone TimeZone;

		// Token: 0x040028D7 RID: 10455
		internal readonly StoreObjectId RootFolderId;

		// Token: 0x040028D8 RID: 10456
		internal readonly StoreObjectId SentItemsFolderId;

		// Token: 0x040028D9 RID: 10457
		internal readonly StoreObjectId[] folderExclusionList;

		// Token: 0x040028DA RID: 10458
		internal readonly MailboxSession mailboxSession;
	}
}
