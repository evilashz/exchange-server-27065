using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000673 RID: 1651
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FavoritesFolderValidation : SearchFolderValidation
	{
		// Token: 0x06004436 RID: 17462 RVA: 0x00122DC8 File Offset: 0x00120FC8
		internal FavoritesFolderValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00122DEC File Offset: 0x00120FEC
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			folder.Save();
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.ApplyContinuousSearch(this.CreateSearchCriteria(context));
			folder.Load(null);
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x00122E24 File Offset: 0x00121024
		private static QueryFilter GetQueryFilter()
		{
			return new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Contact"),
				new ComparisonFilter(ComparisonOperator.Equal, ContactSchema.IsFavorite, true)
			});
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00122E68 File Offset: 0x00121068
		private SearchFolderCriteria CreateSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(FavoritesFolderValidation.GetQueryFilter(), new StoreId[]
			{
				context[DefaultFolderType.QuickContacts]
			});
		}
	}
}
