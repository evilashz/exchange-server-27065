using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200066F RID: 1647
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DumpsterFolderHelper
	{
		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x00122378 File Offset: 0x00120578
		public static QueryFilter ExcludeAuditFoldersFilter
		{
			get
			{
				return DumpsterFolderHelper.excludeAuditFoldersFilter;
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x00122380 File Offset: 0x00120580
		public static bool RunQueryOnDiscoveryHoldsFolder(MailboxSession session, SortBy sortBy, Func<QueryResult, bool> queryProcessor, ICollection<PropertyDefinition> properties)
		{
			StoreId folderId = session.CowSession.CheckAndCreateDiscoveryHoldsFolder(session);
			bool result;
			using (Folder folder = Folder.Bind(session, folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
				{
					sortBy
				}, properties))
				{
					result = queryProcessor(queryResult);
				}
			}
			return result;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001223F8 File Offset: 0x001205F8
		public static bool RunQueryOnMigratedMessagesFolder(MailboxSession session, SortBy sortBy, Func<QueryResult, bool> queryProcessor, ICollection<PropertyDefinition> properties)
		{
			StoreId folderId = session.CowSession.CheckAndCreateMigratedMessagesFolder();
			bool result;
			using (Folder folder = Folder.Bind(session, folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
				{
					sortBy
				}, properties))
				{
					result = queryProcessor(queryResult);
				}
			}
			return result;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x00122470 File Offset: 0x00120670
		public static IStorePropertyBag[] FindItemsFromInternetId(MailboxSession session, string internetMessageId, params PropertyDefinition[] propertyDefinitions)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullOrEmptyArgument(internetMessageId, "internetMessageId");
			Util.ThrowOnNullOrEmptyArgument(propertyDefinitions, "propertyDefinitions");
			DumpsterFolderHelper.CheckAndCreateFolder(session);
			ICollection<PropertyDefinition> dataColumns = InternalSchema.Combine<PropertyDefinition>(propertyDefinitions, new PropertyDefinition[]
			{
				ItemSchema.InternetMessageId
			});
			IStorePropertyBag[] result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.RecoverableItemsDeletions))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, DumpsterFolderHelper.searchSortBy, dataColumns))
				{
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, DumpsterFolderHelper.searchSortBy[0].ColumnDefinition, internetMessageId)))
					{
						result = DumpsterFolderHelper.ProcessQueryResult(queryResult, internetMessageId);
					}
					else
					{
						result = Array<IStorePropertyBag>.Empty;
					}
				}
			}
			return result;
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x00122538 File Offset: 0x00120738
		internal static IStorePropertyBag[] ProcessQueryResult(QueryResult queryResult, string internetMessageId)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>(3);
			for (;;)
			{
				IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(3);
				for (int i = 0; i < propertyBags.Length; i++)
				{
					string b = propertyBags[i].TryGetProperty(ItemSchema.InternetMessageId) as string;
					if (!string.Equals(internetMessageId, b, StringComparison.OrdinalIgnoreCase))
					{
						goto IL_3B;
					}
					list.Add(propertyBags[i]);
				}
				if (propertyBags.Length == 0)
				{
					goto Block_3;
				}
			}
			IL_3B:
			return list.ToArray();
			Block_3:
			return list.ToArray();
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x0012259C File Offset: 0x0012079C
		public static void CheckAndCreateFolder(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			if (session.LogonType == LogonType.Delegated)
			{
				return;
			}
			foreach (DefaultFolderType defaultFolderType in DumpsterFolderHelper.dumpsterFoldersTypes)
			{
				if (session.GetDefaultFolderId(defaultFolderType) == null)
				{
					StoreObjectId storeObjectId = session.CreateDefaultFolder(defaultFolderType);
					session.GetDefaultFolderId(defaultFolderType);
				}
			}
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x001225F4 File Offset: 0x001207F4
		public static bool IsDumpsterFolder(MailboxSession session, StoreObjectId folderId)
		{
			Util.ThrowOnNullArgument(session, "session");
			if (folderId == null)
			{
				return false;
			}
			foreach (DefaultFolderType defaultFolderType in DumpsterFolderHelper.dumpsterFoldersTypes)
			{
				if (folderId.Equals(session.GetDefaultFolderId(defaultFolderType)))
				{
					return true;
				}
			}
			return folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDiscoveryHolds)) || folderId.Equals(session.GetDefaultFolderId(DefaultFolderType.RecoverableItemsMigratedMessages)) || DumpsterFolderHelper.IsAuditFolder(session, folderId);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x00122698 File Offset: 0x00120898
		public static bool IsAuditFolder(MailboxSession session, StoreObjectId folderId)
		{
			Util.ThrowOnNullArgument(session, "session");
			if (folderId == null)
			{
				return false;
			}
			StoreObjectId auditsFolder = null;
			StoreObjectId adminAuditFolder = null;
			session.BypassAuditsFolderAccessChecking(delegate
			{
				auditsFolder = session.GetAuditsFolderId();
				adminAuditFolder = session.GetAdminAuditLogsFolderId();
			});
			return folderId.Equals(auditsFolder) || folderId.Equals(adminAuditFolder);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x0012270B File Offset: 0x0012090B
		public static MiddleTierStoragePerformanceCountersInstance GetPerfCounters()
		{
			return NamedPropMap.GetPerfCounters();
		}

		// Token: 0x04002532 RID: 9522
		private static readonly QueryFilter excludeAuditFoldersFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.DisplayName, "Audits"),
			new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.DisplayName, "AdminAuditLogs")
		});

		// Token: 0x04002533 RID: 9523
		private static SortBy[] searchSortBy = new SortBy[]
		{
			new SortBy(ItemSchema.InternetMessageId, SortOrder.Ascending)
		};

		// Token: 0x04002534 RID: 9524
		private static DefaultFolderType[] dumpsterFoldersTypes = new DefaultFolderType[]
		{
			DefaultFolderType.RecoverableItemsRoot,
			DefaultFolderType.RecoverableItemsDeletions,
			DefaultFolderType.RecoverableItemsVersions,
			DefaultFolderType.RecoverableItemsPurges,
			DefaultFolderType.CalendarLogging
		};
	}
}
