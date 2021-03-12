using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EntryIdStrategy
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x0002D363 File Offset: 0x0002B563
		internal EntryIdStrategy()
		{
		}

		// Token: 0x06000569 RID: 1385
		internal abstract void GetDependentProperties(object location, IList<StorePropertyDefinition> result);

		// Token: 0x0600056A RID: 1386
		internal abstract byte[] GetEntryId(DefaultFolderContext context);

		// Token: 0x0600056B RID: 1387
		internal abstract void SetEntryId(DefaultFolderContext context, byte[] entryId);

		// Token: 0x0600056C RID: 1388
		internal abstract FolderSaveResult UnsetEntryId(DefaultFolderContext context);

		// Token: 0x04000171 RID: 369
		internal static NoEntryIdStrategy NoEntryId = new NoEntryIdStrategy();
	}
}
