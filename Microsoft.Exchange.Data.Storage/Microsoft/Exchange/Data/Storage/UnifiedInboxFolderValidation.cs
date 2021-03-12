using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200068B RID: 1675
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedInboxFolderValidation : SearchFolderValidation
	{
		// Token: 0x06004493 RID: 17555 RVA: 0x001249E8 File Offset: 0x00122BE8
		internal UnifiedInboxFolderValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x00124A0C File Offset: 0x00122C0C
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			UnifiedInboxFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Entering UnifiedInboxFolderValidation.EnsureIsValid");
			if (!base.EnsureIsValid(context, folder))
			{
				UnifiedInboxFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting UnifiedInboxFolderValidation.EnsureIsValid: folder failed base class validation.");
				return false;
			}
			SearchFolder searchFolder = folder as SearchFolder;
			if (searchFolder == null)
			{
				UnifiedInboxFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting UnifiedInboxFolderValidation.Validate: not a SearchFolder instance.");
				return false;
			}
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(searchFolder);
			SearchFolderCriteria searchCriteria = UnifiedInboxFolderValidation.GetSearchCriteria(context);
			if (searchFolderCriteria == null || !SearchFolderValidation.MatchSearchFolderCriteria(searchFolderCriteria, searchCriteria) || searchFolderCriteria.DeepTraversal != searchCriteria.DeepTraversal)
			{
				UnifiedInboxFolderValidation.Tracer.TraceDebug((long)this.GetHashCode(), "Current criteria are NOT initialized or don't match desired criteria. Updating.");
				searchFolder.ApplyContinuousSearch(searchCriteria);
			}
			UnifiedInboxFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting UnifiedInboxFolderValidation.EnsureIsValid.  Validation is done.");
			return true;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x00124AD0 File Offset: 0x00122CD0
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			UnifiedInboxFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "UnifiedInboxFolderValidation.SetPropertiesInternal");
			base.SetPropertiesInternal(context, folder);
			folder.Save();
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.ApplyContinuousSearch(UnifiedInboxFolderValidation.GetSearchCriteria(context));
			folder.Load(null);
			UnifiedInboxFolderValidation.Tracer.TraceFunction((long)this.GetHashCode(), "Exiting UnifiedInboxFolderValidation.SetPropertiesInternal. Initialization is done.");
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x00124B50 File Offset: 0x00122D50
		private static SearchFolderCriteria GetSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(UnifiedInboxFolderValidation.SearchQueryFilter, (from defaultFolderType in UnifiedInboxFolderValidation.FolderScope
			select context.Session.GetDefaultFolderId(defaultFolderType)).ToArray<StoreObjectId>());
		}

		// Token: 0x0400255F RID: 9567
		private static readonly Trace Tracer = ExTraceGlobals.StorageTracer;

		// Token: 0x04002560 RID: 9568
		private static readonly DefaultFolderType DefaultFolderType = DefaultFolderType.UnifiedInbox;

		// Token: 0x04002561 RID: 9569
		public static readonly DefaultFolderType[] FolderScope = UnifiedMailboxHelper.GetSearchScopeForDefaultSearchFolder(UnifiedInboxFolderValidation.DefaultFolderType);

		// Token: 0x04002562 RID: 9570
		public static readonly QueryFilter SearchQueryFilter = UnifiedMailboxHelper.CreateQueryFilter(UnifiedInboxFolderValidation.DefaultFolderType);
	}
}
