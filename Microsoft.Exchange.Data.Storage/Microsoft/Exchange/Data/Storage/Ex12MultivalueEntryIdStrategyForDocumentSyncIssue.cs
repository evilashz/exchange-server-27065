using System;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Ex12MultivalueEntryIdStrategyForDocumentSyncIssue : Ex12MultivalueEntryIdStrategy
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0002D97A File Offset: 0x0002BB7A
		internal Ex12MultivalueEntryIdStrategyForDocumentSyncIssue(StorePropertyDefinition property, LocationEntryIdStrategy.GetLocationPropertyBagDelegate getLocationPropertyBag) : base(property, getLocationPropertyBag, 1)
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002D985 File Offset: 0x0002BB85
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			if (Utils.IsTeamMailbox(context.Session))
			{
				return base.GetEntryId(context);
			}
			return null;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002D99D File Offset: 0x0002BB9D
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			if (Utils.IsTeamMailbox(context.Session))
			{
				base.SetEntryId(context, entryId);
			}
		}
	}
}
