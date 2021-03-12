using System;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Ex12MultivalueEntryIdStrategyForSyncIssue : Ex12MultivalueEntryIdStrategy
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x0002D940 File Offset: 0x0002BB40
		internal Ex12MultivalueEntryIdStrategyForSyncIssue(StorePropertyDefinition property, LocationEntryIdStrategy.GetLocationPropertyBagDelegate getLocationPropertyBag) : base(property, getLocationPropertyBag, 1)
		{
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002D94B File Offset: 0x0002BB4B
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			if (!Utils.IsTeamMailbox(context.Session))
			{
				return base.GetEntryId(context);
			}
			return null;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002D963 File Offset: 0x0002BB63
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			if (!Utils.IsTeamMailbox(context.Session))
			{
				base.SetEntryId(context, entryId);
			}
		}
	}
}
