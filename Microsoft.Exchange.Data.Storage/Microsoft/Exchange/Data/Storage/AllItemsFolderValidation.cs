using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200065F RID: 1631
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AllItemsFolderValidation : SearchFolderValidation
	{
		// Token: 0x060043A8 RID: 17320 RVA: 0x0011ED04 File Offset: 0x0011CF04
		internal AllItemsFolderValidation() : base(new IValidator[]
		{
			new MatchContainerClass("IPF"),
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x0011ED35 File Offset: 0x0011CF35
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			return base.EnsureIsValid(context, folder) && AllItemsFolderValidation.VerifyAndFixSearchFolder(context, (SearchFolder)folder);
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x0011ED50 File Offset: 0x0011CF50
		private static void TryApplyContinuousSearch(SearchFolder folder, SearchFolderCriteria criteria)
		{
			try
			{
				folder.ApplyContinuousSearch(criteria);
			}
			catch (QueryInProgressException)
			{
				SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(folder);
				if (searchFolderCriteria == null || !SearchFolderValidation.MatchSearchFolderCriteria(criteria, searchFolderCriteria))
				{
					throw;
				}
			}
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x0011ED90 File Offset: 0x0011CF90
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.Save();
			searchFolder.Load(null);
			AllItemsFolderHelper.InitializeTransportIndexes(folder);
			AllItemsFolderValidation.TryApplyContinuousSearch(searchFolder, AllItemsFolderValidation.CreateSearchCriteria(context));
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x0011EDCC File Offset: 0x0011CFCC
		private static SearchFolderCriteria CreateSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(new ExistsFilter(InternalSchema.ItemClass), new StoreId[]
			{
				context.Session.GetDefaultFolderId(DefaultFolderType.Root)
			})
			{
				DeepTraversal = true
			};
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x0011EE0C File Offset: 0x0011D00C
		private static bool VerifyAndFixSearchFolder(DefaultFolderContext context, SearchFolder folder)
		{
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(folder);
			SearchFolderCriteria searchFolderCriteria2 = AllItemsFolderValidation.CreateSearchCriteria(context);
			if (searchFolderCriteria == null || !SearchFolderValidation.MatchSearchFolderCriteria(searchFolderCriteria, searchFolderCriteria2))
			{
				AllItemsFolderValidation.TryApplyContinuousSearch(folder, searchFolderCriteria2);
			}
			return true;
		}
	}
}
