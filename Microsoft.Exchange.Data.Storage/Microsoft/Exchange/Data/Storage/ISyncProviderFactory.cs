using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E5A RID: 3674
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncProviderFactory
	{
		// Token: 0x06007F3F RID: 32575
		ISyncProvider CreateSyncProvider(ISyncLogger syncLogger = null);

		// Token: 0x06007F40 RID: 32576
		byte[] GetCollectionIdBytes();

		// Token: 0x06007F41 RID: 32577
		void SetCollectionIdFromBytes(byte[] collectionBytes);
	}
}
