using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200067B RID: 1659
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UMFaxValidation : SearchFolderValidation
	{
		// Token: 0x06004456 RID: 17494 RVA: 0x0012357C File Offset: 0x0012177C
		internal UMFaxValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x001235A0 File Offset: 0x001217A0
		internal static QueryFilter GetUMFaxQueryFilter(DefaultFolderContext context)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Fax.CA");
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x001235B4 File Offset: 0x001217B4
		internal static SearchFolderCriteria CreateUMFaxSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(UMFaxValidation.GetUMFaxQueryFilter(context), new StoreId[]
			{
				context[DefaultFolderType.Inbox]
			})
			{
				DeepTraversal = true
			};
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x001235E8 File Offset: 0x001217E8
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			if (!base.EnsureIsValid(context, folder))
			{
				return false;
			}
			OutlookSearchFolder outlookSearchFolder = folder as OutlookSearchFolder;
			return outlookSearchFolder != null && this.ValidateUMFaxFilter(context, outlookSearchFolder);
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x00123618 File Offset: 0x00121818
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			folder[InternalSchema.OutlookSearchFolderClsId] = UMFaxValidation.UmFaxClsId;
			folder.ClassName = "IPF.Note.Microsoft.Fax";
			OutlookSearchFolder outlookSearchFolder = (OutlookSearchFolder)folder;
			outlookSearchFolder.Save();
			outlookSearchFolder.ApplyContinuousSearch(UMFaxValidation.CreateUMFaxSearchCriteria(context));
			outlookSearchFolder.Load(null);
			outlookSearchFolder.MakeVisibleToOutlook(true);
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x00123678 File Offset: 0x00121878
		private bool ValidateUMFaxFilter(DefaultFolderContext context, OutlookSearchFolder folder)
		{
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(folder);
			if (searchFolderCriteria == null || !UMFaxValidation.GetUMFaxQueryFilter(context).Equals(searchFolderCriteria.SearchQuery))
			{
				folder.ApplyContinuousSearch(UMFaxValidation.CreateUMFaxSearchCriteria(context));
			}
			return true;
		}

		// Token: 0x0400254D RID: 9549
		private static readonly Guid UmFaxClsId = new Guid("{22875B0C-FEF8-4150-952A-5D0EEE323D99}");
	}
}
