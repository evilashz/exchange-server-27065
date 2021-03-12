using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200065B RID: 1627
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AllContactsFolderValidation : SearchFolderValidation
	{
		// Token: 0x06004391 RID: 17297 RVA: 0x0011E510 File Offset: 0x0011C710
		internal AllContactsFolderValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x0011E534 File Offset: 0x0011C734
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			folder.Save();
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.ApplyContinuousSearch(this.CreateSearchCriteria(context));
			folder.Load(null);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x0011E56C File Offset: 0x0011C76C
		private static QueryFilter GetQueryFilter(DefaultFolderContext context)
		{
			return new AndFilter(new QueryFilter[]
			{
				SearchFolderValidation.GetSearchExclusionFoldersFilter(context, null, AllContactsFolderValidation.ExcludedFolders),
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Contact"),
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.DistList")
				})
			});
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x0011E5CC File Offset: 0x0011C7CC
		private SearchFolderCriteria CreateSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(AllContactsFolderValidation.GetQueryFilter(context), new StoreId[]
			{
				context[DefaultFolderType.Root]
			})
			{
				DeepTraversal = true
			};
		}

		// Token: 0x040024C7 RID: 9415
		private static readonly DefaultFolderType[] ExcludedFolders = new DefaultFolderType[]
		{
			DefaultFolderType.Conflicts,
			DefaultFolderType.DeletedItems,
			DefaultFolderType.Drafts,
			DefaultFolderType.Inbox,
			DefaultFolderType.JunkEmail,
			DefaultFolderType.LocalFailures,
			DefaultFolderType.Outbox,
			DefaultFolderType.SentItems,
			DefaultFolderType.ServerFailures,
			DefaultFolderType.SyncIssues,
			DefaultFolderType.DocumentSyncIssues
		};
	}
}
