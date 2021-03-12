using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISynchronizationUploadContextBase : IDisposable
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007B4 RID: 1972
		StoreSession Session { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007B5 RID: 1973
		StoreObjectId SyncRootFolderId { get; }

		// Token: 0x060007B6 RID: 1974
		void GetCurrentState(ref StorageIcsState currentState);
	}
}
