using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NoEntryIdStrategy : EntryIdStrategy
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x0002D377 File Offset: 0x0002B577
		internal override void GetDependentProperties(object location, IList<StorePropertyDefinition> result)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002D379 File Offset: 0x0002B579
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			return null;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002D37C File Offset: 0x0002B57C
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			throw new NotSupportedException("NoEntryIdStrategy does not support Set.");
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002D388 File Offset: 0x0002B588
		internal override FolderSaveResult UnsetEntryId(DefaultFolderContext context)
		{
			throw new NotSupportedException("NoEntryIdStrategy does not support Unset.");
		}
	}
}
