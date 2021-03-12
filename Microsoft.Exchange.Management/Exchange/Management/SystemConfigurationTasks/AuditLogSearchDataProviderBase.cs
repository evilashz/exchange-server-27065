using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000088 RID: 136
	internal class AuditLogSearchDataProviderBase : XsoMailboxDataProviderBase
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x00011EF1 File Offset: 0x000100F1
		protected AuditLogSearchDataProviderBase(MailboxSession mailboxSession, AuditLogSearchDataProviderBase.SearchSetting setting) : base(mailboxSession)
		{
			this.setting = setting;
			this.folder = this.GetSearchRequestFolder();
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00011F0D File Offset: 0x0001010D
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00011F18 File Offset: 0x00010118
		internal static MailboxSession GetMailboxSession(OrganizationId organizationId, string action)
		{
			ADSessionSettings sessionSettings = organizationId.ToADSessionSettings();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, sessionSettings, 99, "GetMailboxSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AuditLogSearch\\AuditLogSearchDataProvider.cs");
			ADUser discoveryMailbox = MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession);
			return AuditLogSearchDataProviderBase.GetMailboxSession(discoveryMailbox, action);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00011F54 File Offset: 0x00010154
		internal static MailboxSession GetMailboxSession(ADUser mailbox, string action)
		{
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(mailbox, RemotingOptions.AllowCrossSite);
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=" + action);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00011F80 File Offset: 0x00010180
		private Folder GetSearchRequestFolder()
		{
			StoreObjectId defaultFolderId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			string folderName = this.setting.FolderName;
			Folder folder = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				int num = 0;
				while (folder == null && num <= 1)
				{
					StoreObjectId storeObjectId;
					lock (this.setting.LockObj)
					{
						if (!this.setting.CachedFolderIds.TryGetValue(base.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, out storeObjectId))
						{
							storeObjectId = this.GetFolderId(base.MailboxSession, defaultFolderId, folderName);
							if (storeObjectId == null)
							{
								folder = Folder.Create(base.MailboxSession, defaultFolderId, StoreObjectType.Folder, folderName, CreateMode.OpenIfExists);
								disposeGuard.Add<Folder>(folder);
								folder.Save();
								folder.Load(AuditLogSearchDataProviderBase.FolderProperties);
								storeObjectId = folder.Id.ObjectId;
							}
							this.setting.CachedFolderIds[base.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid] = storeObjectId;
						}
					}
					if (folder == null)
					{
						try
						{
							folder = Folder.Bind(base.MailboxSession, storeObjectId);
							disposeGuard.Add<Folder>(folder);
						}
						catch (ObjectNotFoundException)
						{
							lock (this.setting.LockObj)
							{
								this.setting.CachedFolderIds.Remove(base.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid);
							}
							bool flag3 = num >= 1;
							if (flag3)
							{
								throw;
							}
						}
					}
					num++;
				}
				disposeGuard.Success();
			}
			return folder;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00012184 File Offset: 0x00010384
		private StoreObjectId GetFolderId(MailboxSession mailboxSession, StoreObjectId rootFolderId, string folderName)
		{
			using (Folder folder = Folder.Bind(mailboxSession, rootFolderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id,
					StoreObjectSchema.DisplayName
				}))
				{
					QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.DisplayName, folderName);
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
					{
						object[][] rows = queryResult.GetRows(1);
						if (rows.Length > 0)
						{
							VersionedId versionedId = (VersionedId)rows[0][0];
							return versionedId.ObjectId;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001261C File Offset: 0x0001081C
		internal IEnumerable<VersionedId> FindMessageIds(ObjectId rootId, SortBy sortBy, bool latest)
		{
			SortBy[] sortColumns = (sortBy == null) ? null : new SortBy[]
			{
				sortBy
			};
			ExDateTime momentsAgo = new ExDateTime(ExTimeZone.UtcTimeZone, DateTime.UtcNow.Add(AuditLogSearchDataProviderBase.DelayPeriod));
			QueryFilter queryFilter = this.setting.MessageQueryFilter;
			using (QueryResult queryResult = this.Folder.ItemQuery(ItemQueryType.None, queryFilter, sortColumns, AuditLogSearchDataProviderBase.MessageProperties))
			{
				AuditLogSearchId requestId = rootId as AuditLogSearchId;
				if (requestId != null)
				{
					QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, AuditLogSearchItemSchema.Identity, requestId.Guid);
					SeekReference seekReference = SeekReference.OriginBeginning;
					while (queryResult.SeekToCondition(seekReference, seekFilter))
					{
						seekReference = SeekReference.OriginCurrent;
						foreach (VersionedId messageId in this.ReadMessageIdsFromQueryResult(queryResult, momentsAgo, latest))
						{
							yield return messageId;
						}
					}
				}
				else
				{
					foreach (VersionedId messageId2 in this.ReadMessageIdsFromQueryResult(queryResult, momentsAgo, latest))
					{
						yield return messageId2;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001264E File Offset: 0x0001084E
		public override IConfigurable Read<T>(ObjectId identity)
		{
			return this.Find<T>(null, identity, true, null).FirstOrDefault<IConfigurable>();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001265F File Offset: 0x0001085F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Folder != null)
			{
				this.Folder.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000128F8 File Offset: 0x00010AF8
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			foreach (VersionedId messageId in this.FindMessageIds(rootId, sortBy, true))
			{
				using (AuditLogSearchItemBase requestItem = this.GetItemFromStore(messageId))
				{
					AuditLogSearchBase request = (AuditLogSearchBase)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
					request.Initialize(requestItem);
					yield return (T)((object)request);
				}
			}
			yield break;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012924 File Offset: 0x00010B24
		protected override void InternalSave(ConfigurableObject instance)
		{
			switch (instance.ObjectState)
			{
			case ObjectState.New:
				this.SaveObjectToStore((AuditLogSearchBase)instance);
				return;
			case ObjectState.Unchanged:
				break;
			case ObjectState.Changed:
				this.SaveRequest((AuditLogSearchBase)instance);
				return;
			case ObjectState.Deleted:
				base.Delete(instance);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012970 File Offset: 0x00010B70
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AuditLogSearchDataProviderBase>(this);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012978 File Offset: 0x00010B78
		private void SaveRequest(AuditLogSearchBase search)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001297F File Offset: 0x00010B7F
		protected virtual void SaveObjectToStore(AuditLogSearchBase search)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012986 File Offset: 0x00010B86
		internal virtual AuditLogSearchItemBase GetItemFromStore(VersionedId messageId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00012990 File Offset: 0x00010B90
		internal void DeleteItem(VersionedId messageId)
		{
			base.MailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				messageId
			});
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000129B6 File Offset: 0x00010BB6
		internal void DeleteAllItems()
		{
			this.folder.DeleteAllObjects(DeleteItemFlags.SoftDelete);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012BF0 File Offset: 0x00010DF0
		private IEnumerable<VersionedId> ReadMessageIdsFromQueryResult(QueryResult queryResult, ExDateTime cutoffDateTime, bool latest)
		{
			object[][] rows = queryResult.GetRows(1000);
			foreach (object[] row in rows)
			{
				VersionedId messageId = PropertyBag.CheckPropertyValue<VersionedId>(ItemSchema.Id, row[0]);
				ExDateTime createTime = PropertyBag.CheckPropertyValue<ExDateTime>(StoreObjectSchema.CreationTime, row[1]);
				if ((latest && createTime > cutoffDateTime) || (!latest && createTime <= cutoffDateTime))
				{
					yield return messageId;
				}
			}
			yield break;
		}

		// Token: 0x0400022F RID: 559
		private const int MaxItemsToQuery = 1000;

		// Token: 0x04000230 RID: 560
		private static readonly TimeSpan DelayPeriod = TimeSpan.FromSeconds(-20.0);

		// Token: 0x04000231 RID: 561
		private static readonly PropertyDefinition[] FolderProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x04000232 RID: 562
		private static readonly PropertyDefinition[] MessageProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.CreationTime,
			AuditLogSearchItemSchema.Identity
		};

		// Token: 0x04000233 RID: 563
		private readonly AuditLogSearchDataProviderBase.SearchSetting setting;

		// Token: 0x04000234 RID: 564
		private readonly Folder folder;

		// Token: 0x02000089 RID: 137
		internal class SearchSetting
		{
			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000470 RID: 1136 RVA: 0x00012C7E File Offset: 0x00010E7E
			internal object LockObj
			{
				get
				{
					return this.lockObj;
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000471 RID: 1137 RVA: 0x00012C86 File Offset: 0x00010E86
			// (set) Token: 0x06000472 RID: 1138 RVA: 0x00012C8E File Offset: 0x00010E8E
			public string FolderName { get; set; }

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000473 RID: 1139 RVA: 0x00012C97 File Offset: 0x00010E97
			// (set) Token: 0x06000474 RID: 1140 RVA: 0x00012C9F File Offset: 0x00010E9F
			public QueryFilter MessageQueryFilter { get; set; }

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x00012CA8 File Offset: 0x00010EA8
			// (set) Token: 0x06000476 RID: 1142 RVA: 0x00012CB0 File Offset: 0x00010EB0
			public Dictionary<Guid, StoreObjectId> CachedFolderIds { get; set; }

			// Token: 0x04000235 RID: 565
			private readonly object lockObj = new object();
		}
	}
}
