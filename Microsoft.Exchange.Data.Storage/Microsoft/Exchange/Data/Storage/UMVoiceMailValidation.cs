using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200067A RID: 1658
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UMVoiceMailValidation : SearchFolderValidation
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x00123348 File Offset: 0x00121548
		internal UMVoiceMailValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0012336C File Offset: 0x0012156C
		internal static QueryFilter GetUMVoicemailQueryFilter(DefaultFolderContext context)
		{
			return new AndFilter(new QueryFilter[]
			{
				SearchFolderValidation.GetSearchExclusionFoldersFilter(context, null, UMVoiceMailValidation.excludeFromUMSearchFolder),
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Voicemail.UM.CA"),
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA"),
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM"),
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Exchange.Voice.UM.CA"),
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Voicemail.UM"),
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note.Microsoft.Exchange.Voice.UM")
				})
			});
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0012341C File Offset: 0x0012161C
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			if (!base.EnsureIsValid(context, folder))
			{
				return false;
			}
			OutlookSearchFolder outlookSearchFolder = folder as OutlookSearchFolder;
			return outlookSearchFolder != null && this.ValidateUMVoiceMailFilter(context, outlookSearchFolder);
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0012344C File Offset: 0x0012164C
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			folder[InternalSchema.OutlookSearchFolderClsId] = UMVoiceMailValidation.UmVoiceMailClsId;
			folder.ClassName = "IPF.Note.Microsoft.Voicemail";
			OutlookSearchFolder outlookSearchFolder = (OutlookSearchFolder)folder;
			outlookSearchFolder.Save();
			outlookSearchFolder.ApplyContinuousSearch(UMVoiceMailValidation.CreateUMVoiceMailSearchCriteria(context));
			outlookSearchFolder.Load(null);
			outlookSearchFolder.MakeVisibleToOutlook(true);
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x001234AC File Offset: 0x001216AC
		private static SearchFolderCriteria CreateUMVoiceMailSearchCriteria(DefaultFolderContext context)
		{
			return new SearchFolderCriteria(UMVoiceMailValidation.GetUMVoicemailQueryFilter(context), new StoreId[]
			{
				context[DefaultFolderType.Root]
			})
			{
				DeepTraversal = true
			};
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x001234E0 File Offset: 0x001216E0
		private bool ValidateUMVoiceMailFilter(DefaultFolderContext context, OutlookSearchFolder folder)
		{
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(folder);
			if (searchFolderCriteria == null || !UMVoiceMailValidation.GetUMVoicemailQueryFilter(context).Equals(searchFolderCriteria.SearchQuery))
			{
				folder.ApplyContinuousSearch(UMVoiceMailValidation.CreateUMVoiceMailSearchCriteria(context));
				folder.MakeVisibleToOutlook(true);
			}
			return true;
		}

		// Token: 0x0400254B RID: 9547
		private static readonly DefaultFolderType[] excludeFromUMSearchFolder = new DefaultFolderType[]
		{
			DefaultFolderType.DeletedItems,
			DefaultFolderType.JunkEmail,
			DefaultFolderType.SentItems,
			DefaultFolderType.Conflicts,
			DefaultFolderType.LocalFailures,
			DefaultFolderType.ServerFailures,
			DefaultFolderType.SyncIssues,
			DefaultFolderType.Outbox,
			DefaultFolderType.Drafts,
			DefaultFolderType.DocumentSyncIssues
		};

		// Token: 0x0400254C RID: 9548
		private static readonly Guid UmVoiceMailClsId = new Guid("{F9D57CDE-EACF-4493-B0EC-B58EF594A3F7}");
	}
}
