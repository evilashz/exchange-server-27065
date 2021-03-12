using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200065A RID: 1626
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SearchFolderValidation : DefaultFolderValidator
	{
		// Token: 0x0600438C RID: 17292 RVA: 0x0011E37A File Offset: 0x0011C57A
		internal SearchFolderValidation(params IValidator[] validators) : base(validators)
		{
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x0011E384 File Offset: 0x0011C584
		internal static SearchFolderCriteria TryGetSearchCriteria(SearchFolder folder)
		{
			SearchFolderCriteria result = null;
			try
			{
				result = folder.GetSearchCriteria();
			}
			catch (ObjectNotInitializedException)
			{
			}
			catch (CorruptDataException)
			{
			}
			return result;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x0011E3C0 File Offset: 0x0011C5C0
		internal static QueryFilter GetSearchExclusionFoldersFilter(DefaultFolderContext context, IEnumerable<QueryFilter> currentExclusionCriteria, DefaultFolderType[] excludedDefaultFolders)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (currentExclusionCriteria != null)
			{
				list.AddRange(currentExclusionCriteria);
			}
			foreach (DefaultFolderType defaultFolderType in excludedDefaultFolders)
			{
				StoreObjectId storeObjectId = context[defaultFolderType];
				if (storeObjectId != null)
				{
					ComparisonFilter item = new ComparisonFilter(ComparisonOperator.NotEqual, InternalSchema.ParentItemId, storeObjectId);
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			return new AndFilter(list.ToArray());
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x0011E42C File Offset: 0x0011C62C
		protected static bool MatchSearchFolderCriteria(SearchFolderCriteria currentCriteria, SearchFolderCriteria expectedCriteria)
		{
			if (currentCriteria.FolderScope.Length != expectedCriteria.FolderScope.Length)
			{
				return false;
			}
			for (int i = 0; i < expectedCriteria.FolderScope.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < currentCriteria.FolderScope.Length; j++)
				{
					if (currentCriteria.FolderScope[j].Equals(expectedCriteria.FolderScope[i]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ExTraceGlobals.DefaultFoldersTracer.TraceError<int, string>(-1L, "SearchFolderValidation::MatchSearchFolderCriteria. We failed to find the folder scope from the current search criteria. Index = {0}, Id = {1}.", i, expectedCriteria.FolderScope[i].ToBase64String());
					return false;
				}
			}
			return currentCriteria.SearchQuery.Equals(expectedCriteria.SearchQuery);
		}

		// Token: 0x040024C6 RID: 9414
		internal static readonly DefaultFolderType[] ExcludeFromRemindersSearchFolder = new DefaultFolderType[]
		{
			DefaultFolderType.Conflicts,
			DefaultFolderType.LocalFailures,
			DefaultFolderType.ServerFailures,
			DefaultFolderType.SyncIssues,
			DefaultFolderType.DeletedItems,
			DefaultFolderType.JunkEmail,
			DefaultFolderType.Outbox,
			DefaultFolderType.Drafts,
			DefaultFolderType.DocumentSyncIssues
		};
	}
}
