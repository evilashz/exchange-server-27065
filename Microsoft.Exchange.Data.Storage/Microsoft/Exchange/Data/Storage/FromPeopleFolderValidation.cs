using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000675 RID: 1653
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FromPeopleFolderValidation : SearchFolderValidation
	{
		// Token: 0x0600443F RID: 17471 RVA: 0x0012306C File Offset: 0x0012126C
		internal FromPeopleFolderValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x00123090 File Offset: 0x00121290
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			FromPeopleFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Entering FromPeopleFolderValidation.EnsureIsValid");
			if (!base.EnsureIsValid(context, folder))
			{
				FromPeopleFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromPeopleFolderValidation.EnsureIsValid:  folder failed base class validation.");
				return false;
			}
			SearchFolder searchFolder = folder as SearchFolder;
			if (searchFolder == null)
			{
				FromPeopleFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromPeopleFolderValidation.Validate:  not a SearchFolder instance.");
				return false;
			}
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(searchFolder);
			SearchFolderCriteria searchCriteria = FromPeopleFolderValidation.GetSearchCriteria(context);
			if (searchFolderCriteria == null || !SearchFolderValidation.MatchSearchFolderCriteria(searchFolderCriteria, searchCriteria))
			{
				FromPeopleFolderValidation.Tracer.TraceDebug((long)this.GetHashCode(), "Current criteria is not initialized or doesn't match desired criteria.  Updating.");
				searchFolder.ApplyContinuousSearch(searchCriteria);
			}
			FromPeopleFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromPeopleFolderValidation.EnsureIsValid.  Validation is done.");
			return true;
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x00123148 File Offset: 0x00121348
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			FromPeopleFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "FromPeopleFolderValidation.SetPropertiesInternal");
			base.SetPropertiesInternal(context, folder);
			folder.Save();
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.ApplyContinuousSearch(FromPeopleFolderValidation.GetSearchCriteria(context));
			folder.Load(null);
			FromPeopleFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting FromPeopleFolderValidation.SetPropertiesInternal.  Initialization is done.");
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x001231C8 File Offset: 0x001213C8
		private static SearchFolderCriteria GetSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(FromPeopleFolderValidation.SearchQueryFilter, (from defaultFolderType in FromPeopleFolderValidation.FolderScope
			select context.Session.GetDefaultFolderId(defaultFolderType)).ToArray<StoreObjectId>());
		}

		// Token: 0x04002545 RID: 9541
		private static readonly Trace Tracer = ExTraceGlobals.StorageTracer;

		// Token: 0x04002546 RID: 9542
		private static readonly DefaultFolderType[] FolderScope = new DefaultFolderType[]
		{
			DefaultFolderType.Inbox,
			DefaultFolderType.SentItems
		};

		// Token: 0x04002547 RID: 9543
		private static readonly ExistsFilter ExchangeApplicationFlagsExists = new ExistsFilter(ItemSchema.ExchangeApplicationFlags);

		// Token: 0x04002548 RID: 9544
		private static readonly BitMaskFilter IsFromPersonFilter = new BitMaskFilter(ItemSchema.ExchangeApplicationFlags, 2048UL, true);

		// Token: 0x04002549 RID: 9545
		private static readonly BitMaskFilter IsFromFavoriteSenderFilter = new BitMaskFilter(ItemSchema.ExchangeApplicationFlags, 1UL, true);

		// Token: 0x0400254A RID: 9546
		private static readonly QueryFilter SearchQueryFilter = new AndFilter(new QueryFilter[]
		{
			FromPeopleFolderValidation.ExchangeApplicationFlagsExists,
			new OrFilter(new QueryFilter[]
			{
				FromPeopleFolderValidation.IsFromPersonFilter,
				FromPeopleFolderValidation.IsFromFavoriteSenderFilter
			})
		});
	}
}
