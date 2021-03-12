using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000261 RID: 609
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _MoveCopyAction
	{
		// Token: 0x040010C5 RID: 4293
		internal int cbStoreEntryID;

		// Token: 0x040010C6 RID: 4294
		internal unsafe byte* lpbStoreEntryID;

		// Token: 0x040010C7 RID: 4295
		internal int cbFolderEntryID;

		// Token: 0x040010C8 RID: 4296
		internal unsafe byte* lpbFolderEntryID;
	}
}
