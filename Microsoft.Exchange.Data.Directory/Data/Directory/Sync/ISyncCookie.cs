using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007AF RID: 1967
	internal interface ISyncCookie
	{
		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x060061C4 RID: 25028
		Dictionary<string, int> ErrorObjectsAndFailureCounts { get; }

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x060061C5 RID: 25029
		DateTime SequenceStartTimestamp { get; }

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x060061C6 RID: 25030
		Guid SequenceId { get; }
	}
}
