using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E79 RID: 3705
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedMailboxHelper
	{
		// Token: 0x0600809C RID: 32924 RVA: 0x002328DC File Offset: 0x00230ADC
		internal static QueryFilter CreateQueryFilter(DefaultFolderType defaultFolderType)
		{
			if (defaultFolderType == DefaultFolderType.UnifiedInbox)
			{
				return UnifiedMailboxHelper.UnifiedInboxQueryFilter;
			}
			return null;
		}

		// Token: 0x0600809D RID: 32925 RVA: 0x002328F8 File Offset: 0x00230AF8
		internal static DefaultFolderType[] GetSearchScopeForDefaultSearchFolder(DefaultFolderType defaultFolderType)
		{
			if (defaultFolderType == DefaultFolderType.UnifiedInbox)
			{
				return new DefaultFolderType[]
				{
					DefaultFolderType.Inbox,
					DefaultFolderType.Drafts
				};
			}
			return Array<DefaultFolderType>.Empty;
		}

		// Token: 0x040056AF RID: 22191
		internal static readonly DefaultFolderType[] DefaultSearchFolderTypesSupportedForUnifiedViews = new DefaultFolderType[]
		{
			DefaultFolderType.UnifiedInbox
		};

		// Token: 0x040056B0 RID: 22192
		private static readonly ComparisonFilter NonDraftMessageQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.IsDraft, false);

		// Token: 0x040056B1 RID: 22193
		private static readonly QueryFilter DraftMessageQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.IsDraft, true);

		// Token: 0x040056B2 RID: 22194
		private static readonly QueryFilter NonActionMessageQueryFilter = new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.SubjectPrefix, null),
			new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.SubjectPrefix, string.Empty)
		});

		// Token: 0x040056B3 RID: 22195
		private static readonly QueryFilter DraftNewConversationMessageQueryFilter = new AndFilter(new QueryFilter[]
		{
			UnifiedMailboxHelper.DraftMessageQueryFilter,
			UnifiedMailboxHelper.NonActionMessageQueryFilter
		});

		// Token: 0x040056B4 RID: 22196
		private static readonly QueryFilter UnifiedInboxQueryFilter = new OrFilter(new QueryFilter[]
		{
			UnifiedMailboxHelper.NonDraftMessageQueryFilter,
			UnifiedMailboxHelper.DraftNewConversationMessageQueryFilter
		});
	}
}
