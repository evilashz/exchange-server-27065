using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000674 RID: 1652
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FromFavoriteSendersFolderValidation : SearchFolderValidation
	{
		// Token: 0x0600443A RID: 17466 RVA: 0x00122E94 File Offset: 0x00121094
		internal FromFavoriteSendersFolderValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00122EB8 File Offset: 0x001210B8
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			FromFavoriteSendersFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Entering FromFavoriteSendersFolderValidation.EnsureIsValid");
			if (!base.EnsureIsValid(context, folder))
			{
				FromFavoriteSendersFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromFavoriteSendersFolderValidation.EnsureIsValid:  folder failed base class validation.");
				return false;
			}
			SearchFolder searchFolder = folder as SearchFolder;
			if (searchFolder == null)
			{
				FromFavoriteSendersFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromFavoriteSendersFolderValidation.Validate:  not a SearchFolder instance.");
				return false;
			}
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(searchFolder);
			SearchFolderCriteria searchCriteria = FromFavoriteSendersFolderValidation.GetSearchCriteria(context);
			if (searchFolderCriteria == null || !SearchFolderValidation.MatchSearchFolderCriteria(searchFolderCriteria, searchCriteria) || searchFolderCriteria.DeepTraversal != searchCriteria.DeepTraversal)
			{
				FromFavoriteSendersFolderValidation.Tracer.TraceDebug((long)this.GetHashCode(), "Current criteria are NOT initialized or don't match desired criteria.  Updating.");
				searchFolder.ApplyContinuousSearch(searchCriteria);
			}
			FromFavoriteSendersFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromFavoriteSendersFolderValidation.EnsureIsValid.  Validation is done.");
			return true;
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x00122F7C File Offset: 0x0012117C
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			FromFavoriteSendersFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "FromFavoriteSendersFolderValidation.SetPropertiesInternal");
			base.SetPropertiesInternal(context, folder);
			folder.Save();
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.ApplyContinuousSearch(FromFavoriteSendersFolderValidation.GetSearchCriteria(context));
			folder.Load(null);
			FromFavoriteSendersFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromFavoriteSendersFolderValidation.SetPropertiesInternal.  Initialization is done.");
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00122FE0 File Offset: 0x001211E0
		private static SearchFolderCriteria GetSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(FromFavoriteSendersFolderValidation.SearchQueryFilter, new StoreId[]
			{
				context.Session.GetDefaultFolderId(DefaultFolderType.Inbox)
			});
		}

		// Token: 0x04002540 RID: 9536
		public const DefaultFolderType FolderScope = DefaultFolderType.Inbox;

		// Token: 0x04002541 RID: 9537
		private static readonly Trace Tracer = ExTraceGlobals.StorageTracer;

		// Token: 0x04002542 RID: 9538
		private static readonly ExistsFilter ExchangeApplicationFlagsExists = new ExistsFilter(ItemSchema.ExchangeApplicationFlags);

		// Token: 0x04002543 RID: 9539
		private static readonly BitMaskFilter IsFromFavoriteSenderFilter = new BitMaskFilter(ItemSchema.ExchangeApplicationFlags, 1UL, true);

		// Token: 0x04002544 RID: 9540
		public static readonly QueryFilter SearchQueryFilter = new AndFilter(new QueryFilter[]
		{
			FromFavoriteSendersFolderValidation.ExchangeApplicationFlagsExists,
			FromFavoriteSendersFolderValidation.IsFromFavoriteSenderFilter
		});
	}
}
