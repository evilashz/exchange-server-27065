using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000884 RID: 2180
	internal interface IMserveService
	{
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002E6C RID: 11884
		// (set) Token: 0x06002E6D RID: 11885
		bool TrackDuplicatedAddEntries { get; set; }

		// Token: 0x06002E6E RID: 11886
		List<RecipientSyncOperation> Synchronize(RecipientSyncOperation op);

		// Token: 0x06002E6F RID: 11887
		List<RecipientSyncOperation> Synchronize(List<RecipientSyncOperation> operations);

		// Token: 0x06002E70 RID: 11888
		List<RecipientSyncOperation> Synchronize();

		// Token: 0x06002E71 RID: 11889
		void Reset();
	}
}
