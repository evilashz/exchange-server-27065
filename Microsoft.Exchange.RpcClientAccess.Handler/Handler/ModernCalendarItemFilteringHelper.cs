using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000013 RID: 19
	internal static class ModernCalendarItemFilteringHelper
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000069B8 File Offset: 0x00004BB8
		internal static QueryFilter GetDefaultContentsViewFilter(CoreFolder coreFolder, Logon logon)
		{
			if (logon.Session.MailboxOwner.GetConfiguration().RpcClientAccess.FilterModernCalendarItemsMomtView.Enabled && ModernCalendarItemFilteringHelper.FolderNeedsFilter(coreFolder.Id.ObjectId, logon))
			{
				return ModernCalendarItemFilteringHelper.IgnoreModernCalendarItemsFilter;
			}
			return null;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006A04 File Offset: 0x00004C04
		internal static QueryFilter AddFolderFilterForIcsIfRequired(QueryFilter queryFilter, CoreFolder coreFolder, Logon logon)
		{
			if (logon.Session.MailboxOwner.GetConfiguration().RpcClientAccess.FilterModernCalendarItemsMomtIcs.Enabled && ModernCalendarItemFilteringHelper.FolderNeedsFilter(coreFolder.Id.ObjectId, logon))
			{
				queryFilter = ModernCalendarItemFilteringHelper.AddModernCalendarItemFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006A54 File Offset: 0x00004C54
		internal static QueryFilter AddModernCalendarItemsFilterForSearch(QueryFilter queryFilter, Logon logon)
		{
			if (logon.Session.MailboxOwner.GetConfiguration().RpcClientAccess.FilterModernCalendarItemsMomtSearch.Enabled)
			{
				return ModernCalendarItemFilteringHelper.AddModernCalendarItemFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006A90 File Offset: 0x00004C90
		internal static void RemoveModernCalendarItemRestriction(SearchFolderCriteria searchFolderCriteria, Logon logon)
		{
			if (logon.Session.MailboxOwner.GetConfiguration().RpcClientAccess.FilterModernCalendarItemsMomtSearch.Enabled)
			{
				QueryFilter searchQuery;
				if (ModernCalendarItemFilteringHelper.TryRemovingModernCalendarItemAndClause(searchFolderCriteria.SearchQuery, out searchQuery))
				{
					searchFolderCriteria.SearchQuery = searchQuery;
					return;
				}
				if (ModernCalendarItemFilteringHelper.IsModernCalendarItemFilter(searchFolderCriteria.SearchQuery))
				{
					searchFolderCriteria.SearchQuery = null;
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006AEC File Offset: 0x00004CEC
		private static QueryFilter AddModernCalendarItemFilter(QueryFilter queryFilter)
		{
			if (queryFilter == null)
			{
				queryFilter = ModernCalendarItemFilteringHelper.IgnoreModernCalendarItemsFilter;
			}
			else
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					ModernCalendarItemFilteringHelper.IgnoreModernCalendarItemsFilter,
					queryFilter
				});
			}
			return queryFilter;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006B21 File Offset: 0x00004D21
		private static bool FolderNeedsFilter(StoreObjectId folderObjectId, Logon logon)
		{
			return folderObjectId.ObjectType == StoreObjectType.CalendarFolder && !ModernCalendarItemFilteringHelper.IsConnectionWithSupportTool(logon);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006B38 File Offset: 0x00004D38
		private static bool IsConnectionWithSupportTool(Logon logon)
		{
			string processName = logon.Connection.ClientInformation.ProcessName;
			return processName != null && processName.Contains("mfcmapi");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006B68 File Offset: 0x00004D68
		private static bool TryRemovingModernCalendarItemAndClause(QueryFilter queryFilter, out QueryFilter newFilter)
		{
			newFilter = null;
			AndFilter andFilter = queryFilter as AndFilter;
			if (andFilter != null && andFilter.FilterCount > 1 && ModernCalendarItemFilteringHelper.IsModernCalendarItemFilter(andFilter.Filters[0]))
			{
				if (andFilter.FilterCount == 2)
				{
					newFilter = andFilter.Filters[1];
				}
				else
				{
					newFilter = new AndFilter(andFilter.IgnoreWhenVerifyingMaxDepth, andFilter.Filters.Skip(1).ToArray<QueryFilter>());
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006BD8 File Offset: 0x00004DD8
		private static bool IsModernCalendarItemFilter(QueryFilter queryFilter)
		{
			return ModernCalendarItemFilteringHelper.IgnoreModernCalendarItemsFilter.Equals(queryFilter);
		}

		// Token: 0x04000052 RID: 82
		private static readonly ComparisonFilter IgnoreModernCalendarItemsFilter = new ComparisonFilter(ComparisonOperator.NotEqual, CalendarItemBaseSchema.IsHiddenFromLegacyClients, true);
	}
}
